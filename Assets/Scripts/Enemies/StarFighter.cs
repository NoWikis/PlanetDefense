using UnityEngine;
using System.Collections;

public class StarFighter : MonoBehaviour {

	public int Health;

	public GameObject projectile;
	public GameObject explosionPrefab;
	public GameObject itemProjectile;

	public float speedRotate;
	public float speedMoving;

	private float planetAngle;
	private Vector3 planetPos;
	private float planetDistance;
	private Vector3 relativeDistance;

	private float time = 0f;
	public float shootingTimeAvg;
	private float shootingTime;

	public float item_spawn_chance = 1f;
	public GameObject[] item_list;
	
	// Use this for initialization
	void Start () {
		planetPos = GameObject.Find ("planet").transform.position;
		planetAngle = Util.getAngleVector (transform.position, planetPos) + 270;
		planetDistance = Vector3.Distance (planetPos, transform.position);
		transform.eulerAngles = new Vector3 (0, 0, planetAngle);
		Debug.Log (planetAngle);
		shootingTime = Random.Range (shootingTimeAvg - 0.5f, shootingTimeAvg + 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
		relativeDistance = transform.position - planetPos;
		planetPos = GameObject.Find ("planet").transform.position;
		//If they don't reach the planet move towards it
	}

	void LateUpdate () {
		if (planetDistance > 20) {
			transform.RotateAround(planetPos, Vector3.forward, 0.1f);
			transform.position = Vector3.MoveTowards(transform.position,planetPos,Time.deltaTime*speedMoving);
			planetAngle = Util.getAngleVector (transform.position, planetPos);
			transform.eulerAngles = new Vector3 (0, 0, planetAngle + 270f);
			planetDistance = Vector3.Distance (planetPos, this.transform.position);
		}
		else {
			transform.position = planetPos + relativeDistance;
			time += Time.deltaTime;
			if (time > shootingTime) {
				shootProjectile();
				time = 0f;
				shootingTime = Random.Range (shootingTimeAvg - 0.5f, shootingTimeAvg + 0.5f);
			}
			transform.RotateAround(planetPos, Vector3.forward, speedRotate);
			planetAngle = Util.getAngleVector (transform.position, planetPos) ;
			transform.eulerAngles = new Vector3 (0, 0, planetAngle + 180f);
			relativeDistance = transform.position - planetPos;
		}
		if (Health < 1) {
			int spawn_item = Mathf.RoundToInt(Random.value * (item_list.Length - 1));
			float spawn_chance = Random.value;
			if(spawn_chance <= item_spawn_chance){
				GameObject o = (GameObject)Instantiate (item_list[spawn_item]);
				o.transform.position = transform.position;
			}
			if (GameObject.Find("StarFighterSpawnerPrefab"))
				GameObject.Find("StarFighterSpawnerPrefab").GetComponent<StarFighterSpawner>().childCount--;
			Destroy(this.gameObject);
		}
		
	}

	void shootProjectile() {
		GameObject o = (GameObject) Instantiate (projectile);
		o.transform.position = transform.position;
		o.GetComponent<AlienProjectile>().initialSpeed = 
			Quaternion.Euler (0, 0, Util.getAngleVector( transform.position,
				GameObject.FindGameObjectWithTag("Planet").transform.position
				)  + 270) * 
				new Vector3(0, 500, 0);
		//Debug.Log (o.GetComponent<projecitile>().initialSpeed);
	}
}