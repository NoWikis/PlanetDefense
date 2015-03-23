using UnityEngine;
using System.Collections;

public class SmallComet : MonoBehaviour {
	
	float baseEffectScaleX;
	float baseEffectScaleY;
	
	GameObject flareEffect;

	public GameObject planet;
	
	public Vector3 targetPos;

	public GameObject explosion;

	private Vector3 direction;

	public float speed = 10f;

	private float time = 0f;
	
	
	// Use this for initialization
	void Start () {
		planet = GameObject.Find ("planet");
		flareEffect = GameObject.FindGameObjectWithTag("Effect");
		baseEffectScaleX = flareEffect.transform.localScale.x;
		baseEffectScaleY = flareEffect.transform.localScale.y;
		targetPos = planet.transform.position;
		transform.LookAt (targetPos);	
		direction = Vector3.Normalize(targetPos-this.transform.position);
		Debug.Log (direction);

	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		//if (Vector3.Distance (targetPos, planet.transform.position) < 2.5f) {
			//targetPos = planet.transform.position;
			//transform.LookAt (targetPos);
		//}
		if (this.gameObject != null) {
			flareEffect.transform.localScale = new Vector3(baseEffectScaleX + Random.Range (-.01f, .01f),
			                                              baseEffectScaleY + Random.Range (-.1f, .1f), 1);
		}
		transform.position += direction * speed;
		//Debug.Log (transform.position);
		if (time > 4f) {
			Destroy (this.gameObject);
			return;
		}


	}
	
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.GetComponent<AsteroidBehavior> ()) {
			GameObject o = (GameObject) Instantiate(explosion);
			o.transform.position = other.transform.position;
			Destroy (other.gameObject);
		}
	}
}
