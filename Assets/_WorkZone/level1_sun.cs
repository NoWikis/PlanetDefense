using UnityEngine;
using System.Collections;

public class level1_sun : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider c){
		if (c.gameObject.CompareTag ("Planet")) {
			c.gameObject.GetComponent<Health>().takeDamage(100);
		}
	}
}
