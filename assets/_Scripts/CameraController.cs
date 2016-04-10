using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//Script to control de Manual Camera.
//The camera is controlled automatically by default when the player moves. 
public class CameraController : MonoBehaviour {

	//Spaceship Object
	public GameObject player;

	//Text to display information about the use of the manual camera. 
	public Text info;

	//Speed of rotation of the camera. 
	public float cameraSensibility;

	//When allowed, we control the manual camera. 
	private bool allowCamera;

	//Stores the Input value for the left mouse. 
	private float cameraHortizontal;

	//Initialization... 
	void Start () {
		allowCamera = false;
		info.text = "Camera control disabled";
	}

	//Each frame... 
	void FixedUpdate () {
		
		if (Input.GetKey (KeyCode.Mouse1)) {
			allowCamera = true;
			info.text = "Camera control enabled";
		} else {
			allowCamera = false;
			info.text = "Camera control disabled";
		}

		if (allowCamera) {
			cameraHortizontal = Input.GetAxis ("Mouse X");
			transform.RotateAround (player.transform.position, Vector3.up, (cameraHortizontal * cameraSensibility));
		}
	}
}
