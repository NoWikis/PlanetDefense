using UnityEngine;
using System.Collections;

public class explosion_script : MonoBehaviour {

	public float expansion_rate = 5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.localScale.x <= 427f)
			transform.localScale += new Vector3(expansion_rate, expansion_rate, 0);
	
	}

	void OnTriggerEnter(Collider c){
		if (c.gameObject.CompareTag ("mars")) {
			c.gameObject.GetComponent<level2_planet>().alive = false;
		}
		else{
			Destroy (c.gameObject);
		}
	}
}
