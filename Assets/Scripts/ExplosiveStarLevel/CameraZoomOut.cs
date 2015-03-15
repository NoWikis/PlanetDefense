using UnityEngine;
using System.Collections;

public class CameraZoomOut : MonoBehaviour {
	public float panSpeed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Camera.main.orthographicSize < 44)
			Camera.main.orthographicSize += panSpeed*Time.deltaTime;
	}
}
