using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Radar : MonoBehaviour {

	public float range = 100f;
	public float blipRate = 1f;


	public GameObject BlipPrefab;



	/* What tags should be targeted */

	string[] tags = {
		"Asteroid_P1",
		"Asteroid_P2",
		"Comet"

	};


	List<GameObject> blips = new List<GameObject>();
	List<GameObject> blipObjects = new List<GameObject>();
	float time;
	GameObject planet;

	// Use this for initialization
	void Start () {
		time = 0f;
		planet = GameObject.FindGameObjectWithTag ("Planet");
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		if (time >= blipRate) {
			generateBlips();
			time = 0;
		}
	}

	void generateBlips() {
		gatherObjects ();
		foreach (GameObject obj in blipObjects) {
			Vector3 normalizedPos = new Vector3(
				(planet.transform.position.x - obj.transform.position.x) / range,
				(planet.transform.position.y - obj.transform.position.y) / range,
				(planet.transform.position.x - obj.transform.position.z) / range);

			// if too far, discard
			if (normalizedPos.magnitude > 1f) continue;

			GameObject newBlip = (GameObject) Instantiate(BlipPrefab);
			newBlip.transform.localScale = obj.transform.localScale;
			newBlip.transform.position = normalizedPos + transform.position;
			newBlip.transform.parent = this.gameObject.transform;
		}
		

	}


	// Get a list of all relevant objects (blips);
	void gatherObjects() {
		blipObjects.Clear ();
		foreach(string tag in tags) {
			GameObject[] objs = GameObject.FindGameObjectsWithTag(tag);
			foreach(GameObject o in objs) {
				blipObjects.Add (o);
			}
		}
	}
}
