using UnityEngine;
using System.Collections;

public class LaserPhysics : MonoBehaviour {

	public GameObject player;
	public GameObject planet;

	public float ray_length = 3f;
	public LayerMask collisionMask;
	public LayerMask collisionMask2;
	public LayerMask collisionMask3;

	private BoxCollider collider;
	private Vector3 size;
	private Vector3 center;

	private float skin = .005f;



	Ray ray;
	Ray ray2;
	RaycastHit hit;
	RaycastHit hit2;

	// Use this for initialization
	void Start () {
		collider = GetComponent<BoxCollider> ();
		planet = GameObject.FindGameObjectWithTag ("Planet");
		player = GameObject.FindGameObjectWithTag ("GameController");

	}
	
	// Update is called once per frame
	void Update () {
		size = transform.localScale;
		center = collider.center;
	}

	void LateUpdate() {
		Vector2 p = transform.position;


		for (int i = 0; i < 40; i++) {

			float deltaX = player.transform.position.x - planet.transform.position.x;
			float deltaY = player.transform.position.y - planet.transform.position.y;

			float real_angle = Mathf.Atan2 (deltaY, deltaX);
			float angle = Mathf.Atan2 (deltaY, deltaX) + 1.57079633f ;

			//float dir = Mathf.Sign(1f);

			float x = (p.x + center.x - size.x/2) + size.x/39 * i;
			float y = p.y + center.y +size.y/2;

			//Debug.Log(center);
			
			ray = new Ray(new Vector2(x, y), new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)));
			Debug.DrawRay(ray.origin, ray.direction);
			
			
			if(Physics.Raycast (ray, out hit, ray_length + skin, collisionMask)) {

				Destroy(hit.transform.gameObject);
				
				break;
			}
			
		}


		for (int i = 0; i < 3; i++) {
			float dir = Mathf.Sign (1f);
			float x = p.x + center.x + size.x / 2 * dir;
			float y = p.y + center.y - size.y / 2 + size.y / 2 * i;
			
			ray = new Ray (new Vector2 (x, y), new Vector2 (dir, 0));
			Debug.DrawRay (ray.origin, ray.direction);
			
			
			if (Physics.Raycast (ray, out hit, ray_length + skin, collisionMask)) {

				Destroy(hit.transform.gameObject);

				break;
			}

			
		}


	}
}
