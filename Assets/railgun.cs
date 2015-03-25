using UnityEngine;
using System.Collections;

public class railgun : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 expanded = transform.localScale;
		expanded.x += 0.1f;
		transform.localScale = expanded;
		Vector3 movement = transform.localPosition;
		movement.x += 0.1f;
		transform.localPosition = movement;
	}
}
