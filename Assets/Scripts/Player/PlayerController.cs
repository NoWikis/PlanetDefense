using UnityEngine;
using System.Collections;
using InControl;

public class PlayerController : MonoBehaviour {
	//public Boundary boundary;
	public float speed;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		var InputDevice = InputManager.ActiveDevice;

		//rotation controls

		//movement controlsd

		Quaternion rotate = this.transform.rotation;
		//transform.RotateAround(Vector3.zero, Vector3.forward, InputDevice.LeftStickX*50 * Time.deltaTime);
		//transform.localRotation = rotate;

		if (Mathf.Abs (InputDevice.LeftStickX)> 0.2 || Mathf.Abs (InputDevice.LeftStickY )> 0.2) {
			float stickAngle = Mathf.Atan2(InputDevice.LeftStickX,InputDevice.LeftStickY)* Mathf.Rad2Deg;
			float playerAngle = PlanetManager.getAngleVector (transform.position);
			Debug.Log (playerAngle + stickAngle);
			//Debug.Log (playerAngles);

			
			//if (playerAngle <= 0) { //players' above
				if (playerAngle + stickAngle > playerAngle)
					transform.RotateAround(Vector3.zero, Vector3.forward, -50 * Time.deltaTime);
				else
					transform.RotateAround(Vector3.zero, Vector3.forward, 50 * Time.deltaTime);
			//}
			//if (playerAngle > 0) {
				//if (playerAngle + stickAngle > playerAngle)
				//	transform.RotateAround(Vector3.zero, Vector3.forward, 50 * Time.deltaTime);
				//else
					//transform.RotateAround(Vector3.zero, Vector3.forward, -50 * Time.deltaTime);
			//}
		}
		//if ( transform.localRotation.z < normalized - 0.1 && transform.localRotation.z < normalized + 0.1)
		transform.Rotate (new Vector3 (0f,0f,1f), 200.0f * Time.deltaTime * InputDevice.RightStickX, Space.World);
		//Vector3 movement = new Vector3 (0f,0f,PlanetManager.getAngleVector (transform.position)* InputDevice.LeftStickX);
		//rigidbody.velocity = movement * speed;
		//auto adjusts rotation according to plane
		//transform.localRotation = Quaternion.Euler (0,0,PlanetManager.getAngleVector(rotate));

	}
}
