using UnityEngine;
using System.Collections;

public class gravitationalForceOnPlayer : MonoBehaviour {


	public float mass;
	public float gravityStrength;
	public float gravityRangeInv;

	// Mass is accessed differently
	public GameObject planet1, planet2, planet3, planet4, planet5, planet6, planet7, planet8;

	private AxialTiltsAndRotation planetComponent;
	private float massPlanet1, massPlanet2, massPlanet3, massPlanet4, massPlanet5, massPlanet6, massPlanet7, massPlanet8;

	float distanceX;		// distance between the target and the planet in x-direction
	float distanceZ;		// distance between the target and the planet in y-direction
	float distance;			// distance between the target and the planet
	Vector3 planetaryForces;
	private Rigidbody rb;


	Vector3 PlanetaryForce (GameObject planet, float massPlanet){
		distanceX = transform.position.x - planet.transform.position.x;
		distanceZ = transform.position.z - planet.transform.position.z;
		Vector3 distanceVec = new Vector3 (distanceX, 0.0f, distanceZ);
		distance = Mathf.Sqrt (distanceX * distanceX + distanceZ * distanceZ);
		// force on the target will be:
		Vector3 force = -gravityStrength * mass * massPlanet / (distance* Mathf.Pow(distance,gravityRangeInv)) * distanceVec;
		//Vector3 shouldBeChanged = new Vector3 (0.0f, 0.0f, 0.0f);
		return force;
	}


	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		massPlanet1 = planet1.GetComponent<AxialTiltsAndRotation> ().mass;
		massPlanet2 = planet2.GetComponent<AxialTiltsAndRotation> ().mass;
		massPlanet3 = planet3.GetComponent<AxialTiltsAndRotation> ().mass;
		massPlanet4 = planet4.GetComponent<AxialTiltsAndRotation> ().mass;
		massPlanet5 = planet5.GetComponent<AxialTiltsAndRotation> ().mass;
		massPlanet6 = planet6.GetComponent<AxialTiltsAndRotation> ().mass;
		massPlanet7 = planet7.GetComponent<AxialTiltsAndRotation> ().mass;
		massPlanet8 = planet8.GetComponent<AxialTiltsAndRotation> ().mass;
		// massPlanet1 = planetComponent.mass;
		// Need to access to the mass of planet1
	}
	
	// FixedUpdate is called once per frame
	void FixedUpdate () {
		planetaryForces = PlanetaryForce (planet1, massPlanet1) 
						+ PlanetaryForce (planet2, massPlanet2)
						+ PlanetaryForce (planet3, massPlanet3)
						+ PlanetaryForce (planet4, massPlanet4)
						+ PlanetaryForce (planet5, massPlanet5)
						+ PlanetaryForce (planet6, massPlanet6)
						+ PlanetaryForce (planet7, massPlanet7)
						+ PlanetaryForce (planet8, massPlanet8);
		rb.AddForce (planetaryForces);
		//print (planetaryForces);
	}


}
