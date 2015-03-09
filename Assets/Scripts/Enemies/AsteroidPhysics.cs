using UnityEngine;
using System.Collections;

public class AsteroidPhysics : MonoBehaviour {

	public GameObject		planet;
	public GameObject[]	asteroids;
	public Vector3 		initialVelocity = new Vector3 (0, 0, 0);

	public float			terminalVelocity;

	// Use this for initialization
	void Awake () {
		planet = GameObject.FindGameObjectWithTag ("Planet");
	}

	void Start() {
		rigidbody.velocity = initialVelocity;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		Vector3 _force = PlanetPhysics.S.universalGravity (planet, this.gameObject);
		//float drag = _force.magnitude / terminalVelocity;

		//rigidbody.velocity += (_force - (rigidbody.velocity * drag)) * Time.deltaTime;
//		print (_force);

		if (rigidbody.velocity.magnitude < terminalVelocity) {
			rigidbody.AddForce (_force);
		} else {
			rigidbody.AddForce(_force);
			Vector3 _v = rigidbody.velocity.normalized;
//			print (_v);
			rigidbody.velocity = _v * terminalVelocity;
//			rigidbody.velocity = _force.normalized * terminalVelocity;
//			rigidbody.AddForce (1.5f*_force - _force);
//			rigidbody.AddForce(-1.5f * _force);
		}
	
	}


//	Just google Vector Reflection for better reference material
//	This code I was originally moving the projectile by reassigning its xy-pos
//	I believe that dir will probably corresspond to rigidbody.velocity
//	Probably want to make sure collision detection is strict

//	ContactPoint hit_pt = c.contacts [0];
//	dir = dir - 2 * (Vector3.Dot (dir, hit_pt.normal)) * hit_pt.normal;
//	Vector3 dir; << Corressponds to the direction of the asteroid
	void OnCollisionEnter(Collision c){
//		print ("YO");
		if (c.gameObject.CompareTag ("Player")) {
			print (rigidbody.velocity);
			ContactPoint hit_pt = c.contacts [0];
			Vector3 dir = rigidbody.velocity;
			dir = dir - 2 * (Vector3.Dot (dir, hit_pt.normal)) * hit_pt.normal;
			print (dir);
			rigidbody.velocity = 2f*dir;
//			rigidbody.AddRelativeForce(1.75f*dir,ForceMode.VelocityChange);
//
		}

	}

	void OnCollisionStay(Collision c){
		//OnCollisionEnter (c);
	}
}
