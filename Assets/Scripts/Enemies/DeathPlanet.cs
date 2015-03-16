using UnityEngine;
using System.Collections;

public class DeathPlanet : MonoBehaviour {

	public GameObject		homePlanet;
	public float 			speed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		MovePlanet ();
	}

	void MovePlanet () {
		var planet = homePlanet.GetComponent<Transform>();
		Vector3 movement = planet.transform.position - transform.position; 
		planet.transform.position = planet.transform.position + movement*speed*0.005f;
//		transform.parent.GetComponent<Transform>().transform.position = planet.transform.position;

		//planetPos = transform.parent.GetComponent<Transform> ().transform.position;
		
	}
}
