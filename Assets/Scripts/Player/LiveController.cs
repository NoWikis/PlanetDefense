using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LiveController : MonoBehaviour {
	static int lives = 3;
	static string lastScene;
	static bool goneToLimbo = false;

	static float timeInLimbo = 5f;
	static float time;

	// Use this for initialization
	void Start () {
		time = 0f;
		GetComponentInChildren<Text> ().text = "Lives Left: " + lives;
	}

	void Update() {
		time += Time.deltaTime;
		if (time >= timeInLimbo) {
			goneToLimbo = false;
			Application.LoadLevel (lastScene);
		}
	}



	public static void LoseLife() {
		lives--;

		if (lives < 0) {
			lives=3;
			Application.LoadLevel("End");
		} else {
			lastScene = Application.loadedLevelName;
			goneToLimbo = true;
			Application.LoadLevel ("Continue");
		}
	}


}
