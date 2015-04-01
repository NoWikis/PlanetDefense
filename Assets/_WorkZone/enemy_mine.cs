using UnityEngine;
using System.Collections;

public class enemy_mine : MonoBehaviour {
	
	//public Vector2 initialSpeed;
	public GameObject explosionPrefab;
	public GameObject NoEffectPrefab;
	public string[] targetTags;
	public float[] targetDamage;
	
	public float flyTime 	= 1f;
	public float flyTimer 	= 0f;

	public float speed;
	
	// Use this for initialization
	void Start () {
		if (targetTags.Length != targetDamage.Length)
			Debug.LogError("Tag / Damage count mismatch");
		//GetComponent<Rigidbody>().AddForce(initialSpeed);
	}
	
	// Update is called once per frame
	void Update () {
		if (flyTimer > flyTime) {
			GetComponent<Light>().enabled = true;
			transform.DetachChildren();

			//rigidbody.velocity = rigidbody.velocity*0.5f;

			
			if (GetComponent<Light>().intensity < 4) {
				GetComponent<Light>().intensity += Time.deltaTime*4;
			}
			
			
			
			
		}
		else{
			flyTimer += Time.deltaTime;
			transform.position += Vector3.left * Time.deltaTime * speed;
		}

		
	}
	void OnTriggerEnter(Collider other) {
		//Debug.Log ("Mine Collision");
		if (other.gameObject.tag == "Proj_P1" || other.gameObject.tag == "Proj_P2") {
			GameObject o = (GameObject)Instantiate (explosionPrefab);
			o.transform.position = transform.position;
			Destroy (this.gameObject);
		}

		if (other.gameObject.tag == "Planet") {
			GameObject o = (GameObject)Instantiate (explosionPrefab);
			o.transform.position = transform.position;
			other.gameObject.GetComponent<Health>().takeDamage(15);
			Destroy (this.gameObject);
		}
	}
}
