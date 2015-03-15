using UnityEngine;
using System.Collections;

public class BoundaryRules : MonoBehaviour {

	private bool alphaBumper;
	// Use this for initialization
	void Start () {
		alphaBumper = true;
	}
	
	// Update is called once per frame
	void Update () {
		Pulsate ();
	}

	void OnTriggerExit(Collider c){
		if (c.gameObject.CompareTag ("Planet")) {
			Debug.Log("Outside");
			Color color = Color.green;
			renderer.material.color = color;
		}
	}

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
