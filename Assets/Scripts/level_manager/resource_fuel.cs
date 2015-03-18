using UnityEngine;
using System.Collections;

public class resource_fuel : MonoBehaviour {

	public GameObject		player;
	public Vector3 			initialVelocity = new Vector3 (0, 0, 0);
	public float			terminalVelocity;
	public int				fuelPlayerNum;
	public float 			topSpeed		=	20f;
	public float 			initSpeed		=	2f;

	Rigidbody  physicsBase;

	private float			stationary		=	1f;
	private float 			stationaryTimer	= 	0f;

	void Awake () {

	}
	

	// Update is called once per frame
	void Update () {
		
		if (fuelPlayerNum == 1) {
			player = GameObject.Find ("playerPrefab_1");
		}
		else {
			player = GameObject.Find ("playerPrefab_2");
		}
		stationaryTimer += Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position,player.transform.position,Time.deltaTime*initSpeed);
		Debug.Log (initSpeed);

		if (stationaryTimer > stationary) {
			if (topSpeed > initSpeed)
				initSpeed += 0.15f;
		}
	}

	void OnCollisionEnter(Collision c){
		//		print ("YO");
		//if(c.gameObject.CompareTag ("player")) {
			//Destroy(gameObject);
		//}
		
	}
}
