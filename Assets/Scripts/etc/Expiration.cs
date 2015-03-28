using UnityEngine;
using System.Collections;


// After a set time limit, the object is destroyed
public class Expiration : MonoBehaviour {
	
	public float timeLimit = .6f; // in seconds
	float time = 0;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		if (time > timeLimit) Destroy (this.gameObject);
	}
}
