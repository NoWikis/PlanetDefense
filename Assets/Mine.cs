using UnityEngine;
using System.Collections;

public class Mine : MonoBehaviour {

	public Vector2 initialSpeed;
	public GameObject explosionPrefab;
	public GameObject NoEffectPrefab;
	public string[] targetTags;
	public float[] targetDamage;

	public float flyTime 	= 1.5f;
	public float flyTimer 	= 0f;
	
	// Use this for initialization
	void Start () {
		if (targetTags.Length != targetDamage.Length)
			Debug.LogError("Tag / Damage count mismatch");
		GetComponent<Rigidbody>().AddForce(initialSpeed);
	}
	
	// Update is called once per frame
	void Update () {
		if (flyTimer > flyTime) {
			GetComponent<Light>().enabled = true;
			transform.DetachChildren();
			rigidbody.velocity = rigidbody.velocity*0.5f;

			if (GetComponent<Light>().intensity < 4) {
				GetComponent<Light>().intensity += Time.deltaTime*4;
			}




		}
		flyTimer += Time.deltaTime;
	
	}
	void OnCollisionEnter(Collision other) {
		Debug.Log ("Mine Collision");
		if (other.gameObject.tag == "Comet") {
			GameObject o = (GameObject)Instantiate (NoEffectPrefab);
			o.transform.position = transform.position;
			Destroy (this.gameObject);
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
