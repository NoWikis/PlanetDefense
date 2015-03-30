using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using InControl;


public class PlayerController : MonoBehaviour {
	//public Boundary boundary;
	public float InitSpeed;
	private float speed;
	public float maxSpeed;
	public int playerNum;
	public GameObject projectilePrefab;
	public GameObject projectilePrefab2;
	public GameObject railgunPrefab;
	public GameObject railgunChargePrefab;
	
	//projecitile
	public float projecitileCoolDown	=	0.5f;
	public float projecitileTimer		=	5f;
	private bool projectileButtonDownOnce = false;

	//mine
	public float mineCoolDown	=	5f;
	public float mineTimer		=	5f;

	//Railgun
	public float railgunChargeUp	=	2f;
	public float railgunTimer		=	0f;

	//projectile combined
	public float combinedCoolDown		=	5f;
	public float combinedTimer 			= 	5f;
	

	public float fuel_auto_fill_rate 	= 	30f;
	public float fuel_collect_rate		=	.2f;
	public float fuel_dec_rate 			= 	2f;
	public float start_fuel = 0f;

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

	//for PowerUps
	public float	power_timer = 0f;
	public float 	speed_modifier = 1f;
	public bool		super_shot = false;
	public float	rocket_boost = 1f;
	SpriteRenderer	p1_pwr_icon;
	SpriteRenderer	p2_pwr_icon;
	Image	p1_pwr_bar;
	Image	p2_pwr_bar;

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

		p1_fuel_bar.fillAmount = start_fuel;
		p2_fuel_bar.fillAmount = start_fuel;

		//PowerUp Cooldown UI Bar Initializaitons
		p1_pwr_icon = GameObject.Find ("p1_pwr_icon").GetComponent<SpriteRenderer> ();
		p2_pwr_icon = GameObject.Find ("p2_pwr_icon").GetComponent<SpriteRenderer> ();
		p1_pwr_bar = GameObject.Find ("p1_pwr_cd").GetComponent<Image> ();
		p2_pwr_bar = GameObject.Find ("p2_pwr_cd").GetComponent<Image> ();
		p1_pwr_bar.fillAmount = 0;
		p2_pwr_bar.fillAmount = 0;

		var aSources = GetComponents<AudioSource>();
		sound_basic = aSources [0];
		sound_combined = aSources [1];

		Transform[] allChildren = GetComponentsInChildren<Transform>();

		foreach (Transform child in allChildren) {
			if (child.name == "playerBoost") {
				child.GetComponent<ParticleSystem>().enableEmission = false;
			}
		}

		speed = InitSpeed;

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


		if(playerNum == 0)
			p1_pwr_bar.fillAmount = power_timer / 5f;
		if (playerNum == 1)
			p2_pwr_bar.fillAmount = power_timer / 5f;

		if (power_timer > 0)
			power_timer -= Time.deltaTime;
		else
			applyPower ("ResetPower");

		if(refill == true){
			p1_fuel_bar.fillAmount += Time.deltaTime / fuel_auto_fill_rate;
			p2_fuel_bar.fillAmount += Time.deltaTime / fuel_auto_fill_rate;
		}

	}

	GameObject shootProjectile(GameObject projectileType, float angle_offset, float speed){
		GameObject o = (GameObject)Instantiate (projectileType);		
		o.transform.position = transform.position;
		if (o.GetComponent<Projectile> ()) {
			o.GetComponent<Projectile> ().initialSpeed = 
			Quaternion.Euler (0, 0, Util.getAngleVector (
				GameObject.FindGameObjectWithTag ("Planet").transform.position, transform.position
			) + 270 + angle_offset) * 
				new Vector3 (0, speed, 0);
		}
		return o;
	}

	void shootBasic() {
		shootProjectile(projectilePrefab,0,1000);
		sound_basic.Play ();
		//Debug.Log (o.GetComponent<projecitile>().initialSpeed);
	}

	void shootSecondary() {
		GameObject o = shootProjectile (projectilePrefab2, 0, 200);
		//This Lines specific to Mine's 
		//o.GetComponent<Transform>().eulerAngles = new Vector3(0,0,transform.eulerAngles.z-90f);
		sound_basic.Play ();
	}

	void shootRailgun() {
		GameObject o = shootProjectile (railgunPrefab, 0, 1);
		o.transform.position = transform.position;
		o.GetComponent<railgun> ().angle = this.transform.localPosition;
		o.GetComponent<Transform>().eulerAngles = new Vector3(0,0,transform.eulerAngles.z);
		Debug.Log (transform.localPosition);
		Debug.Log (planetPos);

		if((combined && combinedTimer >= combinedCoolDown)){
			combinedTimer = 0f;
			if(playerNum == 0){
				p1_comb_cd.fillAmount = 0;
			}
			
			if(playerNum == 1){
				p2_comb_cd.fillAmount = 0;
			}
			o.GetComponent<railgun> ().super = true;
		}
		sound_basic.Play ();
	}

	void shootCombined(){
		for (int x = 1; x <= 4; x++) {
			shootProjectile(projectilePrefab,10*x - 25,1000);
		}
		sound_combined.Play ();
	}

	void MovePlanet (float speed) {
		var planet = transform.parent.GetComponent<Transform>();
		Vector3 movement = planet.transform.position - transform.position; 
		if (speed < maxSpeed) {
			speed += 0.02f;
		}
		planet.transform.position = planet.transform.position + movement*speed*0.02f*rocket_boost;
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
		
		//Shooting Controls
		if (inputDevice.RightBumper) {
			railgunTimer += Time.deltaTime;
			if (railgunTimer > 0.2 && railgunTimer < railgunChargeUp) {
				GetComponent<Light>().intensity += Time.deltaTime*2;
			}
			if (railgunTimer > railgunChargeUp) {
				Transform[] allChildren = GetComponentsInChildren<Transform>();
				foreach (Transform child in allChildren) {
					if (child.name == "ChargedUp") {
						child.GetComponent<ParticleSystem>().enableEmission = true;
					}
				}
			}
			if (!projectileButtonDownOnce) {
				if(!combined && projecitileTimer >= projecitileCoolDown){
					projecitileTimer = 0f;
					if(super_shot)
						shootCombined();
					else
						shootBasic();

					if(playerNum == 0){
						p1_cd_bar.fillAmount = 0;
					}

					if(playerNum == 1){
						p2_cd_bar.fillAmount = 0;
					}
				}

				else if((combined && combinedTimer >= combinedCoolDown) ){
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
			projectileButtonDownOnce = true;
		}
		else {
			if (railgunTimer > railgunChargeUp) {
				Debug.Log(railgunTimer);
				shootRailgun ();
			}
			Transform[] allChildren = GetComponentsInChildren<Transform>();
			foreach (Transform child in allChildren) {
				if (child.name == "ChargedUp") {
					child.GetComponent<ParticleSystem>().enableEmission = false;
				}
			}
			GetComponent<Light>().intensity = 0;
			railgunTimer = 0;
			projectileButtonDownOnce = false;
		}


		if (inputDevice.LeftBumper) {
			if(mineTimer >= mineCoolDown){
				mineTimer = 0f;
				shootSecondary();
				
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
	
						child.GetComponent<ParticleSystem>().enableEmission = true;
					}
				}
				refill = false;
			}
		}
		else {
			Transform[] allChildren = GetComponentsInChildren<Transform>();
			speed = InitSpeed;
			foreach (Transform child in allChildren) {
				if (child.name == "playerBoost") {
	
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
					transform.RotateAround(planetPos, Vector3.forward, 150 * max * Time.deltaTime * speed_modifier);
				}
				else if (angle < 0){
					transform.RotateAround(planetPos, Vector3.forward, -150 * max * Time.deltaTime * speed_modifier);
				}
			}
			
		}

		//rotating turrets
		/*if (Mathf.Abs (inputDevice.RightStickX)> 0.2 || Mathf.Abs (inputDevice.RightStickY )> 0.2) {
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
		}*/

		//Reangles cannon
		/*if (inputDevice.RightStickButton) {
			Transform[] allChildren = GetComponentsInChildren<Transform>();
			foreach (Transform child in allChildren) {
				if (child.name == "cannon") {
					Vector3 turretAngle = child.transform.localEulerAngles;
					turretAngle.z = 90f;
					child.transform.localEulerAngles = turretAngle;
					turretRotationOffset = 0f;
				}
			}		
		}*/
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
		if (c.gameObject.CompareTag ("PowerUp")) {
			applyPower ("ResetPower");
			applyPower (c.gameObject.name);
			Destroy(c.gameObject);
		}
	}

	void applyPower(string powerType){
		//Change Corressponding variable (code)
		//Change Power Icon (UI)
		//Change Cooldown Timer (code)
		//Change Cooldown Bar (UI)
		if (powerType.Contains ("rocketBoost")) {
			power_timer = 5f;
			rocket_boost = 2f;
			fuel_dec_rate  = 100f;

			if(playerNum == 0){
				p1_pwr_icon.sprite = Resources.Load("booster", typeof(Sprite)) as Sprite;
				p1_fuel_bar.fillAmount = 1;
			} else{
				p2_pwr_icon.sprite = Resources.Load("booster", typeof(Sprite)) as Sprite;
				p2_fuel_bar.fillAmount = 1;
			}
				

		} else if (powerType.Contains ("superShot")) {
			power_timer = 5f;
			super_shot = true;

			if(playerNum == 0)
				p1_pwr_icon.sprite = Resources.Load("superShot", typeof(Sprite)) as Sprite;
			else
				p2_pwr_icon.sprite = Resources.Load("superShot", typeof(Sprite)) as Sprite;

		} else if (powerType.Contains ("speedUp")) {
			power_timer = 5f;
			speed_modifier = 2f;

			if(playerNum == 0)
				p1_pwr_icon.sprite = Resources.Load("speedUp", typeof(Sprite)) as Sprite;
			else
				p2_pwr_icon.sprite = Resources.Load("speedUp", typeof(Sprite)) as Sprite;

		} else {

			if(playerNum == 0)
				p1_pwr_icon.sprite = null;
			else
				p2_pwr_icon.sprite = null;

			rocket_boost = 1f;
			fuel_dec_rate = 2f;
			speed_modifier = 1f;
			super_shot = false;
			power_timer = 0f;
		}
	}

	void OnCollisionExit(Collision c){
		if (c.gameObject.CompareTag ("Player")) {
			combined = false;
			Debug.Log ("Separated");
		}
	}



}
