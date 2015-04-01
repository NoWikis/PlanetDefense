using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class level3_jupiter : MonoBehaviour {

	level3_camera manager;
	public Text comment_1;
	
	// Use this for initialization
	void Start () {
		GameObject comment_obj = GameObject.Find ("comment_1");
		comment_1 = comment_obj.GetComponent<Text> ();
		
		manager = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<level3_camera>();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (transform.position.x >= 875) {
			comment_1.enabled = true;
		}

		if (transform.position.x >= 895) {
			manager.scene_playing = false;
			Destroy(gameObject);
		}
		
		Vector3 go = new Vector3 (13, 0, 0);
		transform.Translate(go * Time.deltaTime);
	}
	
}
