using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public Vector2 initialSpeed;
	public GameObject explosionPrefab;
	public string[] targetTags;
	public float[] targetDamage;

	// Use this for initialization
	void Start () {
		if (targetTags.Length != targetDamage.Length)
			Debug.LogError("Tag / Damage count mismatch");
		GetComponent<Rigidbody>().AddForce(initialSpeed);
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnCollisionEnter(Collision other) {

		for(int i = 0; i < targetTags.Length; ++i) {
			if (other.gameObject.tag == targetTags[i]) {
				other.gameObject.GetComponent<Health>().takeDamage(targetDamage[i]);

				// emit particle
				GameObject o = (GameObject)Instantiate (explosionPrefab);
				o.transform.position = transform.position;
				Destroy (this.gameObject);
			}
		}


	}
}
