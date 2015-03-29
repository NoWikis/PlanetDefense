using UnityEngine;
using System.Collections;

public class ShieldedShip : MonoBehaviour {

	const float ENDED = 0f;
	const float DORMANT = -100f;

	public int numShipsPerWave = 2;
	public float spawnCycle	= 10f;
	




	public GameObject turret;
	float showHurtDuration = .08f;
	Health health;
	SpriteRenderer sprite;

	int count = 0;
	float showHurtTime = DORMANT;
	float spawnTime = DORMANT;
	Color origColor;



	// Use this for initialization
	void Start () {
		health = GetComponent<Health> ();
		health.init (100, 100);
		health.registerDamageCallback (showHurt);
		sprite = transform.parent.GetComponentInChildren<SpriteRenderer> ();

	}
	
	// Update is called once per frame
	void Update () {
		updateHurt ();
		updateSpawn ();
	}



	void showHurt(GameObject o) {
		if (isActive (showHurtTime))
						return;

		origColor = sprite.material.color;
		sprite.material.color = new Color (255, 0, 0, 255);
		showHurtTime = showHurtDuration;
		Debug.Log ("showing hurt");
	}


	void updateHurt() {
		if (isActive(showHurtTime)) {
			showHurtTime -= Time.deltaTime;
			if (isExpired(showHurtTime)) {
				showHurtTime = DORMANT;
				sprite.material.color = origColor;
				Debug.Log ("revertingColor");
			}
		}

		if (health.isDead ()) {
			Destroy(transform.parent.gameObject);
		}


	}

	void updateSpawn() {
		if (isExpired(spawnTime)) {
			SpawnShips ();
			spawnTime = spawnCycle;
		} 
		spawnTime -= Time.deltaTime;
	}

	void SpawnShips() {
		GameObject o = (GameObject)Instantiate (turret);
		o.transform.position = transform.position;
	}


	/// timing
	bool isActive(float time) {
		return time > DORMANT;
	}

	bool isExpired(float time) {
		return time < 0f;
	}	





}
