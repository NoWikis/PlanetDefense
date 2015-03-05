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
		transform.Rotate (new Vector3 (0f,0f,1f), 200.0f * Time.deltaTime * InputDevice.RightStickX, Space.World);

		//movement controls
		if (InputDevice.LeftStickX > 0f || InputDevice.LeftStickY > 0f) {
			Vector3 movement = new Vector3 (InputDevice.LeftStickX, InputDevice.LeftStickY, 0.0f);
			rigidbody.velocity = movement * speed;

			//auto adjusts rotation according to plane
			transform.localRotation = Quaternion.Euler (0,0,PlanetManager.getAngleVector(transform.position));
		}

		//Debug.Log (PlanetManager.getAngleVector (transform.position));
		//rigidbody.position = new Vector3 
			//(
			//	Mathf.Clamp (rigidbody.position.x, boundary.xMin, boundary.xMax), 
			//	0.0f, 
			//	Mathf.Clamp (rigidbody.position.z, boundary.zMin, boundary.zMax)
			//);
	}
}
