using UnityEngine;
using System.Collections;

public class AlienPlanet : MonoBehaviour {

	public bool activePlanet;
	public GameObject projectile;
	public GameObject explosionPrefab;

	private float shootingTimer;
	public float shootingCooldown;

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
			if (shootingTimer > shootingCooldown) {
				for (int x = 1; x <= 3; x++) {
					shootProjectile(5*x - 25);
				}
				shootingTimer = 0f;
			}
			shootingTimer += Time.deltaTime;
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
				new Vector3(0, 250, 0);
		//Debug.Log (o.GetComponent<projecitile>().initialSpeed);
	}

}
