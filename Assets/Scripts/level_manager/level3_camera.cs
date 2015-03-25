using UnityEngine;
using System.Collections;

public class level3_camera : MonoBehaviour {

	public float scene_start = 1f;
	public GameObject Planet;
	public GameObject Jupiter;

	public bool scene_playing = false;
	public float speed;

	Vector3 planet_pos;
	Vector3 jupiter_pos;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!scene_playing || Jupiter == null) {
			float step = speed * Time.deltaTime * 5;
			planet_pos.x = Planet.transform.position.x;
			planet_pos.y = Planet.transform.position.y;
			planet_pos.z = -10;
			transform.position = Vector3.MoveTowards(transform.position, planet_pos, step);
		}

		scene_start -= Time.deltaTime;

		if(scene_start <= 0)
			scene_playing = true;

		if(Jupiter != null){
			if(scene_playing){
				float step = speed * Time.deltaTime * 5;
				jupiter_pos.x = Jupiter.transform.position.x;
				jupiter_pos.y = Jupiter.transform.position.y;
				jupiter_pos.z = -10;
				transform.position = Vector3.MoveTowards(transform.position, jupiter_pos, step);
			}
		}
	}
}
