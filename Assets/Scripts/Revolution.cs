using UnityEngine;
using System.Collections;

public class Revolution : MonoBehaviour {

	public float speedRotate;
	public float speedMoving;

	private Vector3 planetPos;
	private float planetDistance;


	// Use this for initialization
	void Start () {
		//find planet position once spawned
		planetPos = GameObject.Find ("planet").transform.position;


	}
	
	// Update is called once per frame
	void Update () {

		transform.RotateAround(Vector3.zero, Vector3.forward, speedRotate);
		//else
			//timer += Time.deltaTime;


	}
}
