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

	// Use this for initialization
	void Start () {
		GameObject p1_cd_obj = GameObject.Find ("p1_cd");
		p1_cd_bar = p1_cd_obj.GetComponent<Image> ();

		GameObject p2_cd_obj = GameObject.Find ("p2_cd");
		p2_cd_bar = p2_cd_obj.GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {
		var inputDevice = (InputManager.Devices.Count > playerNum) ? InputManager.Devices[playerNum] : null;

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
		p1_cd_bar.fillAmount += (Time.deltaTime/(projectileCoolDown*1.3f));
		p2_cd_bar.fillAmount += (Time.deltaTime/(projectileCoolDown*1.3f));

	}




	void shootProjectile() {
		GameObject o = (GameObject) Instantiate (ProjectilePrefab);
		o.transform.position = transform.position;
		o.GetComponent<Projectile>().initialSpeed = 
			Quaternion.Euler (0, 0, Util.getAngleVector(
				GameObject.FindGameObjectWithTag("Planet").transform.position, transform.position
				)  + 270 ) * 
				new Vector3(0, 1000, 0);
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
	}


	void updatePlayer( InputDevice inputDevice){
		//movement controlsd
		
		Quaternion rotate = this.transform.rotation;
		
		//if (Mathf.Abs (inputDevice.LeftStickX)> 0.2 || Mathf.Abs (inputDevice.LeftStickY )> 0.2) {
			float stickAngle = Mathf.Atan2(inputDevice.LeftStickX,inputDevice.LeftStickY)* Mathf.Rad2Deg;
			float playerAngle = PlanetManager.getAngleVector (transform.position);
			//Debug.Log (playerAngle + stickAngle);
			transform.RotateAround(Vector3.zero, Vector3.forward, inputDevice.LeftTrigger * 50 * Time.deltaTime);
			transform.RotateAround(Vector3.zero, Vector3.forward, inputDevice.RightTrigger * -50 * Time.deltaTime);

		//}
		
		transform.Rotate (new Vector3 (0f,0f,1f), 100.0f * Time.deltaTime * inputDevice.LeftBumper, Space.World);
		transform.Rotate (new Vector3 (0f,0f,1f), -100.0f * Time.deltaTime * inputDevice.RightBumper, Space.World);
		
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
