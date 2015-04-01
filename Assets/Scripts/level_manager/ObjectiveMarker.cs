using UnityEngine;
using System.Collections;

public class ObjectiveMarker : MonoBehaviour {
	
	
	SpriteRenderer sprite;
	GameObject planet;
	GameObject objective;
	public float limitLength = 30;
	public float fadeBase = 1;
	public float fadeFactor = 10;
	public float zLayer = 0;
	public float time = 0;

	float originalScaleX;
	float originalScaleY;

	void Awake() {
		sprite = GetComponentInChildren<SpriteRenderer> ();
		objective = GameObject.FindGameObjectWithTag ("Objective");
		//sprite.enabled = false;

		originalScaleX = transform.localScale.x;
		originalScaleY = transform.localScale.y;
	}
	
	
	// Use this for initialization
	void Start () {
		
		planet = GameObject.FindGameObjectWithTag ("Planet");
		
		
		
		
		//sprite.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (objective == null || planet == null) {
			Destroy (gameObject);
			return;
		}
		
		//sprite.enabled = !(transform.parent.GetComponent<Renderer>().isVisible);
		Vector3 newPos = objective.transform.position - planet.transform.position;
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
			Util.getAngleVector(objective.transform.position, planet.transform.position) - 180
			);
		
		
		transform.position = new Vector3(newPos.x + planet.transform.position.x,
		                                 newPos.y + planet.transform.position.y,
		                                 transform.position.z);



		
		time += Time.deltaTime;
		float alpha = .5f + .5f*Mathf.Sin(2*time);
		transform.localScale = new Vector3 (
			originalScaleX + originalScaleX * Mathf.Sin (time*3)*.3f,
			originalScaleY + originalScaleY * Mathf.Sin (time*3)*.3f,
			1
		);



		sprite.color = new Color (
			1f, 
			1f, 
			1f, 
			alpha
		);


		//Debug.Log (alpha);

	}
}
