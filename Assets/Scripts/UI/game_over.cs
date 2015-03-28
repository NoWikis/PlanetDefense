using UnityEngine;
using System.Collections;
using InControl;

public class game_over : MonoBehaviour {
	
	public int playerNum;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		InputDevice inputDevice = (InputManager.Devices.Count > playerNum) ? InputManager.Devices[playerNum] : null;
		if (inputDevice == null)
		{
			// If no controller exists for this cube, just make it translucent.
			Debug.Log("no player");
			Destroy(this.gameObject);
		}
		
		else 
		{
			if (inputDevice.Action1) 
			{
				Debug.Log ("Pressing A");
				Application.LoadLevel ("Title");
			}
		}
	}
}
