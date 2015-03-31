using UnityEngine;
using System.Collections;

public class AsteroidSpawner : MonoBehaviour {

	public float spawnCycle = 1f;
	public bool spawnInitially = false;
	public float[] masses;
	public GameObject asteroidType1;
	public GameObject asteroidType2;

	public Vector3 DistanceSource;
	public GameObject Planet;

	public float maxDistnace;

	float time = 0;





	// Use this for initialization
	void Start () {
		Planet = GameObject.Find ("planet");
		if (spawnInitially) {
			spawn ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance(Planet.transform.position, DistanceSource) < maxDistnace) {
		time += Time.deltaTime;
			if (time > spawnCycle) {
				spawn();
			}
		}
	}

	void spawn() {
		GameObject o = (GameObject)Instantiate (Random.value>.1f?asteroidType1 : asteroidType2);
		o.transform.position = transform.position;
		o.GetComponent<AsteroidBehavior>().setSizeClass(getRandomSize());
		
		//o.GetComponent<AsteroidPhysics>().initialVelocity = new Vector3(
		//	Random.value*3 - 1.5f, Random.value*3 - 1.5f, 0);
		o.GetComponent<AsteroidPhysics> ().initialVelocity = 
			(Planet.transform.position - transform.position).normalized * Random.value * 1.5f;
		time = 0f;
	}














	AsteroidBehavior.SizeClass getRandomSize() {
		int val = Random.Range (0, (int)AsteroidBehavior.SizeClass.Larger+1);
		switch(val) {
				case (0): return AsteroidBehavior.SizeClass.Small;
				case (1): return AsteroidBehavior.SizeClass.Medium;
				case (2): return AsteroidBehavior.SizeClass.Large;
				case (3): return AsteroidBehavior.SizeClass.Larger;

		}
		return AsteroidBehavior.SizeClass.Medium;

	}
}
