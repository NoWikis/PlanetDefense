using UnityEngine;
using System.Collections;

public class Shadifier : MonoBehaviour {

	public float minShadeModifier = .6f;
	public float maxShadeModifier = .8f;

	// Use this for initialization
	public void Shadify () {
		float value = Random.Range(
			minShadeModifier,
			maxShadeModifier
		);
		Debug.Log (value);
		Debug.Log (GetComponent<MeshRenderer>().material.color);
		Color newColor = new Color(
			value * GetComponent<MeshRenderer>().material.color.r,
			value * GetComponent<MeshRenderer>().material.color.g,
			value * GetComponent<MeshRenderer>().material.color.b,
			1f
		);


		GetComponent<MeshRenderer>().material.color = 
			newColor ;
		Debug.Log (GetComponent<MeshRenderer>().material.color);
	}

}
