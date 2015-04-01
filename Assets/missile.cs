using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class missile : MonoBehaviour {


	public GameObject Planet;
	public GameObject explosion;
	public GameObject spawner;
	public GameObject boss;
	public float speed;

	public float offset;


	Vector3 planet_pos;

	Image hp;


	// Use this for initialization
	void Start () {
		Planet = GameObject.Find ("planet");
		boss = GameObject.Find ("boss");
		hp = GameObject.Find ("HP").GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {

		planet_pos.x = Planet.transform.position.x + offset;
		planet_pos.y = Planet.transform.position.y;
		planet_pos.z = Planet.transform.position.z;
		
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, planet_pos, step);
	}

	void OnCollisionEnter(Collision other) {
		if (other.gameObject.tag == "Player") {
			GameObject o = (GameObject)Instantiate (explosion);
			o.transform.position = transform.position;
			spawner.GetComponent<missile_spawn>().spawnCycle = spawner.GetComponent<missile_spawn>().spawnTime;
			//boss.GetComponent<boss_ship> ().spawn--;
			Destroy (this.gameObject);
		}

		if (other.gameObject.tag == "Planet") {
			GameObject o = (GameObject)Instantiate (explosion);
			o.transform.position = transform.position;
			hp.fillAmount -= .15f;
			spawner.GetComponent<missile_spawn>().spawnCycle = spawner.GetComponent<missile_spawn>().spawnTime;
			//boss.GetComponent<boss_ship> ().spawn--;
			Destroy (this.gameObject);
		}

		if (other.gameObject.tag == "Proj_P1" || other.gameObject.tag == "Proj_P2") {
			GameObject o = (GameObject)Instantiate (explosion);
			o.transform.position = transform.position;
			spawner.GetComponent<missile_spawn>().spawnCycle = spawner.GetComponent<missile_spawn>().spawnTime;
			//boss.GetComponent<boss_ship> ().spawn--;
			Destroy (this.gameObject);
		}
	}
}
