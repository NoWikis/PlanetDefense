using UnityEngine;
using System.Collections;

public class level2_fake_player : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision c){
		if (c.gameObject.CompareTag ("explosion")) {
			Destroy(gameObject);
		}
	}
}
