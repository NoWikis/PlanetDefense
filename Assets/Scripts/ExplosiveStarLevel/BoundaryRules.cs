using UnityEngine;
using System.Collections;

public class BoundaryRules : MonoBehaviour {
	public GameObject	planet;
	private bool alphaBumper;
	// Use this for initialization
	public	float	endOfLevelDelay;
	void Start () {
		alphaBumper = true;
		planet = GameObject.FindGameObjectWithTag ("Planet");
	}

	// Update is called once per frame
	void Update () {
		Pulsate ();
		if ( planet && (Vector3.Distance(planet.transform.position,transform.position) >= transform.localScale.x/2 + 5))
			endOfLevel ();

	}

	void endOfLevel(){
		Color color = Color.green;
		renderer.material.color = color;
		while (endOfLevelDelay > 0)
			endOfLevelDelay -= Time.deltaTime;
		Application.LoadLevel("level_3_Real");
	}

//	void OnTriggerExit(Collider c){
//		if (c.gameObject.CompareTag ("Planet")) {
//			Debug.Log("Outside");
//			Color color = Color.green;
//			renderer.material.color = color;
//		}
//	}

	void Pulsate(){
		Color color = renderer.material.color;
		if (alphaBumper) {
			color.a += 0.025f;
		} else {
			color.a -= 0.025f;
		}
		
		renderer.material.color = color;
		if (color.a >= 0.90)
			alphaBumper = false;
		else if(color.a < 0.25)
			alphaBumper = true;
	}
}
