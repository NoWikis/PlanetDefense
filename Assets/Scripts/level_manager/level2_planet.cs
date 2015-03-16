using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class level2_planet : MonoBehaviour {

	public Text comment_1;
	public GameObject Explosion;

	// Use this for initialization
	void Start () {
		GameObject comment_obj = GameObject.Find ("comment_1");
		comment_1 = comment_obj.GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {


		Vector3 go = new Vector3 (13, 0, 0);
		transform.Translate(go * Time.deltaTime);

		if(transform.position.x >= 144.5) {

			comment_1.enabled = true;
			Explosion.GetComponent<level2_script>().start_explode = true;
		}
	}

}
