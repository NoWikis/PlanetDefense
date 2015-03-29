using UnityEngine;
using System.Collections;

public class level1_music : MonoBehaviour {

	public AudioClip main;
	public AudioClip comet;

	AudioSource player;

	// Use this for initialization
	void Start () {
		player = GetComponent<AudioSource>();

		player.clip = main;
		player.loop = false;

		player.Play ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!player.isPlaying) {
			player.clip = comet;
			player.Play();
			player.loop = true;
		}
	}
}
