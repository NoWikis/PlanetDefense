using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AsteroidPhysics : MonoBehaviour {

	public GameObject		planet;
	public GameObject[]	asteroids;
	public Vector3 		initialVelocity = new Vector3 (0, 0, 0);

	public float			terminalVelocity;
	public float 			bounceFactor;
	public Image	hp_bar;
	private float			bounceDelay;

	Rigidbody  physicsBase;

	// Use this for initialization
	void Awake () {
		planet = GameObject.FindGameObjectWithTag ("Planet");
	}

	void Start() {
		physicsBase = GetComponent<Rigidbody>();
		rigidbody.velocity = initialVelocity;
		GameObject hp_obj = GameObject.Find ("HP");
		hp_bar = hp_obj.GetComponent<Image> ();
		bounceDelay = 0f;
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 _force = PlanetPhysics.S.universalGravity (planet, this.gameObject);
		//float drag = _force.magnitude / terminalVelocity;
		if (bounceDelay > 0)
			bounceDelay -= Time.deltaTime;
		//rigidbody.velocity += (_force - (rigidbody.velocity * drag)) * Time.deltaTime;
//		print (_force);
		rigidbody.AddForce (_force);
		if (rigidbody.velocity.magnitude > terminalVelocity) {
			rigidbody.velocity = rigidbody.velocity.normalized * terminalVelocity;
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
		if (c.gameObject.CompareTag ("Player") && bounceDelay <= 0f) {
			ContactPoint hit_pt = c.contacts [0];
			Vector3 dir = rigidbody.velocity;
			dir = dir - 2 * (Vector3.Dot (dir, hit_pt.normal)) * hit_pt.normal;
			rigidbody.velocity = bounceFactor*dir;
			//rigidbody.AddRelativeForce(2f*dir,ForceMode.VelocityChange);
			bounceDelay = 1f;
		}

		else if(c.gameObject.CompareTag ("Planet")) {
			hp_bar.fillAmount -= physicsBase.mass/500;
			Destroy(gameObject);

			if(hp_bar.fillAmount == 0){
				Application.LoadLevel ("End");
			}
		}

	}
}
