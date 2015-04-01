using UnityEngine;
using System.Collections;

public class miner_spawner : MonoBehaviour {
	
	public float spawnCycle = 1f;

	public GameObject miner;
	
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
			
			GameObject o = (GameObject)Instantiate (miner);
			o.transform.position = transform.position;
			
			Vector3 planet_pos = GameObject.FindGameObjectWithTag ("Planet").transform.position;
			
			time = 0f;			
			
		}
	}
	
	
}
