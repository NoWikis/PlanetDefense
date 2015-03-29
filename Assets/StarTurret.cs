using UnityEngine;
using System.Collections;

public class StarTurret : MonoBehaviour {
	
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

	//Timer for shifting around
	public float shiftAvg;
	private float shiftTimer;
	private float timeTilShift;

	public float movementAvg;
	private float movementTimer;
	private float timeTilStop;
	private float direction;

	public float item_spawn_chance = 1f;
	public GameObject[] item_list;
	
	// Use this for initialization
	void Start () {
		planetPos = GameObject.Find ("planet").transform.position;
		planetAngle = Util.getAngleVector (transform.position, planetPos) + 270;
		planetDistance = Vector3.Distance (planetPos, transform.position);
		transform.eulerAngles = new Vector3 (0, 0, planetAngle);
		Debug.Log (planetAngle);
		timeTilShift = shiftAvg;
		timeTilStop = movementAvg;
		if (Random.Range(0f,1f) > 0.5f) {
			direction = 1;
		}
		else
			direction = -1;
	}
	
	// Update is called once per frame
	void Update () {
		relativeDistance = transform.position - planetPos;
		planetPos = GameObject.Find ("planet").transform.position;
		//If they don't reach the planet move towards it
	}
	
	void LateUpdate () {
		if (planetDistance > 20) {
			transform.position = Vector3.MoveTowards(transform.position,planetPos,Time.deltaTime*speedMoving);
			planetAngle = Util.getAngleVector (transform.position, planetPos) + 270;
			transform.eulerAngles = new Vector3 (0, 0, planetAngle);
			planetDistance = Vector3.Distance (planetPos, this.transform.position);
		}
		else {
			transform.position = planetPos + relativeDistance;
			relativeDistance = transform.position - planetPos;
			planetAngle = Util.getAngleVector (transform.position, planetPos) + 270f ;
			transform.eulerAngles = new Vector3 (0, 0, planetAngle);
			Maneuvering();
		}
		if (Health < 1) {
			int spawn_item = Mathf.RoundToInt(Random.value * (item_list.Length - 1));
			float spawn_chance = Random.value;
			if(spawn_chance <= item_spawn_chance){
				GameObject o = (GameObject)Instantiate (item_list[spawn_item]);
				o.transform.position = transform.position;
			}
			GameObject.Find("StarFighterSpawnerPrefab").GetComponent<StarFighterSpawner>().childCount--;
			Destroy(this.gameObject);
		}
		
	}

	void Maneuvering() {
		if (shiftTimer > timeTilShift) {
			Movement();
		}
		else
			shiftTimer += Time.deltaTime;

	}

	void Movement() {
		if (movementTimer < timeTilStop) {
			transform.RotateAround(planetPos, Vector3.forward, direction*speedRotate);
			movementTimer += Time.deltaTime;
			Debug.Log (movementTimer);
		}
		else {
			if (Random.Range(0f,1f) > 0.4f) {
				shootProjecitile();
			}

			movementTimer = 0f;
			shiftTimer = 0f;
			timeTilStop = Random.Range (movementAvg - 0.1f, movementAvg + 0.1f);
			timeTilShift = Random.Range (shiftAvg - 2.0f, shiftAvg + 1.0f);
			if (Random.Range(0,1) > 0.5) {
				direction = 1;
			}
			else
				direction = -1;
		}

	}
	
	void shootProjecitile() {
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