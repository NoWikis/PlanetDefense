using UnityEngine;
using System.Collections;

public class AsteroidSpawner : MonoBehaviour {

	public float spawnCycle = 1f;
	public float[] masses;
	public GameObject asteroidPrefab;


	float time = 0;





	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		if (time > spawnCycle) {
			GameObject o = (GameObject)Instantiate (asteroidPrefab);
			o.transform.position = transform.position;
			o.GetComponent<AsteroidBehavior>().mass = getRandomMass();
			o.GetComponent<AsteroidPhysics>().initialVelocity = new Vector3(
				Random.value*3 - 1.5f, Random.value*3 - 1.5f, 0);
			time = 0f;
		}
	}














	float getRandomMass() {
		if (masses.Length == 0) return 100f;

		return masses[(int) Random.Range (0, masses.Length)];

	}
}
