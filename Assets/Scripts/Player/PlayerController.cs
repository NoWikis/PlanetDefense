using UnityEngine;
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

	private float offset				=	0f;

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
				)  + 270 - offset) * 
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
				)  + 255 ) * 
				new Vector3(0, 1000, 0);

		GameObject o2 = (GameObject) Instantiate (ProjectilePrefab);
		o2.transform.position = transform.position;
		o2.GetComponent<Projectile>().initialSpeed = 
			Quaternion.Euler (0, 0, Util.getAngleVector(
				GameObject.FindGameObjectWithTag("Planet").transform.position, transform.position
				)  + 265 ) * 
				new Vector3(0, 1000, 0);

		GameObject o3 = (GameObject) Instantiate (ProjectilePrefab);
		o3.transform.position = transform.position;
		o3.GetComponent<Projectile>().initialSpeed = 
			Quaternion.Euler (0, 0, Util.getAngleVector(
				GameObject.FindGameObjectWithTag("Planet").transform.position, transform.position
				)  + 275 ) * 
				new Vector3(0, 1000, 0);

		GameObject o4 = (GameObject) Instantiate (ProjectilePrefab);
		o4.transform.position = transform.position;
		o4.GetComponent<Projectile>().initialSpeed = 
			Quaternion.Euler (0, 0, Util.getAngleVector(
				GameObject.FindGameObjectWithTag("Planet").transform.position, transform.position
				)  + 285 ) * 
				new Vector3(0, 1000, 0);

		sound_combined.Play ();
	}


	void updatePlayer( InputDevice inputDevice){
		//movement controlsd
		
		Quaternion rotate = this.transform.rotation;
		
		//if (Mathf.Abs (inputDevice.LeftStickX)> 0.2 || Mathf.Abs (inputDevice.LeftStickY )> 0.2) {
			float stickAngle = Mathf.Atan2(inputDevice.LeftStickX,inputDevice.LeftStickY)* Mathf.Rad2Deg;
			float playerAngle = PlanetManager.getAngleVector (transform.position);
			//Debug.Log (playerAngle + stickAngle);

			//transform.RotateAround(Vector3.zero, Vector3.forward, inputDevice.LeftStickX * -50 * Time.deltaTime);

			transform.RotateAround(Vector3.zero, Vector3.forward, inputDevice.LeftTrigger * 100 * Time.deltaTime);
			transform.RotateAround(Vector3.zero, Vector3.forward, inputDevice.RightTrigger * -100 * Time.deltaTime);

		//}
		Transform[] allChildren = GetComponentsInChildren<Transform>();
		foreach (Transform child in allChildren) {
			if (child.name == "cannon") {
				Vector3 angle = child.transform.localEulerAngles;
				if (inputDevice.LeftBumper) {
					angle.z = Mathf.Clamp(angle.z + Time.deltaTime*100*(-inputDevice.LeftBumper), 45f, 135.0f);
				}
				if (inputDevice.RightBumper) {
					angle.z = Mathf.Clamp(angle.z + Time.deltaTime*100*(inputDevice.RightBumper), 45f , 135.0f);
				}
				child.transform.localEulerAngles = angle;
				offset = 90f-angle.z;
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
