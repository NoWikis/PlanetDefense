using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class level1_script : MonoBehaviour {

	level_manager manager;
	bool spawned = false;
	public GameObject Planet;

	Image hp;

	void Awake() {
		manager = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<level_manager>();
		spawned = true;
		Planet = GameObject.FindGameObjectWithTag ("Planet");

	}

	void Start() {
		manager.level1_boss_pos = transform.position;
		manager.level1_boss_pos.x -= 32.2f;
		manager.level1_boss = true;
		hp = GameObject.Find ("HP").GetComponent<Image>();
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
			hp.fillAmount = 0f;
		}

		if (c.gameObject.CompareTag ("end_lvl1") || c.gameObject.CompareTag ("explosion")) {
			Application.LoadLevel("Level_2_Intro");
		}
	}
}
