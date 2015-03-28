using UnityEngine;
using System.Collections;

public class IndestructableAsteroid : MonoBehaviour {
	

	

	float mass = 100;




	
	public float initialRotation = 0f;
	public float initialRotationalVelocity = 0f;

	
	
	
	void Awake() {
		

		transform.rotation = Quaternion.Euler(0, 0, initialRotation);
		rigidbody.AddTorque(0, 0, initialRotationalVelocity);
	}
	
	
	
	// Use this for initialization
	void Start () {

		GetComponent<AsteroidMesh>().GenerateMesh();
		GetComponent<SphereCollider>().radius = GetComponent<AsteroidMesh>().GetAverageRadius();
	}
	

	
	
	
	


}
