using UnityEngine;
using System.Collections;

public class missile_spawn : MonoBehaviour {

	public float spawnCycle = 1f;
	public float spawnTime = 5f;

	public GameObject Planet;
	public GameObject boss;
	public GameObject missile;

	public float spawn;
	public float spawn_amount = 4f;

	public float offset;


	// Use this for initialization
	void Start () {
		Planet = GameObject.Find ("planet");
		boss = GameObject.Find ("boss");
		spawnCycle = spawnTime;
		//spawn = 0;
	}
	
	// Update is called once per frame
	void Update () {

		//spawn = boss.GetComponent<boss_ship> ().spawn;

		spawnCycle -= Time.deltaTime;
		if(spawnCycle <= 0){
			GameObject o = (GameObject)Instantiate(missile);
			o.transform.position = transform.position;
			o.GetComponent<missile>().offset = offset;
			o.GetComponent<missile>().spawner = gameObject;
			spawnCycle = 999f;
			//boss.GetComponent<boss_ship> ().spawn++;

		}


	}
}
