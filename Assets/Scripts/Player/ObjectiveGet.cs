using UnityEngine;
using System.Collections;

public class ObjectiveGet : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Planet")
			Application.LoadLevel ("Congrats");
	}
}
