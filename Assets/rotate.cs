using UnityEngine;
using System.Collections;

public class rotate : MonoBehaviour {


	public GameObject block;

	private float complete_speed = 1;


	private float angle = 0;
	private float radius = 1;

	private float speed = (2 * Mathf.PI) / 5; //2*PI in degress is 360, so you get 5 seconds to complete a circle

	
	// Use this for initialization
	void Start () {
		//complete_speed = block.GetComponent<rotate_master> ().complete_speed;
		complete_speed = transform.root.gameObject.GetComponent<speed_control> ().complete_speed;
		speed = (2 * Mathf.PI) / complete_speed;
		float deltaY = transform.position.y;
		float deltaX = transform.position.x;

		angle = Mathf.Atan2 (deltaY, deltaX) ;



		radius = Vector3.Distance (Vector3.zero, transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		angle += speed*Time.deltaTime; //if you want to switch direction, use -= instead of +=
		
		Vector2 temp = new Vector2 (0, 0);
		temp.x = Mathf.Cos(angle)*radius;
		temp.y = Mathf.Sin(angle)*radius;
		
		transform.position = temp;
		//transform.LookAt(Vector3.zero);
		
		
	}
}
