﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class planet_lvl3 : MonoBehaviour {

	public Image			hp_bar;
	private Vector3 move_to;
	bool hit;
	bool back_off;
	public float kb_speed;
	public float dmg;

	void Start() {
		GameObject hp_obj = GameObject.Find ("HP");
		hp_bar = hp_obj.GetComponent<Image> ();

	}

	void Update() {


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


		if (hp_bar.fillAmount <= 0) {
			Application.LoadLevel("End");
		}
	}

	// Use this for initialization
	void OnTriggerEnter(Collider c) {
		if (c.gameObject.layer == 9) {
			hp_bar.fillAmount -= dmg;
			hit = true;
		}

		if (c.tag == "end_lvl1") {
			Application.LoadLevel("Title");
		}

	}
}