using UnityEngine;
using System.Collections;

public class StarFighterSpawner : MonoBehaviour {
	
	public GameObject StarFighter;
	
	float time = 0f;
	public float spawnAverage;
	
	private float nextSpawn;
	
	public bool enableStarFighter;

	public int childCount = 0f;
	
	// Use this for initialization
	void Start () {
		nextSpawn = Random.Range (spawnAverage-2.5f,spawnAverage);
	}
	
	// Update is called once per frame
	void Update () {
		if (enableStarFighter) {
			time += Time.deltaTime;
			if (time > nextSpawn) {
				if (transform.childCount == 0) {
					GameObject o = (GameObject)Instantiate (StarFighter);
					float randomX = (Random.value>.5f?-1:1)*Random.Range (300f,350f);
					float randomY = (Random.value>.5f?-1:1)*Random.Range (300f,350f);
					o.transform.position = new Vector3 (randomX + transform.position.x,
					                                    randomY + transform.position.y, 0);

					o = (GameObject)Instantiate (StarFighter);
					o.transform.position = new Vector3 (randomX + 30f + transform.position.x,
					                                    randomY + transform.position.y, 0);
					o = (GameObject)Instantiate (StarFighter);
					o.transform.position = new Vector3 (randomX + 60f + transform.position.x,
					                                    randomY + transform.position.y, 0);
					childCount = 3;
				}
				time = 0f;
				nextSpawn = Random.Range (spawnAverage-2.5f,spawnAverage);
			}
		}
		//Set condition here if you want to turn on smallComet spawn
	}
}
