using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class level2_planet : MonoBehaviour {

	level_manager manager;
	public Text comment_1;
	public Text comment_2;
	public GameObject Explosion;

	public bool mars;
	public bool alive = true;

	// Use this for initialization
	void Start () {
		GameObject comment_obj = GameObject.Find ("comment_1");
		comment_1 = comment_obj.GetComponent<Text> ();

		GameObject comment_obj2 = GameObject.Find ("comment_2");
		comment_2 = comment_obj2.GetComponent<Text> ();

		manager = Explosion.GetComponent<level_manager>();
	}
	
	// Update is called once per frame
	void Update () {

		if (mars && transform.position.x >= 75 && alive) {
			comment_2.enabled = true;
		}

		if (manager.level2_scene_done)
			comment_2.enabled = false;


		Vector3 go = new Vector3 (13, 0, 0);
		transform.Translate(go * Time.deltaTime);

		if(transform.position.x >= 144.5) {

			comment_1.enabled = true;
			Explosion.GetComponent<level2_script>().start_explode = true;
		}
	}

}
