using UnityEngine;
using System.Collections;

public class AxialTiltsAndRotation : MonoBehaviour {

    // Variables to change
    public float speedPlanetaryRotation;
    public float axialTilt;
    // Orbitational ellipses of the planets, relative to the Sun
    public GameObject sun;
    public float aRadius;
    public float bRadius;
	public float speedOrbitRotation;

	public float mass;



    // Private variables
    float X;		
    float Z;
    float alpha;
    Vector3 sunPosition;

	// counter for the time in ms.
	float countTime;
	float initialDistanceToSun;
	// initial Angle to the x-axis. Need for the equation of the orbitals.
	float initialAngleToX;
	// starting postion of the planets. y is always zero.
	float startingPositionX;
	float startingPositionZ;


    // Use this for initialization
    void Start () {
        // Get the position of the Sun
        sunPosition = sun.transform.position;
        // Get the eulerAngles for the planetary object
        Vector3 rotationVector = transform.rotation.eulerAngles;
        // Change the axial tilt of the planet
        rotationVector.x = axialTilt;
        transform.eulerAngles = rotationVector;

		// we have to make the asumption, that all ellipses are aligned in the same direction.
		// will be too complicated otherwise.
		countTime = 0;
		startingPositionX = transform.position.x;		
		startingPositionZ = transform.position.z;
		initialDistanceToSun = Mathf.Sqrt (startingPositionX * startingPositionX + startingPositionZ * startingPositionZ);
		initialAngleToX = Mathf.Acos (startingPositionX / initialDistanceToSun);
	}
	
	// Update is called once per frame
	void Update () {

        // Rotate around the Sun
//        alpha += speedOrbitRotation*20*Time.deltaTime;
//        X = sunPosition.x + (aRadius * Mathf.Cos(alpha));
//        Y = sunPosition.y + (bRadius * Mathf.Sin(alpha));
//        transform.position = new Vector3(X, 0, Y);

		countTime += Time.deltaTime;

		X = aRadius * Mathf.Cos (countTime * speedOrbitRotation + initialAngleToX);
		Z = bRadius * Mathf.Sin (countTime * speedOrbitRotation + initialAngleToX);
		transform.position = new Vector3 (X, 0.0f, Z);


        // Rotate around itself
        if (speedPlanetaryRotation == 0.0f) {
            speedPlanetaryRotation = 1.0f;
        }
        transform.Rotate(Vector3.down, Time.deltaTime * speedPlanetaryRotation);


    }
}


//-----------------By-Raj---------------//
// In my part of the code, I am always asuming the sun to be at (0,0,0). 
// I changed the float variable Y to Z. It is just a convention in Unity. We need to talk about that.
//-----------------End-Raj---------------//