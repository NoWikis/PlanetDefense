using UnityEngine;
using System.Collections;

public class railgun : MonoBehaviour {

	public Vector2 initialSpeed;
	public GameObject explosionPrefab;
	public GameObject NoEffectPrefab;
	public string[] targetTags;
	public float[] targetDamage;

	// Use this for initialization
	void Start () {
	
	}

	void Update () {
		Shoot ();
	}

	// Update is called once per frame
	void Shoot () {
		if (transform.localScale.x < 50) {
			Vector3 expanded = transform.localScale;
			expanded.x += 1f;
			transform.localScale = expanded;
			Vector3 movement = transform.localPosition;
			movement.x += 0.5f;
			transform.localPosition = movement;
		} else {
			if (transform.localScale.y > 0.1) {
				Vector3 expanded = transform.localScale;
				expanded.y -= 0.01f;
				transform.localScale = expanded;
			}
			else {
				Destroy(this.gameObject);
			}
		}
		if (transform.localScale.x > 5){
			Transform[] allChildren = GetComponentsInChildren<Transform>();
			foreach (Transform child in allChildren) {
				if (child.name == "ExpandedBeam" && child.GetComponent<Transform>().localScale.y < 2) {
					Vector3 expanded = child.GetComponent<Transform>().localScale;
					expanded.y += 0.1f;
					child.GetComponent<Transform>().localScale = expanded;
				}
			}
		}
	}

	void OnCollisionEnter(Collision other) {
		if (other.gameObject.tag == "Comet") {
			GameObject o = (GameObject)Instantiate (NoEffectPrefab);
			o.transform.position = transform.position;
			Destroy (this.gameObject);
		}
		
		if (other.gameObject.tag == "starFighter") {
			other.gameObject.GetComponentInParent<StarFighter>().Health--;
			GameObject o = (GameObject)Instantiate (explosionPrefab);
			o.transform.position = other.gameObject.transform.position;
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
