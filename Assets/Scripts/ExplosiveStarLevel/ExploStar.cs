using UnityEngine;
using System.Collections;

public class ExploStar : MonoBehaviour {
	public Transform from;
	public Transform target;
	public float speed;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
//		float currentAngle = transform.rotation.eulerAngles.y;
//		transform.rotation = Quaternion.AngleAxis (currentAngle + (Time.deltaTime * speed), Vector3.up);
//	transform.RotateAround (collider.bounds.center,Vector3.up, speed);
//		transform.Rotate (Vector3.forward* Time.deltaTime);
//		transform.rotation.eulerAngles.y += Time.deltaTime;
//		transform.Rotate (0, Time.deltaTime, 0);
//		transform.eulerAngles += new Vector3 (0, Time.deltaTime, 0);
//		transform.Rotate (new Vector3(0,0.9f,0)* Time.deltaTime);
		//transform.rotation = Quaternion.Lerp(from.rotation, to.rotation, speed * Time.time);
//		float step = speed * Time.deltaTime;
//		transform.rotation = Quaternion.RotateTowards(transform.rotation, target.rotation, step);
	}

	void OnCollisionEnter(Collision c){
		if (c.collider.CompareTag ("Asteroid_P1") || c.collider.CompareTag("Asteroid_P2")) {
			Destroy(c.gameObject);
		}
	}
}
