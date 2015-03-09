using UnityEngine;
using System.Collections;
using InControl;

public class PlayerController : MonoBehaviour {
	//public Boundary boundary;
	public float speed;
	public float projectileCoolDown		=	5f;
	public float projectileTimer		=	5f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		var InputDevice = InputManager.ActiveDevice;

		//rotation controls

		//movement controlsd

		Quaternion rotate = this.transform.rotation;

		//Movement around orbit
		if (Mathf.Abs (InputDevice.LeftStickX)> 0.2 || Mathf.Abs (InputDevice.LeftStickY )> 0.2) {
			float stickAngle = Mathf.Atan2(InputDevice.LeftStickX,InputDevice.LeftStickY)* Mathf.Rad2Deg;
			float playerAngle = PlanetManager.getAngleVector (transform.position);
			transform.RotateAround(Vector3.zero, Vector3.forward, InputDevice.LeftStickX * -50 * Time.deltaTime);
		}

		//Rotations done via left and right bumper
		transform.Rotate (new Vector3 (0f,0f,1f), 100.0f * Time.deltaTime * InputDevice.LeftBumper, Space.World);
		transform.Rotate (new Vector3 (0f,0f,1f), -100.0f * Time.deltaTime * InputDevice.RightBumper, Space.World);

		//Shooting Controls
		if (InputDevice.RightTrigger & projectileTimer > projectileCoolDown) {
			projectileTimer = 0f;
			Debug.Log("Shoot");
		}

		projectileTimer += Time.deltaTime;
	}
}
