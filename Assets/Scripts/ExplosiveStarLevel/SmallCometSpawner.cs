using UnityEngine;
using System.Collections;

public class SmallCometSpawner : MonoBehaviour {

	public GameObject comet;

	float time = 0f;
	float nextSpawn;

	// Use this for initialization
	void Start () {
		nextSpawn = Random.Range (5f,5f);
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		if (time > nextSpawn) {
			GameObject o = (GameObject)Instantiate (comet);
			o.transform.position = new Vector3 ((Random.value>.5f?-1:1)*Random.Range (40f,30f) + transform.position.x,
			                                    (Random.value>.5f?-1:1)*Random.Range (40f,30f) + transform.position.y, 0);
			time = 0f;
			nextSpawn = Random.Range (5f,5f);
		}
	}
}
