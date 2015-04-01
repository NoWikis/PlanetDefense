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

		if (Input.GetKey (KeyCode.Alpha1)) {
			Application.LoadLevel ("Level_1");
		}

		if (Input.GetKey (KeyCode.Alpha2)) {
			Application.LoadLevel ("Level_2_Real");
		}

		if (Input.GetKey (KeyCode.Alpha3)) {
			Application.LoadLevel ("level_3_Real");
		}

		if (Input.GetKey (KeyCode.Alpha4)) {
			Application.LoadLevel ("level_4_Real");
		}
		if (hp.fillAmount <= 0) {
			LiveController.LoseLife();
		}
	}
	


}
