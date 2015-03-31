using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlanetSentinel : MonoBehaviour {

	Image hp;

	// Use this for initialization
	void Start () {
		hp = GameObject.Find ("HP").GetComponent<Image>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (hp.fillAmount <= 0) {
			LiveController.LoseLife();
		}
	}
}
