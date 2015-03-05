using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public Vector2 initialSpeed;
	public GameObject explosionPrefab;

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody>().AddForce(initialSpeed);
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnCollisionEnter(Collision other) {

		if (other.gameObject.tag == "Asteroid") {
			other.gameObject.GetComponent<Health>().takeDamage(25);
		}

		// emit particle
		GameObject o = (GameObject)Instantiate (explosionPrefab);
		o.transform.position = transform.position;
		Destroy (this.gameObject);
	}
}
