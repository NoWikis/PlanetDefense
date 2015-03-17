using UnityEngine;
using System.Collections;

public class level_manager : MonoBehaviour {

	public bool level1_boss = false;
	public bool level1_scene_done = false;
	public Vector3 level1_boss_pos;
	public Vector3 planet_pos;
	public float speed;
	public GameObject Planet;

	public bool level2_scene_done = false;
	public float load_level_cd;
	public float load_level_delay = 3f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(level1_boss){
			float step = speed * Time.deltaTime * 4;
			level1_boss_pos.z = -10;
			level1_boss_pos.y = 180f;
			transform.position = Vector3.MoveTowards(transform.position, level1_boss_pos, step);
		}

		if(level1_scene_done){
			float step = speed * Time.deltaTime * 4;
			planet_pos.z = -10;
			transform.position = Vector3.MoveTowards(transform.position, planet_pos, step);
			if(transform.position.x == Planet.transform.position.x && transform.position.y == Planet.transform.position.y){
				level1_scene_done = false;
			}
		}

		if(level2_scene_done) {
			load_level_cd -= Time.deltaTime;

			if(load_level_cd <= 0)
				Application.LoadLevel("Title");
		}

	}

	void OnCollisionEnter(Collision c){
		if (c.gameObject.CompareTag ("Fake_Player")) {
			Destroy(c.gameObject);
		}

		if (c.gameObject.CompareTag ("mars")) {
			Destroy(c.gameObject);
			level2_scene_done = true;
			load_level_cd = load_level_delay;
		}
	}
}
