using UnityEngine;
using System.Collections;

public class ShieldedShip : MonoBehaviour {

	public int numShipsPerWave = 2;
	public float spawnCycle	= 10f;
	




	public GameObject turret;
	Health health;

	int count = 0;


	// Use this for initialization
	void Start () {
		health = GetComponent<Health> ();
		health.init (100, 100);
	}
	
	// Update is called once per frame
	void Update () {
	
	}



	void SpawnShips() {


	}





}
