using UnityEngine;
using System.Collections;

public class SmallCometSpawner : MonoBehaviour {

	public GameObject comet;

	float time = 0f;
	public float spawnAverage;

	private float nextSpawn;

	public float enableSmallComet;

	// Use this for initialization
	void Start () {
		nextSpawn = Random.Range (spawnAverage-5f,spawnAverage);
	}
	
	// Update is called once per frame
	void Update () {
		if (enableSmallComet) {
			time += Time.deltaTime;
			if (time > nextSpawn) {
				GameObject o = (GameObject)Instantiate (comet);
				o.transform.position = new Vector3 ((Random.value>.5f?-1:1)*Random.Range (80f,70f) + transform.position.x,
				                                    (Random.value>.5f?-1:1)*Random.Range (80f,70f) + transform.position.y, 0);
				time = 0f;
				nextSpawn = Random.Range (spawnAverage-5f,spawnAverage);
				Debug.Log(nextSpawn);
			}
		}
		//Set condition here if you want to turn on smallComet spawn
	}
}
