﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using InControl;


public class PlayerController : MonoBehaviour {
	//public Boundary boundary;
	public float speed;
	public int playerNum;
	public GameObject ProjectilePrefab;
	public float projectileCoolDown		=	1f;
	public float combinedCoolDown 		=	5f;
	public float projectileTimer		=	5f;
	public float combinedTimer 			= 	5f;

	public bool combined;

	Image	p1_cd_bar;
	Image	p2_cd_bar;

	Image	p1_comb_cd;
	Image	p2_comb_cd;

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


		var aSources = GetComponents<AudioSource>();
		sound_basic = aSources [0];
		sound_combined = aSources [1];
	}
	
	// Update is called once per frame
	void Update () {
		var inputDevice = (playerNum == 1) ?  InputManager.Devices[1] : InputManager.Devices[0];

		if (inputDevice == null)
		{
			// If no controller exists for this cube, just make it translucent.
			Debug.Log("no player");
			renderer.material.color = new Color( 1.0f, 1.0f, 1.0f, 0.2f );
			Destroy(this.gameObject);
		}
		else {
			planetPos = transform.parent.GetComponent<Transform> ().transform.position;
			updatePlayer(inputDevice);
		}

		if(projectileTimer < 1){
			projectileTimer += Time.deltaTime;
		}

		if (combinedTimer < 5) {
			combinedTimer += Time.deltaTime;
		}

		p1_cd_bar.fillAmount += (Time.deltaTime/(projectileCoolDown*1.5f));
		p2_cd_bar.fillAmount += (Time.deltaTime/(projectileCoolDown*1.5f));

		p1_comb_cd.fillAmount += (Time.deltaTime/(combinedCoolDown*2f));
		p2_comb_cd.fillAmount += (Time.deltaTime/(combinedCoolDown*2f));

	}




	void shootProjectile() {
		GameObject o = (GameObject) Instantiate (ProjectilePrefab);
		o.transform.position = transform.position;
		o.GetComponent<Projectile>().initialSpeed = 
			Quaternion.Euler (0, 0, Util.getAngleVector(
				GameObject.FindGameObjectWithTag("Planet").transform.position, transform.position
				)  + 270 - turretRotationOffset) * 
				new Vector3(0, 1000, 0);
		sound_basic.Play ();
		//Debug.Log (o.GetComponent<Projectile>().initialSpeed);
	}

	void shootCombined(){
		GameObject o1 = (GameObject) Instantiate (ProjectilePrefab);
		o1.transform.position = transform.position;
		o1.GetComponent<Projectile>().initialSpeed = 
			Quaternion.Euler (0, 0, Util.getAngleVector(
				GameObject.FindGameObjectWithTag("Planet").transform.position, transform.position
				)  + 255 - turretRotationOffset) * 
				new Vector3(0, 1000, 0);

		GameObject o2 = (GameObject) Instantiate (ProjectilePrefab);
		o2.transform.position = transform.position;
		o2.GetComponent<Projectile>().initialSpeed = 
			Quaternion.Euler (0, 0, Util.getAngleVector(
				GameObject.FindGameObjectWithTag("Planet").transform.position, transform.position
				)  + 265 - turretRotationOffset) * 
				new Vector3(0, 1000, 0);

		GameObject o3 = (GameObject) Instantiate (ProjectilePrefab);
		o3.transform.position = transform.position;
		o3.GetComponent<Projectile>().initialSpeed = 
			Quaternion.Euler (0, 0, Util.getAngleVector(
				GameObject.FindGameObjectWithTag("Planet").transform.position, transform.position
				)  + 275 - turretRotationOffset) * 
				new Vector3(0, 1000, 0);

		GameObject o4 = (GameObject) Instantiate (ProjectilePrefab);
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
		planet.transform.position = planet.transform.position + movement*speed*0.005f;
		transform.parent.GetComponent<Transform>().transform.position = planet.transform.position;

//		planetPos = transform.parent.GetComponent<Transform> ().transform.position;

	}


	void updatePlayer( InputDevice inputDevice){
		//movement controlsd
		
		Quaternion rotate = this.transform.rotation;
		
		if (Mathf.Abs (inputDevice.LeftStickX)> 0.2 || Mathf.Abs (inputDevice.LeftStickY )> 0.2) {
			//float stickAngle = Mathf.Atan2(inputDevice.LeftStickX,inputDevice.LeftStickY)* Mathf.Rad2Deg;
			//float playerAngle = PlanetManager.getAngleVector (transform.position);
			//Debug.Log (playerAngle + stickAngle);
			Vector3 ThumbPos = new Vector3(inputDevice.LeftStickX, inputDevice.LeftStickY, 0);
			Vector3 playerPos = planetPos + this.transform.position;

			//if (Mathf.Abs(playerPos.x) > Mathf.Abs (playerPos.y)) {
			//	playerPos = playerPos/playerPos.x;
			//}
			//else {
				//playerPos = playerPos/playerPos.y;
			//}

			var angle = Vector3.Angle (playerPos, ThumbPos);
			var cross = Vector3.Cross (playerPos, ThumbPos);
			if (cross.z < 0) 
				angle = -angle;
			Debug.Log (angle);
			Debug.Log(cross);
			if (Mathf.Abs(angle) > 2) {
				if (angle >= 0)
					transform.RotateAround(planetPos, Vector3.forward, 100 * Time.deltaTime);
				else
					transform.RotateAround(planetPos, Vector3.forward, -100 * Time.deltaTime);
			}

		}
		Transform[] allChildren = GetComponentsInChildren<Transform>();
		foreach (Transform child in allChildren) {
			if (child.name == "cannon") {
				Vector3 angle = child.transform.localEulerAngles;
				if (inputDevice.LeftBumper) {
					angle.z = Mathf.Clamp(angle.z + Time.deltaTime*100*(inputDevice.LeftBumper), 45f, 135.0f);
				}
				if (inputDevice.RightBumper) {
					angle.z = Mathf.Clamp(angle.z + Time.deltaTime*100*(-inputDevice.RightBumper), 45f , 135.0f);
				}
				child.transform.localEulerAngles = angle;
				turretRotationOffset = 90f-angle.z;
			}
		}
		//transform.Rotate (new Vector3 (0f,0f,1f), 100.0f * Time.deltaTime * inputDevice.LeftBumper, Space.World);
		//transform.Rotate (new Vector3 (0f,0f,1f), -100.0f * Time.deltaTime * inputDevice.RightBumper, Space.World);
		
		//Shooting Controls
		if (inputDevice.Action1) {

			if(!combined && projectileTimer >= projectileCoolDown){
				projectileTimer = 0f;
				shootProjectile();

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

		if (inputDevice.Action2) {
			MovePlanet(inputDevice.Action2); 

		}
	}

	void OnCollisionEnter(Collision c){
		if (c.gameObject.CompareTag ("Player")) {
			combined = true;
			Debug.Log ("Combined");
		}
	}


	void OnCollisionExit(Collision c){
		if (c.gameObject.CompareTag ("Player")) {
			combined = false;
			Debug.Log ("Separated");
		}
	}



}
