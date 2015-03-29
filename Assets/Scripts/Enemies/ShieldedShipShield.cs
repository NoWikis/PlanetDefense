using UnityEngine;
using System.Collections;

public class ShieldedShipShield : MonoBehaviour {

	public GameObject noEffect;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void OnCollisionEnter(Collision c) {

		if (c.gameObject.tag == "Proj_P1" ||
		    c.gameObject.tag == "Proj_P2") {

			Debug.Log ("What");

			Destroy(c.gameObject);
			GameObject o = (GameObject)Instantiate (noEffect);
			o.transform.position = c.gameObject.transform.position;

		}

	}
}
