using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AlienProjectile : MonoBehaviour {

	
	public Vector2 initialSpeed;
	public GameObject explosionPrefab;
	public GameObject NoEffectPrefab;
	public string[] targetTags;
	public float[] targetDamage;
	public Image	hp_bar;
	
	// Use this for initialization
	void Start () {
		GameObject hp_obj = GameObject.Find ("HP");
		hp_bar = hp_obj.GetComponent<Image> ();
		if (targetTags.Length != targetDamage.Length)
			Debug.LogError("Tag / Damage count mismatch");
		GetComponent<Rigidbody>().AddForce(initialSpeed);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	
	void OnCollisionEnter(Collision other) {
		if (other.gameObject.tag == "Comet") {
			GameObject o = (GameObject)Instantiate (NoEffectPrefab);
			o.transform.position = transform.position;
			Destroy (this.gameObject);
		}
		
		if (other.gameObject.tag == "Player") {
			GameObject o = (GameObject)Instantiate (explosionPrefab);
			o.transform.position = transform.position;
			Destroy (this.gameObject);
		}
		if (other.gameObject.tag == "Planet") {
			Debug.Log("Planet");
			GameObject o = (GameObject)Instantiate (explosionPrefab);
			o.transform.position = transform.position;
			hp_bar.fillAmount -= 0.05f;
			Destroy(gameObject);

		}
		
		
	}
}
