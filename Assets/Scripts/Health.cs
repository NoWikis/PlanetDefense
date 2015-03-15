using UnityEngine;
using System.Collections;
using System.Collections.Generic;


// Passive object that manages a health amount

public class Health : MonoBehaviour {

	float maxHealth = 100;
	float health = 100;
	public bool invincible = false;	

	public delegate void DamageCallback(GameObject healthObj);
	List<DamageCallback> damageCBs = new List<DamageCallback>();



	




	public void init(float currentHealth, float maxHealth_) {
		health = currentHealth;
		maxHealth = maxHealth_;
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
		if (invincible) return;
		health -= damage;

		foreach(DamageCallback d in damageCBs) {
			d(this.gameObject);
		}
	}

	// Regains an amount of health
	public void recover(float healAmt) {
		health += healAmt;
		if (health>maxHealth) health = maxHealth;
	}

	// Returns the maximum health amount
	public float max() {
		return maxHealth;
	}


}
