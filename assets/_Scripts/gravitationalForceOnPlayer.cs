using UnityEngine;
using System.Collections;

public class gravitationalForceOnPlayer : MonoBehaviour {


	public float mass;
	public float gravityStrength;
	public GameObject planet1;
	public GameObject planet2;
	public GameObject planet3;


	float distanceX;		// distance between the target and the planet in x-direction
	float distanceZ;		// distance between the target and the planet in y-direction
	float distance;			// distance between the target and the planet
	Vector3 planetaryForces;
	private Rigidbody rb;


	Vector3 PlanetaryForce (GameObject planet){
		//float massPlanet = ...
		distanceX = transform.position.x - planet.transform.position.x;
		distanceZ = transform.position.z - planet.transform.position.z;
		Vector3 distanceVec = new Vector3 (distanceX, 0.0f, distanceZ);
		distance = Mathf.Sqrt (distanceX * distanceX + distanceZ * distanceZ);
		// force on the target will be:
		// Vector3 force = gravityStrength * mass * massPlanet / distance^3 * distanceVec;
		Vector3 shouldBeChanged = new Vector3 (0.0f, 0.0f, 0.0f);
		return shouldBeChanged;
	}


	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		// Need to access to the mass of planet1
	}
	
	// FixedUpdate is called once per frame
	void FixedUpdate () {
		planetaryForces = PlanetaryForce(planet1) + PlanetaryForce(planet2) + PlanetaryForce(planet3); // + ...
		rb.AddForce (planetaryForces);
	}


}
