using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public Vector2 initialSpeed;
	public GameObject explosionPrefab;
	public GameObject NoEffectPrefab;
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
		Vector3 r = rigidbody.velocity.normalized;
		transform.localRotation = Quaternion.Euler (0, 0, 180 + Util.getAngleVector (r, Vector3.zero));

	}


	void OnCollisionEnter(Collision other) {
		if (other.gameObject.tag == "Comet") {
			GameObject o = (GameObject)Instantiate (NoEffectPrefab);
			o.transform.position = transform.position;
			Destroy (this.gameObject);
		}

		if (other.gameObject.tag == "starFighter") {
			GameObject o = (GameObject)Instantiate (explosionPrefab);
			Destroy (this.gameObject);
			Destroy (other.gameObject.transform.parent.gameObject);

		}

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
