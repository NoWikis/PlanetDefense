using UnityEngine;
using System.Collections;

public class Comet : MonoBehaviour {

	float baseEffectScaleX;
	float baseEffectScaleY;

	GameObject flareEffect;
	public float initialVelocity;


	public GameObject explosion;
	public float speed;

	// Use this for initialization
	void Start () {
		flareEffect = GameObject.FindGameObjectWithTag("Effect");
		baseEffectScaleX = flareEffect.transform.localScale.x;
		baseEffectScaleY = flareEffect.transform.localScale.y;

	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += Vector3.left * Time.deltaTime * speed;
		//rigidbody.velocity = initialVelocity;
		flareEffect.transform.localScale = new Vector3(baseEffectScaleX + Random.Range (-.01f, .01f),
		                                               baseEffectScaleY + Random.Range (-.1f, .1f), 1);
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.GetComponent<AsteroidBehavior> ()) {
			GameObject o = (GameObject) Instantiate(explosion);
			o.transform.position = other.transform.position;
			Destroy (other.gameObject);

		}
	}
}
