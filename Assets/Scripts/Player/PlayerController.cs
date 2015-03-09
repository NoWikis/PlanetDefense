using UnityEngine;
using System.Collections;
using System;
using InControl;



public class PlayerController : MonoBehaviour {
	//public Boundary boundary;
	public float speed;
	public int playerNum = 0;
	public GameObject ProjectilePrefab;
	public float projectileCoolDown		=	1f;
	public float projectileTimer		=	5f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		var InputDevice = (InputManager.Devices.Count > playerNum) ? InputManager.Devices[playerNum] : null;

		if (InputDevice == null)
		{
			// If no controller exists for this cube, just make it translucent.
			renderer.material.color = new Color( 1.0f, 1.0f, 1.0f, 0.2f );
		}
		else {
			//movement controlsd

			Quaternion rotate = this.transform.rotation;

			if (Mathf.Abs (InputDevice.LeftStickX)> 0.2 || Mathf.Abs (InputDevice.LeftStickY )> 0.2) {
				float stickAngle = Mathf.Atan2(InputDevice.LeftStickX,InputDevice.LeftStickY)* Mathf.Rad2Deg;
				float playerAngle = PlanetManager.getAngleVector (transform.position);
				Debug.Log (playerAngle + stickAngle);
				transform.RotateAround(Vector3.zero, Vector3.forward, InputDevice.LeftStickX * -50 * Time.deltaTime);
			}

			transform.Rotate (new Vector3 (0f,0f,1f), 100.0f * Time.deltaTime * InputDevice.LeftBumper, Space.World);
			transform.Rotate (new Vector3 (0f,0f,1f), -100.0f * Time.deltaTime * InputDevice.RightBumper, Space.World);

			//Shooting Controls
			if (InputDevice.RightTrigger & projectileTimer > projectileCoolDown) {
				projectileTimer = 0f;
				shootProjectile();
			}
			projectileTimer += Time.deltaTime;
		}
	}




	void shootProjectile() {
		GameObject o = (GameObject) Instantiate (ProjectilePrefab);
		o.transform.position = transform.position;
		o.GetComponent<Projectile>().initialSpeed = 
			Quaternion.Euler (0, 0, Util.getAngleVector(
				GameObject.FindGameObjectWithTag("Planet").transform.position, transform.position
				)  + 270 ) * 
				new Vector3(0, 1000, 0);
		Debug.Log (o.GetComponent<Projectile>().initialSpeed);
	}
}
