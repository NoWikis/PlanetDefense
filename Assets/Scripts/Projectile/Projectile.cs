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

	void OnTriggerEnter(Collider c){
		print ("HI");
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

		if (other.gameObject.tag == "starTurret") {
			other.gameObject.GetComponentInParent<StarTurret>().Health--;
			GameObject o = (GameObject)Instantiate (explosionPrefab);
			o.transform.position = other.gameObject.transform.position;
			Destroy (this.gameObject);
		}

		if (other.gameObject.tag == "CrackedAsteroid") {
			other.gameObject.GetComponent<CrackedAsteroid>().giveProjectile(gameObject);
			other.gameObject.GetComponent<Health>().takeDamage(5);
			GameObject o = (GameObject)Instantiate (explosionPrefab);
			o.transform.position = gameObject.transform.position;
			Destroy(this.gameObject);
		}

		if (other.gameObject.tag == "shieldedShip") {
			other.gameObject.GetComponent<Health>().takeDamage (10);
			GameObject o = (GameObject)Instantiate (explosionPrefab);
			o.transform.position = gameObject.transform.position;
			Destroy (this.gameObject);
		}

		if (other.gameObject.tag == "starPlanet") {
			other.gameObject.GetComponent<Health>().takeDamage (5);
			other.gameObject.GetComponent<AlienPlanet>().damageIndicator();
			GameObject o = (GameObject)Instantiate (explosionPrefab);
			o.transform.position = gameObject.transform.position;
			Destroy (this.gameObject);
		}

		if (other.gameObject.tag == "miner") {
			other.gameObject.GetComponent<miner>().hp--;
			GameObject o = (GameObject)Instantiate (explosionPrefab);
			o.transform.position = gameObject.transform.position;
			Destroy (this.gameObject);
		}

		if (other.gameObject.tag == "mine") {
			GameObject o = (GameObject)Instantiate (explosionPrefab);
			o.transform.position = gameObject.transform.position;
			Destroy (this.gameObject);
			Destroy (other.gameObject);
		}

		if (other.gameObject.tag == "Boss") {
			GameObject o = (GameObject)Instantiate (explosionPrefab);
			o.transform.position = gameObject.transform.position;
			other.gameObject.GetComponent<boss_ship>().hp--;
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
