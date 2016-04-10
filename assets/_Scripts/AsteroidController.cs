using UnityEngine;
using System.Collections;

//Just a simple script to make the asteroids of the "Main" scene rotate. 
public class AsteroidController : MonoBehaviour {

	//Speed of rotation of the asteroid
	public float speed;

	//Each frame...
	void Update ()
	{
		//Rotation of the asteroid
		transform.Rotate (new Vector3 (0, speed, 0) * Time.deltaTime);
	}
}
