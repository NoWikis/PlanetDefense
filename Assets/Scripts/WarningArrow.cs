using UnityEngine;
using System.Collections;

public class WarningArrow : MonoBehaviour {


	SpriteRenderer sprite;
	GameObject planet;
	public float limitLength = 20;
	public float fadeBase = 1;
	public float fadeFactor = 10;

	void Awake() {
		sprite = GetComponent<SpriteRenderer> ();
		//sprite.enabled = false;
	}


	// Use this for initialization
	void Start () {

		planet = GameObject.FindGameObjectWithTag ("Planet");



		if (transform.parent == null || planet == null)
			Destroy (gameObject);

		//sprite.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		sprite.enabled = !(transform.parent.GetComponent<Renderer>().isVisible);
		Vector3 newPos = transform.parent.position;
		if (newPos.x > limitLength)
						newPos.x = limitLength;
		if (newPos.x < -limitLength)
						newPos.x = -limitLength;
		if (newPos.y < -limitLength) 
						newPos.y = -limitLength;
		if (newPos.y > limitLength)
						newPos.y = limitLength;

		transform.rotation = Quaternion.Euler (
			0, 
			0, 
			Util.getAngleVector(transform.parent.transform.position, planet.transform.position)
		);


		transform.position = newPos + planet.transform.position;

		float alpha = (fadeFactor / (1f +
		                 (Vector3.Distance (planet.transform.position, transform.parent.position) - limitLength)
		               )) + fadeBase;
		sprite.color = new Color (
			1f, 
			1f, 
			1f, 
			alpha
		);


		//Debug.Log (alpha);

	}
}
