using UnityEngine;
using System.Collections;

public class railgun : MonoBehaviour {

	public Vector3 angle;
	public GameObject explosionPrefab;
	public GameObject NoEffectPrefab;
	public string[] targetTags;
	public float[] targetDamage;
	public float speed;
	public bool super;

	// Use this for initialization
	void Start () {
		if (super) {
			Vector3 temp = transform.localScale;
			temp.y = 2f;
			transform.localScale = temp;
		}
		Debug.Log (angle);
	}

	void Update () {
		//Debug.Log (angle);
		if (super) {
			ShootSuper();
		}
		else {
			Shoot ();
		}
	}

	void ShootSuper() {
		if (transform.localScale.x < 30) {
			Vector3 expanded = transform.localScale;
			expanded.x += 1f;
			transform.localScale = expanded;
			transform.Translate(speed * angle * Time.deltaTime, Space.World);
			
		} else {
			if (transform.localScale.y > 0.1) {
				Vector3 expanded = transform.localScale;
				expanded.y -= 0.02f;
				transform.localScale = expanded;
			}
			else {
				Destroy(this.gameObject);
			}
		}
		if (transform.localScale.x > 4){
			Transform[] allChildren = GetComponentsInChildren<Transform>();
			foreach (Transform child in allChildren) {
				if (child.name == "ExpandedBeam" && child.GetComponent<Transform>().localScale.y < 4) {
					Vector3 expanded = child.GetComponent<Transform>().localScale;
					expanded.y += 0.1f;
					child.GetComponent<Transform>().localScale = expanded;
				}
			}
		}

	}

	// Update is called once per frame
	void Shoot () {
		if (transform.localScale.x < 45) {
			Vector3 expanded = transform.localScale;
			expanded.x += 1f;
			transform.localScale = expanded;
			transform.Translate(angle * Time.deltaTime * speed * 2, Space.World);

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

	void OnColliderEnter(Collider other) {
		Debug.Log ("hit");
		if (other.gameObject.tag == "Asteroid_P1" || other.gameObject.tag == "Asteroid_P2") {
			//print ("Asteroid");
			other.GetComponent<Health>().takeDamage(25);
		}
		if (other.gameObject.tag == "Comet") {
			GameObject o = (GameObject)Instantiate (NoEffectPrefab);
			o.transform.position = transform.position;
			Destroy (this.gameObject);
		}
		
		if (other.gameObject.tag == "starFighter") {

			other.gameObject.GetComponentInParent<StarFighter>().Health--;
			GameObject o = (GameObject)Instantiate (explosionPrefab);
			o.transform.position = other.gameObject.transform.position;
		}
		
		for(int i = 0; i < targetTags.Length; ++i) {
			Physics.IgnoreCollision(other.GetComponent<Collider>(), GetComponent<Collider>());
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
