using UnityEngine;
using System.Collections;

public class SmallComet : MonoBehaviour {
	
	float baseEffectScaleX;
	float baseEffectScaleY;
	
	GameObject flareEffect;

	public GameObject planet;
	
	public Vector3 targetPos;

	public GameObject explosion;

	public float speed = 10f;
	
	
	// Use this for initialization
	void Start () {
		planet = GameObject.Find ("planet");
		flareEffect = GameObject.FindGameObjectWithTag("Effect");
		baseEffectScaleX = flareEffect.transform.localScale.x;
		baseEffectScaleY = flareEffect.transform.localScale.y;
		targetPos = planet.transform.position;
		transform.LookAt (-targetPos);
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance (targetPos, planet.transform.position) < 2.5f) {
			targetPos = planet.transform.position;
			transform.LookAt (-targetPos);
		}
		flareEffect.transform.localScale = new Vector3(baseEffectScaleX + Random.Range (-.01f, .01f),
		                                               baseEffectScaleY + Random.Range (-.1f, .1f), 1);
		transform.Translate(targetPos*Time.deltaTime*speed);

	}
	
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.GetComponent<AsteroidBehavior> ()) {
			GameObject o = (GameObject) Instantiate(explosion);
			o.transform.position = other.transform.position;
			Destroy (other.gameObject);
			
		}
	}
}
