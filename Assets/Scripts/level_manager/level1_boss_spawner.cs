using UnityEngine;
using System.Collections;

public class level1_boss_spawner : MonoBehaviour {
	
	public float spawnCycle = 1f;
	public float[] masses;
	public GameObject asteroidType1;

	public GameObject mars;
	public GameObject player1;
	public GameObject player2;
	
	float time = 0;
	
	
	bool spawn = true;
	
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		if (time > spawnCycle && spawn) {

			spawn = false;

			GameObject o = (GameObject)Instantiate (asteroidType1);
			o.transform.position = transform.position;
			//o.GetComponent<AsteroidBehavior>().setSizeClass(AsteroidBehavior.SizeClass.Larger);

			Vector3 planet_pos = GameObject.FindGameObjectWithTag ("Planet").transform.position;

			Vector3 towards = new Vector3(0,0,0);

			towards.x = planet_pos.x - transform.position.x;
			towards.y = planet_pos.y - transform.position.y;

			if(towards.x != 0){
				towards.x = towards.x/towards.x;
			}

			if(towards.y != 0){
				towards.y = towards.y/towards.y;
			}

			//o.GetComponent<AsteroidPhysics>().initialVelocity = new Vector3(
			//	towards.x, towards.y, 0);
			time = 0f;




			//take care of planet mars
			GameObject mars_obj = (GameObject)Instantiate (mars);

			Vector3 mars_pos = transform.position;

			mars_pos.y -= 60f;



			mars_obj.transform.position = mars_pos;




		}
	}
	

}
