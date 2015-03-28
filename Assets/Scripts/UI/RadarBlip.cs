using UnityEngine;
using System.Collections;

public class RadarBlip : MonoBehaviour {

	public float duration = 1;
	float time;
	// Use this for initialization
	void Start () {
		time = duration;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		time -= Time.deltaTime;
		GetComponentInChildren<SpriteRenderer> ().material.color = new Color (1, 1, 1, time / duration);
		if (time < 0)
						Destroy (this.gameObject);
	}
}
