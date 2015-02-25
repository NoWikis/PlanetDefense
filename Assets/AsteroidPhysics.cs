using UnityEngine;
using System.Collections;

public class AsteroidPhysics : MonoBehaviour {

	public GameObject		planet;
	public GameObject[]	asteroids;

	// Use this for initialization
	void Awake () {
		planet = GameObject.FindGameObjectWithTag ("Planet");
		rigidbody.velocity = new Vector3 (-1, 1, 0);
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
}
