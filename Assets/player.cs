using UnityEngine;
using System.Collections;
using InControl;

public class player : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		var InputDevice = InputManager.ActiveDevice;
		transform.Rotate (Vector3.down, 500.0f * Time.deltaTime * InputDevice.LeftStickX, Space.World);
		transform.Rotate (Vector3.right, 500.0f * Time.deltaTime * InputDevice.LeftStickY, Space.World);

	}
}
