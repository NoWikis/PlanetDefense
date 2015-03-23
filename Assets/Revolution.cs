using UnityEngine;
using System.Collections;

public class Revolution : MonoBehaviour {

	public float speed;
	public float timer = 0f;
	public float limit = 2f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.RotateAround(Vector3.zero, Vector3.forward, speed);
		if (timer > limit)	
			speed = 0.1f;
		else
			timer += Time.deltaTime;


	}
}
