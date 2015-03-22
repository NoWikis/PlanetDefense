﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using InControl;


public class PlayerController : MonoBehaviour {
	//public Boundary boundary;
	public float speed;
	public int playerNum;
	public GameObject projectilePrefab;
	public GameObject projectilePrefab2;
	
	//projecitile
	public float projecitileCoolDown	=	0.5f;
	public float projecitileTimer		=	5f;

	//mine
	public float mineCoolDown	=	5f;
	public float mineTimer		=	5f;

	//projectile combined
	public float combinedCoolDown		=	5f;
	public float combinedTimer 			= 	5f;


	public float fuel_auto_fill_rate 	= 	30f;
	public float fuel_collect_rate		=	.2f;
	public float fuel_dec_rate 			= 	1f;

	public bool combined;

	bool refill;

	Image	p1_cd_bar;
	Image	p2_cd_bar;

	Image	p1_comb_cd;
	Image	p2_comb_cd;

	Image	p1_fuel_bar;
	Image 	p2_fuel_bar;

	//for turret angle limitation
	private float turretRotationOffset				=	0f;

	//for planetPosition
	public Vector3 planetPos					=	Vector3.zero;

	//for sound effects
	public AudioSource sound_basic;
	public AudioSource sound_combined; 

	// Use this for initialization
	void Start () {
		GameObject p1_cd_obj = GameObject.Find ("p1_cd");
		p1_cd_bar = p1_cd_obj.GetComponent<Image> ();

		GameObject p2_cd_obj = GameObject.Find ("p2_cd");
		p2_cd_bar = p2_cd_obj.GetComponent<Image> ();

		GameObject p1_comb_obj = GameObject.Find ("p1_comb_cd");
		p1_comb_cd = p1_comb_obj.GetComponent<Image> ();

		GameObject p2_comb_obj = GameObject.Find ("p2_comb_cd");
		p2_comb_cd = p2_comb_obj.GetComponent<Image> ();

		GameObject p1_fuel_obj = GameObject.Find ("p1_fuel_bar");
		p1_fuel_bar = p1_fuel_obj.GetComponent<Image> ();
		
		GameObject p2_fuel_obj = GameObject.Find ("p2_fuel_bar");
		p2_fuel_bar = p2_fuel_obj.GetComponent<Image> ();

		p1_fuel_bar.fillAmount = 0;
		p2_fuel_bar.fillAmount = 0;


		var aSources = GetComponents<AudioSource>();
		sound_basic = aSources [0];
		sound_combined = aSources [1];

		Transform[] allChildren = GetComponentsInChildren<Transform>();

		foreach (Transform child in allChildren) {
			if (child.name == "playerBoost") {
				Debug.Log("boosting");
				child.GetComponent<ParticleSystem>().enableEmission = false;
			}
		}

		refill = true;


	}
	
	// Update is called once per frame
	void Update () {
		var inputDevice = (playerNum == 1) ? InputManager.Devices[1] : InputManager.Devices[0];

		if (inputDevice == null)
		{
			// If no controller exists for this cube, just make it translucent.
			Debug.Log("no player");
			renderer.material.color = new Color( 1.0f, 1.0f, 1.0f, 0.2f );
			Destroy(this.gameObject);
		}
		else {
			updatePlayer(inputDevice);
		}

		if(projecitileTimer < 1){
			projecitileTimer += Time.deltaTime;
		}

		if(mineTimer < 5){
			mineTimer += Time.deltaTime;
		}

		if (combinedTimer < 5) {
			combinedTimer += Time.deltaTime;
		}

		p1_cd_bar.fillAmount += (Time.deltaTime/(projecitileCoolDown*1.5f));
		p2_cd_bar.fillAmount += (Time.deltaTime/(projecitileCoolDown*1.5f));

		p1_comb_cd.fillAmount += (Time.deltaTime/(combinedCoolDown*2f));
		p2_comb_cd.fillAmount += (Time.deltaTime/(combinedCoolDown*2f));

		if(refill == true){
			p1_fuel_bar.fillAmount += Time.deltaTime / fuel_auto_fill_rate;
			p2_fuel_bar.fillAmount += Time.deltaTime / fuel_auto_fill_rate;
		}

	}




	void shootProjecitile() {
		GameObject o = (GameObject) Instantiate (projectilePrefab);
		o.transform.position = transform.position;
		o.GetComponent<Projectile>().initialSpeed = 
			Quaternion.Euler (0, 0, Util.getAngleVector(
				GameObject.FindGameObjectWithTag("Planet").transform.position, transform.position
				)  + 270 - turretRotationOffset) * 
				new Vector3(0, 1000, 0);
		sound_basic.Play ();
		//Debug.Log (o.GetComponent<projecitile>().initialSpeed);
	}

	void shootMine() {
		GameObject o = (GameObject) Instantiate (projectilePrefab2);
		o.transform.position = transform.position;
		o.GetComponent<Transform>().eulerAngles = new Vector3(0,0,transform.eulerAngles.z-90f);
		o.GetComponent<Mine>().initialSpeed = 
			Quaternion.Euler (0, 0, Util.getAngleVector(
				GameObject.FindGameObjectWithTag("Planet").transform.position, transform.position
				)  + 270 - turretRotationOffset) * 
				new Vector3(0, 200, 0);
		sound_basic.Play ();
		//Debug.Log (o.GetComponent<projecitile>().initialSpeed);
	}

	void shootCombined(){
		GameObject o1 = (GameObject) Instantiate (projectilePrefab);
		o1.transform.position = transform.position;
		o1.GetComponent<Projectile>().initialSpeed = 
			Quaternion.Euler (0, 0, Util.getAngleVector(
				GameObject.FindGameObjectWithTag("Planet").transform.position, transform.position
				)  + 255 - turretRotationOffset) * 
				new Vector3(0, 1000, 0);

		GameObject o2 = (GameObject) Instantiate (projectilePrefab);
		o2.transform.position = transform.position;
		o2.GetComponent<Projectile>().initialSpeed = 
			Quaternion.Euler (0, 0, Util.getAngleVector(
				GameObject.FindGameObjectWithTag("Planet").transform.position, transform.position
				)  + 265 - turretRotationOffset) * 
				new Vector3(0, 1000, 0);

		GameObject o3 = (GameObject) Instantiate (projectilePrefab);
		o3.transform.position = transform.position;
		o3.GetComponent<Projectile>().initialSpeed = 
			Quaternion.Euler (0, 0, Util.getAngleVector(
				GameObject.FindGameObjectWithTag("Planet").transform.position, transform.position
				)  + 275 - turretRotationOffset) * 
				new Vector3(0, 1000, 0);

		GameObject o4 = (GameObject) Instantiate (projectilePrefab);
		o4.transform.position = transform.position;
		o4.GetComponent<Projectile>().initialSpeed = 
			Quaternion.Euler (0, 0, Util.getAngleVector(
				GameObject.FindGameObjectWithTag("Planet").transform.position, transform.position
				)  + 285 - turretRotationOffset) * 
				new Vector3(0, 1000, 0);

		sound_combined.Play ();
	}

	void MovePlanet (float speed) {
		var planet = transform.parent.GetComponent<Transform>();
		Vector3 movement = planet.transform.position - transform.position; 
		planet.transform.position = planet.transform.position + movement*speed*0.02f;
		transform.parent.GetComponent<Transform>().transform.position = planet.transform.position;

//		planetPos = transform.parent.GetComponent<Transform> ().transform.position;

		if (playerNum == 0) {
			p1_fuel_bar.fillAmount -= Time.deltaTime/fuel_dec_rate;
		}
		
		if (playerNum == 1) {
			p2_fuel_bar.fillAmount -= Time.deltaTime/fuel_dec_rate;
		}

	}


	void updatePlayer( InputDevice inputDevice){
		//movement controlsd

		//transform.Rotate (new Vector3 (0f,0f,1f), 100.0f * Time.deltaTime * inputDevice.LeftBumper, Space.World);
		//transform.Rotate (new Vector3 (0f,0f,1f), -100.0f * Time.deltaTime * inputDevice.RightBumper, Space.World);
		
		//Shooting Controls
		if (inputDevice.RightTrigger) {

			if(!combined && projecitileTimer >= projecitileCoolDown){
				projecitileTimer = 0f;
				shootProjecitile();

				if(playerNum == 0){
					p1_cd_bar.fillAmount = 0;
				}

				if(playerNum == 1){
					p2_cd_bar.fillAmount = 0;
				}
			}

			else if(combined && combinedTimer >= combinedCoolDown){
				combinedTimer = 0f;
				shootCombined();

				if(playerNum == 0){
					p1_comb_cd.fillAmount = 0;
				}

				if(playerNum == 1){
					p2_comb_cd.fillAmount = 0;
				}
			}
		}

		if (inputDevice.LeftTrigger) {
			if(mineTimer >= mineCoolDown){
				mineTimer = 0f;
				shootMine();
				
				if(playerNum == 0){
					p1_cd_bar.fillAmount = 0;
				}
				
				if(playerNum == 1){
					p2_cd_bar.fillAmount = 0;
				}
			}
		}

		//Propulsion action
		if (inputDevice.Action2) {
			if((playerNum == 0 && p1_fuel_bar.fillAmount > 0.05) || (playerNum == 1 && p2_fuel_bar.fillAmount > 0.05)){
				MovePlanet(inputDevice.Action2); 
				Transform[] allChildren = GetComponentsInChildren<Transform>();
				foreach (Transform child in allChildren) {
					if (child.name == "playerBoost") {
						Debug.Log("boosting");
						child.GetComponent<ParticleSystem>().enableEmission = true;
					}
				}
				refill = false;
			}
		}
		else {
			Transform[] allChildren = GetComponentsInChildren<Transform>();
			foreach (Transform child in allChildren) {
				if (child.name == "playerBoost") {
					Debug.Log("boosting");
					child.GetComponent<ParticleSystem>().enableEmission = false;
				}
			}

			refill = true;
		}
		planetPos = transform.parent.GetComponent<Transform> ().transform.position;


		Quaternion rotate = this.transform.rotation;
		var max = Mathf.Abs (inputDevice.LeftStickY);
		if (Mathf.Abs (inputDevice.LeftStickX) > Mathf.Abs (inputDevice.LeftStickY )) {
			max = Mathf.Abs (inputDevice.LeftStickX);
		}

		//rotating player
		if (Mathf.Abs (inputDevice.LeftStickX)> 0.2 || Mathf.Abs (inputDevice.LeftStickY )> 0.2) {
			
			Vector3 ThumbPos = new Vector3(inputDevice.LeftStickX, inputDevice.LeftStickY, 0);
			Vector3 playerPos = this.transform.position - planetPos;
			
			var angle = Vector3.Angle (playerPos, ThumbPos);
			var cross = Vector3.Cross (playerPos, ThumbPos);
			if (cross.z < 0) 
				angle = -angle;
			if (Mathf.Abs (angle) > 2) {
				if (angle >= 0) {
					transform.RotateAround(planetPos, Vector3.forward, 150 * max * Time.deltaTime);
				}
				else if (angle < 0){
					transform.RotateAround(planetPos, Vector3.forward, -150 * max * Time.deltaTime);
				}
			}
			
		}

		//rotating turrets
		if (Mathf.Abs (inputDevice.RightStickX)> 0.2 || Mathf.Abs (inputDevice.RightStickY )> 0.2) {
			Transform[] allChildren = GetComponentsInChildren<Transform>();
			foreach (Transform child in allChildren) {
				if (child.name == "cannon") {
					
					Vector3 ThumbPos = new Vector3(inputDevice.RightStickX, inputDevice.RightStickY, 0);
					Vector3 playerPos = planetPos + this.transform.position;
					var angle = Vector3.Angle (playerPos, ThumbPos);
					var cross = Vector3.Cross (playerPos, ThumbPos);
					if (cross.z < 0) 
						angle = -angle;
					
					Vector3 turretAngle = child.transform.localEulerAngles;
					
					if (Mathf.Abs(angle) > 2) {
						if (angle >= 0)
							turretAngle.z = Mathf.Clamp(turretAngle.z + Time.deltaTime*350, 20f, 160.0f);
						else
							turretAngle.z = Mathf.Clamp(turretAngle.z + Time.deltaTime*-350, 20f , 160.0f);
					}
					
					child.transform.localEulerAngles = turretAngle;
					turretRotationOffset = 90f-turretAngle.z;
				}
			}
		}

		//Reangles cannon
		if (inputDevice.RightStickButton) {
			Transform[] allChildren = GetComponentsInChildren<Transform>();
			foreach (Transform child in allChildren) {
				if (child.name == "cannon") {
					Vector3 turretAngle = child.transform.localEulerAngles;
					turretAngle.z = 90f;
					child.transform.localEulerAngles = turretAngle;
					turretRotationOffset = 0f;
				}
			}		
		}
	}

	void OnCollisionEnter(Collision c){
		if (c.gameObject.CompareTag ("Player")) {
			combined = true;
			Debug.Log ("Combined");
		}

		if (c.gameObject.CompareTag ("p1_fuel") && playerNum == 0) {
			Destroy(c.gameObject);
			if(playerNum == 0)
				p1_fuel_bar.fillAmount += fuel_collect_rate;
		}
		
		if (c.gameObject.CompareTag ("p2_fuel") && playerNum == 1) {
			Destroy(c.gameObject);
			if(playerNum == 1)
				p2_fuel_bar.fillAmount += fuel_collect_rate;
		}
	}


	void OnCollisionExit(Collision c){
		if (c.gameObject.CompareTag ("Player")) {
			combined = false;
			Debug.Log ("Separated");
		}
	}



}
