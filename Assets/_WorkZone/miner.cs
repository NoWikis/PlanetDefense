using UnityEngine;
using System.Collections;

public class miner : MonoBehaviour {

	public GameObject Planet;
	public GameObject spawner;
	public GameObject mines;

	public float x_diff = 47f;
	public float speed;

	Vector3 position;

	public float delay = 2f;
	float delay_init = 2f;

	public float y_diff = 10f;

	bool up = true;

	public float shoot_chance;

	public float hp;

	bool start_follow = false;
	
	// Use this for initialization
	void Awake () {
		delay = delay_init;
		y_diff = 0f;
	}
	
	// Update is called once per frame
	void Update () {

		if(Planet.transform.position.x > 440f)
			start_follow = true;

		if(start_follow){
			float step = speed * Time.deltaTime * 5;
			position.x = Planet.transform.position.x + x_diff;
			position.y = Planet.transform.position.y + y_diff;
			position.z = Planet.transform.position.z;

			delay -= Time.deltaTime;

			if((transform.position.y <= Planet.transform.position.y + y_diff + 0.1f && 
			   transform.position.y >= Planet.transform.position.y + y_diff - 0.1f ) && 
			   delay <= 0) {
				if(y_diff == 30f)
					up = false;
				if(y_diff == -30f)
					up = true;
				
				if(up == true)
					y_diff += 10f;
				else
					y_diff -= 10f;
				
				delay = delay_init;

				shootMine();
			}

			transform.position = Vector3.MoveTowards(transform.position, position, step);

			if(hp <= 0)
				Destroy(gameObject);
		}
	}

	void shootMine() {
		float spawn_chance = Random.value;

		if(spawn_chance < shoot_chance){
			GameObject o = (GameObject) Instantiate (mines);
			Vector3 mine_pos = transform.position;
			mine_pos.x -= 5f;
			o.transform.position = mine_pos;
			Vector2 temp_vector = new Vector2 (-400f, 0);
			//o.GetComponent<Mine> ().initialSpeed = temp_vector;
			//sound_basic.Play ();
		}
	}

//	void OnTriggerEnter(Collider other) {
//		//Debug.Log ("Mine Collision");
//		if (other.gameObject.tag == "Proj_P1" || other.gameObject.tag == "Proj_P2") {
//			hp--;
//		}
//	}
}
