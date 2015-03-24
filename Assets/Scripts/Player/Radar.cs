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
		"Comet",
		"starFighter"

	};


	List<GameObject> blips = new List<GameObject>();
	List<GameObject> blipObjects = new List<GameObject>();
	float time;
	GameObject planet;
	GameObject dial;

	// Use this for initialization
	void Start () {
		time = blipRate;
		planet = GameObject.FindGameObjectWithTag ("Planet");
		dial = GameObject.FindGameObjectWithTag ("RadarDial");
	}
	
	// Update is called once per frame
	void Update () {
		time -= Time.deltaTime;
		if (time <= 0) {
			generateBlips();
			time = blipRate;
		}


		// Have the blip look like it is when the dial reaches the top
		dial.transform.localRotation = Quaternion.Euler (0, 0, 90 + (360 * (time / blipRate)));

	}

	void generateBlips() {
		gatherObjects ();
		foreach (GameObject obj in blipObjects) {
			Vector3 normalizedPos = new Vector3(
				(-planet.transform.position.x + obj.transform.position.x) / range,
				(-planet.transform.position.y + obj.transform.position.y) / range,
				(-planet.transform.position.x + obj.transform.position.z) / range);

			// if too far, discard
			if (normalizedPos.magnitude > 1f) continue;

			GameObject newBlip = (GameObject) Instantiate(BlipPrefab);
			newBlip.transform.localScale = obj.transform.lossyScale;
			newBlip.transform.position = (new Vector3(
						normalizedPos.x * transform.lossyScale.x,
						normalizedPos.y * transform.lossyScale.y,
						normalizedPos.z * transform.lossyScale.z)


			                              ) + transform.position + new Vector3(0, 0, -1);
			newBlip.transform.parent = this.gameObject.transform;
		}
		//GameObject centerBlip = (GameObject) Instantiate(BlipPrefab);
		//centerBlip.transform.localScale = new Vector3 (5, 5, 5);
		//centerBlip.transform.position = transform.position + new Vector3(0, 0, -1);
		//centerBlip.transform.parent = transform;


		

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
