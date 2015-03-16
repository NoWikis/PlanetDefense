using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class level1_fake_player : MonoBehaviour {

	public AudioSource sound_basic;
	public GameObject ProjectilePrefab;
	public float cooldown = 2f;
	public float say = 4f;
	public bool p2 = false;
	public Text comment;

	// Use this for initialization
	void Start () {
		var aSources = GetComponents<AudioSource>();
		sound_basic = aSources [0];
		GameObject comment_obj = GameObject.Find ("comment");
		comment = comment_obj.GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		cooldown -= Time.deltaTime;
		say -= Time.deltaTime;

		if(cooldown <= 0f){
			shootProjectile ();
		}

		if(say<=0f) {
			comment.enabled = true;
		}
	}


	void shootProjectile() {
		GameObject o = (GameObject) Instantiate (ProjectilePrefab);
		o.transform.position = transform.position;
		o.GetComponent<Projectile>().initialSpeed = 
			Quaternion.Euler (0, 0, Util.getAngleVector(
				GameObject.FindGameObjectWithTag("mars").transform.position, transform.position
				)  + 270) * 
				new Vector3(0, 1000, 0);
		sound_basic.Play ();
		cooldown = 2f;
		//Debug.Log (o.GetComponent<Projectile>().initialSpeed);
	}
}
