using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class planet_lvl3 : MonoBehaviour {

	public Image			hp_bar;
	private Vector3 move_to;
	public bool hit;
	bool back_off;
	public float kb_speed;
	public float dmg;

	public float contact_kb_distance;
	public bool contact_point = false;
	Vector3 contact_pos;

	private GameObject hitObject;

	void Start() {
		GameObject hp_obj = GameObject.Find ("HP");
		hp_bar = hp_obj.GetComponent<Image> ();

	}

	void Update() {

		if(!contact_point){
			if (hit) {
				move_to = gameObject.transform.position;
				move_to.x -= 30f;
				hit = false;
				back_off = true;
			}
			
			float step = kb_speed * Time.deltaTime * 5;

			if(back_off){
				transform.position = Vector3.MoveTowards(transform.position, move_to, step);
			}

			if(gameObject.transform.position == move_to)
				back_off = false;
		}

		else {
			if (hit) {
				move_to = gameObject.transform.position;
				move_to.x -= (contact_pos.x - transform.position.x)*contact_kb_distance;
				move_to.y -= (contact_pos.y - transform.position.y)*contact_kb_distance;
				hit = false;
				back_off = true;
			}
			
			float step = kb_speed * Time.deltaTime * 5;
			
			if(back_off){
				transform.position = Vector3.MoveTowards(transform.position, move_to, step);
			}
			
			if(gameObject.transform.position == move_to)
				back_off = false;
		}



	}

	// Use this for initialization
	void OnCollisionEnter(Collision c) {
		if (c.gameObject.layer == 20) {
			hitObject = c.gameObject;
			hp_bar.fillAmount -= dmg;
			hit = true;

			ContactPoint contact = c.contacts[0];
			contact_pos = contact.point;
		}
	}

	void OnTriggerEnter(Collider c) {
		
		if (c.tag == "end_lvl1") {
			Application.LoadLevel("level_4_Real");
		}
		
	}
}
