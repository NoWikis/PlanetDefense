using UnityEngine;
using System.Collections;

public class LaserPhysics2 : MonoBehaviour {
	
	public GameObject player;
	public GameObject planet;
	
	public float ray_length;
	public float ray_radius;
	public LayerMask collisionMask;
	public LayerMask collisionMask2;
	public LayerMask collisionMask3;
	
	private BoxCollider collider;
	private Vector3 size;
	private Vector3 center;
	
	private float skin = .005f;

	Ray ray;
	RaycastHit hit;


	float angle;

	float x;
	float y;
	
	// Use this for initialization
	void Start () {
		collider = GetComponent<BoxCollider> ();
		planet = GameObject.FindGameObjectWithTag ("Planet");
		
		float deltaX = transform.position.x - planet.transform.position.x;
		float deltaY = transform.position.y - planet.transform.position.y;
		
		angle = Mathf.Atan2 (deltaY, deltaX);
		
		Debug.Log ("Angle: " + angle * Mathf.Rad2Deg);
		//player = GameObject.FindGameObjectWithTag ("GameController");

		x = transform.position.x;
		y = transform.position.y;
		
	}

	void Awake() {

	}
	
	// Update is called once per frame
	void Update () {
		size = transform.localScale;
		center = collider.center;
	}
	
	void LateUpdate() {


		//ray = new Ray(transform.position, new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)));

		//float x = transform.position.x;


		ray = new Ray(new Vector2(x, y), new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)));

		Debug.DrawRay(ray.origin, ray.direction);

		ray_length = size.x;

		if(ray_length == 0)
			ray_length = 1;

		ray_radius = size.y / 2;

		if(ray_radius == 0)
			ray_radius = 0.1f;


		if(Physics.SphereCast (ray, 0.2f, out hit, ray_length, collisionMask)) {
			if (hit.transform.gameObject.GetComponent<Health>())
				hit.transform.gameObject.GetComponent<Health>().takeDamage(25);

		}

		if(Physics.SphereCast (ray, 0.2f, out hit, ray_length, collisionMask2)) {

			Debug.Log("hit with aliens");

			if(hit.transform.gameObject.CompareTag("miner"))
				hit.transform.gameObject.GetComponent<miner>().hp--;

			if(hit.transform.gameObject.CompareTag("starFighter"))
				hit.transform.gameObject.GetComponent<StarFighter>().Health = 0;

			if(hit.transform.gameObject.CompareTag("starTurret"))
				hit.transform.gameObject.GetComponent<StarTurret>().Health = 0;

			if(hit.transform.gameObject.CompareTag("starPlanet"))
				hit.transform.gameObject.GetComponent<AlienPlanet>().health.takeDamage(999);

			
		}

		if(Physics.SphereCast (ray, 0.2f, out hit, ray_length, collisionMask3)) {
			Debug.Log("hit with mine");
			Destroy(hit.transform.gameObject);
			
			
		}
		
	}

}