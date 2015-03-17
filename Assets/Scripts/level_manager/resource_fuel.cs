using UnityEngine;
using System.Collections;

public class resource_fuel : MonoBehaviour {

	public GameObject		planet;
	public Vector3 			initialVelocity = new Vector3 (0, 0, 0);
	public float			terminalVelocity;
	Rigidbody  physicsBase;


	void Awake () {
		planet = GameObject.FindGameObjectWithTag ("Planet");

		Vector3 towards = new Vector3(0,0,0);
		
		towards.x = planet.transform.position.x - transform.position.x;
		towards.y = planet.transform.position.y - transform.position.y;
		
		if(towards.x != 0){
			towards.x = (towards.x/towards.x);
		}
		
		if(towards.y != 0){
			towards.y = (towards.y/towards.y);
		}

		rigidbody.velocity = towards * 10;

	}

	// Use this for initialization
	void Start () {
		physicsBase = GetComponent<Rigidbody>();
		//rigidbody.velocity = initialVelocity;
	}
	
	// Update is called once per frame
	void Update () {
		GameObject[] celestialBodies = GameObject.FindGameObjectsWithTag ("Planet");
		
		foreach(GameObject planet in celestialBodies){
			Vector3 _force = PlanetPhysics.S.universalGravity (planet, this.gameObject);
			rigidbody.AddForce(_force);
		}
		
		if (rigidbody.velocity.magnitude > terminalVelocity) {
			rigidbody.velocity = rigidbody.velocity.normalized * terminalVelocity;
		}
	}

	void OnCollisionEnter(Collision c){
		//		print ("YO");
		if(c.gameObject.CompareTag ("Planet")) {
			Destroy(gameObject);
		}
		
	}
}
