using UnityEngine;
using System.Collections;

public class AsteroidSpawner : MonoBehaviour {

	public float spawnCycle = 1f;
	public float[] masses;
	public GameObject asteroidType1;
	public GameObject asteroidType2;

	float time = 0;





	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		if (time > spawnCycle) {
			GameObject o = (GameObject)Instantiate (Random.value>.5f?asteroidType1 : asteroidType2);
			o.transform.position = transform.position;
			o.GetComponent<AsteroidBehavior>().setSizeClass(getRandomSize());

			o.GetComponent<AsteroidPhysics>().initialVelocity = new Vector3(
				Random.value*3 - 1.5f, Random.value*3 - 1.5f, 0);
			time = 0f;
		}
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
