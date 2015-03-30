﻿using UnityEngine;
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
		float deltaX = player.transform.position.x - planet.transform.position.x;
		float deltaY = player.transform.position.y - planet.transform.position.y;
		
		float angle = Mathf.Atan2 (deltaY, deltaX);

		//ray = new Ray(transform.position, new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)));

		float x = transform.position.x;
		float y = transform.position.y;

		ray = new Ray(new Vector2(x, y), new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)));

		Debug.DrawRay(ray.origin, ray.direction);

		ray_length = size.x/2;

		if(ray_length == 0)
			ray_length = 1;

		ray_radius = size.y / 2;

		if(ray_radius == 0)
			ray_radius = 0.1f;


		if(Physics.SphereCast (ray, 0.2f, out hit, ray_length, collisionMask)) {
			
			Destroy(hit.transform.gameObject);

		}
		
	}

}