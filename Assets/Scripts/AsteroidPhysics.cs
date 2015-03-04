using UnityEngine;
using System.Collections;

public class AsteroidPhysics : MonoBehaviour {

	public GameObject		planet;
	public GameObject[]	asteroids;
	public Vector3 		initialVelocity = new Vector3 (0, 0, 0);

	// Use this for initialization
	void Awake () {
		planet = GameObject.FindGameObjectWithTag ("Planet");
	}

	void Start() {
		rigidbody.velocity = initialVelocity;
	}
	
	// Update is called once per frame
	void Update () {
//		if (planet)
//			print ("Got Planet" + planet.tag);
//		if (this.gameObject)
//			print ("Sould work");
		Vector3 _force = PlanetPhysics.S.universalGravity (planet, this.gameObject);
//		print (_force);
		rigidbody.AddForce (_force);

	
	}


//	Just google Vector Reflection for better reference material
//	This code I was originally moving the projectile by reassigning its xy-pos
//	I believe that dir will probably corresspond to rigidbody.velocity
//	Probably want to make sure collision detection is strict

//	Vector3 dir; << Corressponds to the direction of the asteroid
//	void OnCollisionEnter(Collision c){
//		ContactPoint hit_pt = c.contacts [0];
//		dir = dir - 2 *  (Vector3.Dot (dir,hit_pt.normal)) * hit_pt.normal;
//	}
}
