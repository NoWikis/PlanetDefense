using UnityEngine;
using System.Collections;

public class level1_script : MonoBehaviour {

	level_manager manager;
	bool spawned = false;
	public GameObject Planet;

	void Awake() {
		manager = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<level_manager>();
		manager.level1_boss = true;
		spawned = true;
		Planet = GameObject.FindGameObjectWithTag ("Planet");

	}

	void Update() {
		if (spawned) {
			spawned = false;
			manager.level1_boss_pos = transform.position;
		}
	}

	void OnCollisionEnter(Collision c){
		//		print ("YO");
		if (c.gameObject.CompareTag ("Fake_Player")) {
			Destroy(c.gameObject);
		}

		if (c.gameObject.CompareTag ("mars")) {
			Destroy(c.gameObject);
			manager.level1_boss = false;
			manager.planet_pos = Planet.transform.position;
			manager.level1_scene_done = true;
		}
	}
}
