using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Health : MonoBehaviour {

	float health = 100;
	bool tookHit;

	public delegate void DamageCallback(GameObject healthObj);



	List<DamageCallback> damageCBs = new List<DamageCallback>();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}






	// returns the current health state
	public float current() {
		return health;
	}

	// Registers a function to be called when takeDamage is called on the health instance.
	public void registerDamageCallback(DamageCallback d) {
		damageCBs.Add (d);
	}


	// Returns if health is equal or less than zero.
	public bool isDead() {
		return health <= 0;
	}


	// Has the object take damage
	public void takeDamage(float damage) {
		health -= damage;
		tookHit = true;

		foreach(DamageCallback d in damageCBs) {
			d(this.gameObject);
		}
	}

	public void recover(float damage) {
		health += damage;
	}
}
