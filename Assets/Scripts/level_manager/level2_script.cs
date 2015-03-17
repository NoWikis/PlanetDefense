using UnityEngine;
using System.Collections;

public class level2_script : MonoBehaviour {

	public bool start_explode;
	public float expansion_rate = 5f;
	Vector3 originalScale;
	Vector3 destinationScale = new Vector3(425.5f, 425.5f, 1f);

	bool go = true;

		
	// Use this for initialization
	void Start () {
		originalScale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
		if (start_explode) {
			
			if (transform.localScale.x <= 427f)
				transform.localScale += new Vector3 (expansion_rate, expansion_rate, 0);
			
		}
	}
}
