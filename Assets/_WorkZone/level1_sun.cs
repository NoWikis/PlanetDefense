using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class level1_sun : MonoBehaviour {

	Image hp;
	
	// Use this for initialization
	void Start () {
		hp = GameObject.Find ("HP").GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider c){
		if (c.gameObject.CompareTag ("Planet")) {
			hp.fillAmount = 0;
		}
	}
}
