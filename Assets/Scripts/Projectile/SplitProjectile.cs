using UnityEngine;
using System.Collections;

public class SplitProjectile : MonoBehaviour {

	public GameObject		projectilePrefab;
	public float			numBullets;

	private float 			slice;
	// Use this for initialization
	void Start () {
		slice = 360 / numBullets;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDestroy(){
		float angle_from_planet = Util.getAngleVector (GameObject.FindGameObjectWithTag ("Planet").transform.position, transform.position) - 90;
		for (int mult = 1; mult <= numBullets; mult++) {
			createBullet(angle_from_planet, mult * slice);
		}
		GameObject o = (GameObject)Instantiate (GetComponent<Projectile>().explosionPrefab);
		o.transform.position = transform.position;
	}

	void createBullet(float angle, float adjustment){
		GameObject o2 = (GameObject) Instantiate (projectilePrefab);
		o2.transform.position = transform.position;
		o2.GetComponent<Projectile>().initialSpeed = 
			Quaternion.Euler (0, 0, angle + adjustment) * new Vector3(0, 1000, 0);
	}
}
