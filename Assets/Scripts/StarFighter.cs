using UnityEngine;
using System.Collections;

public class StarFighter : MonoBehaviour {
	
	public float speedRotate;
	public float speedMoving;

	private float planetAngle;
	private Vector3 planetPos;
	private float planetDistance;
	
	
	// Use this for initialization
	void Start () {
		planetPos = GameObject.Find ("planet").transform.position;
		planetAngle = Util.getAngleVector (transform.position, planetPos) + 270;
		planetDistance = Vector3.Distance (planetPos, transform.position);
		transform.eulerAngles = new Vector3 (0, 0, planetAngle);
		Debug.Log (planetAngle);
	}
	
	// Update is called once per frame
	void Update () {

		//If they don't reach the planet move towards it
		if (planetDistance > 20) {
			transform.position = Vector3.MoveTowards(transform.position,planetPos,Time.deltaTime*speedMoving);

		}
		else {
			transform.RotateAround(Vector3.zero, Vector3.forward, speedRotate);
		}

		planetPos = GameObject.Find ("planet").transform.position;
		planetDistance = Vector3.Distance (planetPos, this.transform.position);
		
		
	}
}
