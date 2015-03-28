using UnityEngine;
using System.Collections;

public class PowerUps : MonoBehaviour {

	public enum powerTypes{
		rocketBoost,
		superShot,
		speedUp,
		none
	};

	public powerTypes powerType = powerTypes.none; 
	public GameObject planet;
	public float speed;

	// Use this for initialization
	void Start () {
		determineType ();
	}

	void determineType(){
		if (gameObject.name.Contains ("rocketBoost"))
			powerType = powerTypes.rocketBoost;
		else if (gameObject.name.Contains ("superShot"))
			powerType = powerTypes.superShot;
		else if (gameObject.name.Contains ("speedUp"))
			powerType = powerTypes.speedUp;
		else
			powerType = powerTypes.none;
	}
	// Update is called once per frame
	void Update () {		
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, planet.transform.position, step);
	}

	void OnCollisionEnter(Collision c){
		if (c.gameObject.CompareTag ("Planet")) {
			Destroy(gameObject);
		}
	}

//	public string getPower(){
//	switch (powerType) {
//		case powerTypes.rocketBoost:
//			return "rocketBoost";
//		case powerTypes.speedUp:
//			return "speedUp";
//		case powerTypes.superShot:
//			return "superShot";
//		case default:
//			return "none";
//			break;
//		}
//	}
}
