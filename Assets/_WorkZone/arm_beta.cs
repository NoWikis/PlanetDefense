using UnityEngine;
using System.Collections;

public class arm_beta : MonoBehaviour {


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		float deltaY = transform.position.y - -9f;
		float deltaX = transform.position.x - -4f;

		float angle = Mathf.Atan2(deltaY, 0) * Mathf.Rad2Deg;
		transform.Rotate(0, 0, 0);


	}
}
