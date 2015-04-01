using UnityEngine;
using System.Collections;

public class Octo_Arm : MonoBehaviour {

	public float			terminalVelocity;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
			
		GameObject[] celestialBodies = GameObject.FindGameObjectsWithTag ("Planet");

		foreach(GameObject planet in celestialBodies){
			Vector3 _force = boss_physics.S.universalGravity (planet, this.gameObject);
			rigidbody.AddForce(_force);
		}

		
		if (rigidbody.velocity.magnitude > terminalVelocity) {
			rigidbody.velocity = rigidbody.velocity.normalized * terminalVelocity;
		}
//		else {
//			foreach(GameObject planet in celestialBodies){
//				Vector3 _force = PlanetPhysics.S.universalGravity (planet, this.gameObject);
//				rigidbody2D.AddForce(_force);
//			}
//		}


	}
}
