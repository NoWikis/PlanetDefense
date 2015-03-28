using UnityEngine;
using System.Collections;



// Controls basic asteroid appearance and behavior
// Asteroid health and mass are always the same value.
// TODO: determine escape velocity and remove if too far
// TODO: Collide with projectiles
public class AsteroidBehavior : MonoBehaviour {


	public enum SizeClass {
		Small,
		Medium,
		Large,
		Larger
	};


	// These are the default sizes for the asteroids.
	static float[] masses = new float[]{
		25f,   // Small
		50f,   // Medium
		75f,   // Large
		100f,  // Larger
	};

	float mass;
	SizeClass size = SizeClass.Medium;
	Rigidbody  physicsBase;

	Health health;




	// The minimum size 
	static float minimumSize = 1.5f;

	// Controls how mass is interpreted sizewise.
	static float sizePerMass = 20.2f / 100f;

	


	// Whether or not the asteroid can be damaged
	public bool indestructable = false;

	

	public float initialRotation = 0f;
	public float initialRotationalVelocity = 0f;



	//for items	
	public float fuel_spawn_chance = 1f;
	public GameObject fuel_p1;
	public GameObject fuel_p2;

	public float item_spawn_chance = 1f;
	public GameObject[] item_list;
	


	void Awake() {

		physicsBase = GetComponent<Rigidbody>();

		health = GetComponent<Health>();
		health.registerDamageCallback(ReadjustSize);
		if (indestructable) health.invincible = true;

		transform.rotation = Quaternion.Euler(0, 0, initialRotation);
		rigidbody.AddTorque(0, 0, initialRotationalVelocity);
	}



	// Use this for initialization
	void Start () {

		 

		setSizeClass (size);



		physicsBase.mass = mass;
		health.init (mass, mass);


		GetComponent<AsteroidMesh>().GenerateMesh();

		ReadjustSize(gameObject);
		if (!indestructable)
			GetComponent<SphereCollider>().radius = GetComponent<AsteroidMesh>().GetAverageRadius();

		if (GetComponent<Shadifier>())
			GetComponent<Shadifier>().Shadify();
	}
	
	// Update is called once per frame
	void Update () {


	}

	










	/* Collisions */

	public void setSizeClass(SizeClass newSize) {
		size = newSize;
		mass = masses [(int)size];
	}


	// Called upon taking damage (via Health.registerDamageCallback())
	void ReadjustSize(GameObject h) {
		if (indestructable)
						return;

		if (health.isDead()) {
			int spawn_item = Mathf.RoundToInt(Random.value * (item_list.Length - 1));
			float spawn_chance = Random.value;
			if(spawn_chance <= item_spawn_chance){
				GameObject o = (GameObject)Instantiate (item_list[spawn_item]);
				o.transform.position = transform.position;
			}
			
//			float spawn_fuel = Random.value;
//			if(spawn_fuel <= fuel_spawn_chance)
//			{
//				GameObject o = (GameObject)Instantiate (Random.value>.5f?fuel_p1 : fuel_p2);
//				o.transform.position = transform.position;
//				o.GetComponent<Rigidbody>().velocity = new Vector3 (1f, 2f, 0f);
//			}
			
			Destroy (gameObject);
			return;
		}

		physicsBase.mass = health.current ();
		float healthRatio = (physicsBase.mass*sizePerMass) * GetComponent<AsteroidMesh>().GetAverageRadius();
		float newScale =  healthRatio;
		if (newScale < minimumSize)
						newScale = minimumSize;


		transform.localScale  = new Vector3(newScale, newScale, newScale);


	}




































}
