using UnityEngine;
using System.Collections;

public class miner : MonoBehaviour {

	public GameObject Planet;
	public GameObject spawner;
	public GameObject mines;

	public float x_diff = 47f;
	public float speed;

	Vector3 position;
	bool move_up = false;
	float delay = 2f;
	float delay_init = 2f;

	float y_diff = 10f;

	
	// Use this for initialization
	void Awake () {
		delay = delay_init;
		y_diff = 0f;
	}
	
	// Update is called once per frame
	void Update () {

		if(!move_up){
			float step = speed * Time.deltaTime * 5;
			position.x = Planet.transform.position.x + x_diff;
			//position.y = Planet.transform.position.y;
			position.z = Planet.transform.position.z;
			transform.position = Vector3.MoveTowards(transform.position, position, step);
			delay -= Time.deltaTime;
			if(delay <= 0){
				move_up = true;
				delay = delay_init;
			}
		}

		else {
			float step = speed * Time.deltaTime * 5;
			position.x = Planet.transform.position.x + x_diff;

			position.y = Planet.transform.position.y + y_diff;
			position.z = Planet.transform.position.z;
			transform.position = Vector3.MoveTowards(transform.position, position, step);
		}

	}

	void shootMine() {
		GameObject o = (GameObject) Instantiate (mines);
		o.transform.position = transform.position;
		o.GetComponent<Transform>().eulerAngles = new Vector3(0,0,transform.eulerAngles.z-90f);
		o.GetComponent<Mine>().initialSpeed = 
			Quaternion.Euler (0, 0, Util.getAngleVector(
				GameObject.FindGameObjectWithTag("Planet").transform.position, transform.position
				)  + 270) * 
				new Vector3(0, 200, 0);
		//sound_basic.Play ();
	}
}
