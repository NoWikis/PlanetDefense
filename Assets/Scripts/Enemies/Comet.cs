using UnityEngine;
using System.Collections;

public class Comet : MonoBehaviour {

	float baseEffectScaleX;
	float baseEffectScaleY;

	GameObject flareEffect;
	public Vector3 initialVelocity;


	// Use this for initialization
	void Start () {
		flareEffect = GameObject.FindGameObjectWithTag("Effect");
		baseEffectScaleX = flareEffect.transform.localScale.x;
		baseEffectScaleY = flareEffect.transform.localScale.y;

		rigidbody.AddForce(initialVelocity);
	
	}
	
	// Update is called once per frame
	void Update () {
		flareEffect.transform.localScale = new Vector3(baseEffectScaleX + Random.Range (-.01f, .01f),
		                                               baseEffectScaleY + Random.Range (-.1f, .1f), 1);
	}
}
