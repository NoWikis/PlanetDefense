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

	void Start() {
		manager.level1_boss_pos = transform.position;
	}

	void Update() {
		if (spawned) {
			spawned = false;
			//
		}
	}

	void OnTriggerEnter(Collider c){
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

		if(c.gameObject.CompareTag ("Proj_P1") || c.gameObject.CompareTag ("Proj_P2")) {
			Destroy(c.gameObject);
		}

		if (c.gameObject.CompareTag ("Planet")) {
			Application.LoadLevel("End");
		}

		if (c.gameObject.CompareTag ("end_lvl1")) {
			Application.LoadLevel("Level_2_Intro");
		}
	}
}
