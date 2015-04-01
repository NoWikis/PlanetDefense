using UnityEngine;
using System.Collections;

public class AlienPlanet : MonoBehaviour {

	public bool activePlanet;
	public GameObject projectile;
	public GameObject explosionPrefab;

	private float shootingTimer;
	public float shootingCooldown;
	private float burstTimer = 0;
	public float burstCooldown;
	private int burstCount = 0;
	public int burstTotal;
	
	Health health;
	
	public float item_spawn_chance = 1f;
	public GameObject[] item_list;

	// Use this for initialization
	void Start () {
		health = GetComponent<Health>();
	}
	
	// Update is called once per frame
	void Update () {
		activePlanet = this.renderer.isVisible;
		if (activePlanet) {
			if (shootingTimer > shootingCooldown && burstCount < burstTotal) {
				if (burstTimer > burstCooldown) {
					for (int x = 1; x <= 3; x++) {
						shootProjectile(20*x - 40);
					}
					burstTimer = 0;
					burstCount++;
				}
				else {
					burstTimer += Time.deltaTime;
				}
			}
			else if (shootingTimer < shootingCooldown) {
				shootingTimer += Time.deltaTime;
			}
			else if (burstCount == burstTotal) {
				shootingTimer = 0f;
				burstCount = 0;
			}
		}
		if (health.isDead()) {
			int spawn_item = Mathf.RoundToInt(Random.value * (item_list.Length - 1));
			float spawn_chance = Random.value;
			if(spawn_chance <= item_spawn_chance){
				GameObject o = (GameObject)Instantiate (item_list[spawn_item]);
				o.transform.position = transform.position;
			}
			Destroy(this.gameObject);
		}
	}

	void shootProjectile(float angle_offset) {
		GameObject o = (GameObject) Instantiate (projectile);
		o.transform.position = transform.position;
		o.GetComponent<AlienProjectile>().initialSpeed = 
			Quaternion.Euler (0, 0, Util.getAngleVector(transform.position,
			    GameObject.FindGameObjectWithTag("Planet").transform.position
			     ) + 270 + angle_offset) * 
				new Vector3(0, 350, 0);
		//Debug.Log (o.GetComponent<projecitile>().initialSpeed);
	}

}
