using UnityEngine;
using System.Collections;

//Script to control de Manual Camera.
//The camera is controlled automatically by default when the player moves. 
public class CameraController : MonoBehaviour {

	//Spaceship Object
	public GameObject player;

	//Speed of rotation of the camera. 
	public float cameraSensibility;

	//Stores the Input value for the left mouse. 
	private float cameraHortizontal;

	//Each frame... 
	void FixedUpdate () {

		if (Input.GetKey (KeyCode.Mouse1)) {
			cameraHortizontal = Input.GetAxis ("Mouse X");
			transform.RotateAround (player.transform.position, player.transform.up, (cameraHortizontal * cameraSensibility));
		}
	}
}
