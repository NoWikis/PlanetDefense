using UnityEngine;
using System.Collections;

public class StarFighter : MonoBehaviour {

	public GameObject projectile;

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
		planetPos = GameObject.Find ("planet").transform.position;
		planetDistance = Vector3.Distance (planetPos, this.transform.position);
		//If they don't reach the planet move towards it
		if (planetDistance > 20) {
			transform.RotateAround(Vector3.zero, Vector3.forward, 0.1f);
			transform.position = Vector3.MoveTowards(transform.position,planetPos,Time.deltaTime*speedMoving);
			planetAngle = Util.getAngleVector (transform.position, planetPos) + 270;
			transform.eulerAngles = new Vector3 (0, 0, planetAngle);
			
		}
		else {
			transform.RotateAround(Vector3.zero, Vector3.forward, speedRotate);
			planetAngle = Util.getAngleVector (transform.position, planetPos) + 180;
			transform.eulerAngles = new Vector3 (0, 0, planetAngle);
		}



	}

	void shootProjecitile() {
		GameObject o = (GameObject) Instantiate (projectile);
		o.transform.position = transform.position;
		o.GetComponent<Projectile>().initialSpeed = 
			Quaternion.Euler (0, 0, Util.getAngleVector(
				GameObject.FindGameObjectWithTag("Planet").transform.position, transform.position
				)  + 270) * 
				new Vector3(0, 1000, 0);
		//Debug.Log (o.GetComponent<projecitile>().initialSpeed);
	}
}
