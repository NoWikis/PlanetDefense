using UnityEngine;
using System.Collections;

public class PlanetPhysics : MonoBehaviour {

	static public PlanetPhysics S;

	private float G = 0.05f;//6.673e-11f;

	void Awake() {
		S = this;
	}

	//Thanks Newton
	public Vector3 universalGravity(GameObject obj1, GameObject obj2){
		float r = Vector3.Distance (obj1.transform.position, obj2.transform.position);

		Vector3 v = obj1.transform.position - obj2.transform.position;
		float force = G * (obj1.rigidbody.mass * obj2.rigidbody.mass / Mathf.Pow (r, 2f));
		//print ("R: " + r + "\nV: " + v + "\nF: " + force);
		return (v.normalized * force);
	}

}
