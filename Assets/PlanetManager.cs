using UnityEngine;
using System.Collections;

public class PlanetManager : MonoBehaviour {

	static Vector3 planetPosition;

	static public float getAngleVector(Vector3 destination) {	
		Vector2 a = destination - planetPosition;
		
		float angle = Vector2.Angle (new Vector3(-1, 0, 0), a.normalized);
		
		if (a.y > 0) {
			angle *= -1;
		}
		
		
		Debug.DrawLine (Vector3.zero, a.normalized);
		Debug.DrawLine (Vector3.zero, new Vector3(1, 0, 0));
		return angle;
	}
	// Use this for initialization
	void Start () {
		planetPosition = transform.position;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
