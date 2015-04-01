using UnityEngine;
using System.Collections;

public class boss_ship : MonoBehaviour {

	public GameObject planet;
	public float speed;

	Vector3 planet_pos;

	public float spawn;
	public bool fire;

	public float hp;


	// Use this for initialization
	void Start () {
		spawn = 0;
		fire = true;
	}
	
	// Update is called once per frame
	void Update () {

		planet_pos.x = planet.transform.position.x;
		planet_pos.y = transform.position.y;
		planet_pos.z = transform.position.z;

		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, planet_pos, step);

		if (spawn%4 == 0) {
			fire = true;
		}

		else {
			fire = false;
		}

		if(hp <= 0){
			Application.LoadLevel("Congrats");
		}
	
	}



}
