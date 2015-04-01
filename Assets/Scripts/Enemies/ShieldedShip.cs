using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ShieldedShip : MonoBehaviour {

	const float ENDED = 0f;
	const float DORMANT = -100f;

	public int numShipsPerWave = 2;
	public int numMaxShips = 2;
	public float spawnCycle	= 10f;
	public float healthAmount = 100;
	public float speed = .02f;
	public float rotationalRate = .02f;
	public float closingDistance = 10;
	public float minimumDistance = 110;

	




	public GameObject turret;
	float showHurtDuration = .08f;
	Health health;
	SpriteRenderer sprite;

	int count = 0;
	float showHurtTime = DORMANT;
	float spawnTime = DORMANT;
	Color origColor;
	GameObject planetRef;
	GameObject shipParent;
	List<WeakReference> children = new List<WeakReference>();


	// Use this for initialization
	void Start () {
		health = GetComponent<Health> ();
		health.init (healthAmount, healthAmount);
		health.registerDamageCallback (showHurt);
		sprite = transform.parent.GetComponentInChildren<SpriteRenderer> ();
		planetRef = GameObject.FindGameObjectWithTag("Planet");
		shipParent = transform.parent.gameObject;

		shipParent.transform.rotation = 
			Quaternion.Euler(0, 0, Util.getAngleVector(planetRef.transform.position, shipParent.transform.position) - 90); 
	}
	
	// Update is called once per frame
	void Update () {
		updateHurt ();

	}

	void FixedUpdate() {
		if (Vector3.Distance (planetRef.transform.position, transform.position) > minimumDistance)
			return;
		updateSpawn ();
		updateMovement();
		updateChildren ();
	}



















	void updateHurt() {
		if (isActive(showHurtTime)) {
			showHurtTime -= Time.deltaTime;
			if (isExpired(showHurtTime)) {
				showHurtTime = DORMANT;
				sprite.material.color = origColor;

			}
		}

		if (health.isDead ()) {	
			Destroy(shipParent);
		}


	}

	void updateSpawn() {
		if (isExpired(spawnTime)) {
			for(int i = 0; i < numShipsPerWave; ++i)
				SpawnShips ();
			spawnTime = spawnCycle;
		} 
		spawnTime -= Time.deltaTime;
	}

	void updateMovement() {
		if (Vector3.Distance(
				shipParent.transform.position,
				planetRef.transform.position
			) > closingDistance) {
			hoverTowards(planetRef.transform.position);
		}
	}

	void updateChildren() {
		for(int i = 0; i < children.Count; ++i) {
			if (!children[i].IsAlive) {
				children.Remove(children[i]);
				--i;
			}
		}
	}




	
	void showHurt(GameObject o) {
		if (isActive (showHurtTime))
			return;
		
		origColor = sprite.material.color;
		sprite.material.color = new Color (255, 0, 0, 255);
		showHurtTime = showHurtDuration;
	}

	void SpawnShips() {
		Debug.Log (turret.tag);
	
		if (children.Count > numMaxShips)
						return;

		GameObject o = (GameObject)Instantiate (turret);
		o.transform.position = shipParent.transform.position;
		children.Add (new WeakReference(o.gameObject));

	}


	void hoverTowards(Vector3 target) {
		Vector3 delta = target - shipParent.transform.position;
		delta.Normalize();
		delta = delta * speed;
		shipParent.transform.position = shipParent.transform.position + delta;


		float properRotation = Util.getAngleVector(transform.position, target) + 90;
		if (properRotation < 0) properRotation += 360; 



		if (shipParent.transform.eulerAngles.z < properRotation) {
			shipParent.transform.rotation = Quaternion.Euler(0, 0, shipParent.transform.eulerAngles.z + rotationalRate);
		} else {
			shipParent.transform.rotation = Quaternion.Euler(0, 0, shipParent.transform.eulerAngles.z - rotationalRate);
		}

	}


	/// timing
	bool isActive(float time) {
		return time > DORMANT;
	}

	bool isExpired(float time) {
		return time < 0f;
	}	





}
