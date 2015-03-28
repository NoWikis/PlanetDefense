using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class timer : MonoBehaviour {

	Image _me;

	public float cycle_time;
	public float cur_level;


	// Use this for initialization
	void Start () {
		_me = GetComponent<Image> ();

		_me.fillAmount = 0;
		cur_level = 1;
	}
	
	// Update is called once per frame
	void Update () {
		_me.fillAmount += (Time.deltaTime/cycle_time);

		if (_me.fillAmount >= 1) {
			_me.fillAmount = 0;
			cur_level++;
		}
	}
}
