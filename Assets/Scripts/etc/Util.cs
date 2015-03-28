using UnityEngine;
using System.Collections;


// Class for generic utility functions
public class Util : MonoBehaviour {




	// Gets  the degree of rotation between two points on the XY plane
	static public float getAngleVector(Vector3 destination, Vector3 source) {	
		Vector2 a = destination - source;
		
		float angle = Vector2.Angle (new Vector3(-1, 0, 0), a.normalized);
		
		if (a.y > 0) {
			angle *= -1;
		}
		
		
		Debug.DrawLine (Vector3.zero, a.normalized);
		Debug.DrawLine (Vector3.zero, new Vector3(1, 0, 0));
		return angle;
	}




}
