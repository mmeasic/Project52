using UnityEngine;
using System.Collections;

//Script to control de Manual Camera.
//The camera is controlled automatically by default when the player moves. 
public class CameraController : MonoBehaviour {

	//Spaceship Object
	public GameObject player;

	//Speed of rotation of the camera. 
	public float cameraSensibility;

	//When allowed, we control the manual camera. 
	private bool allowCamera;

	//Stores the Input value for the left mouse. 
	private float cameraHortizontal;

	private UIController uiController;

	//Initialization... 
	void Start () {

		GameObject uiControllerObject = GameObject.FindGameObjectWithTag ("UIController");
		if (uiControllerObject  != null) {
			uiController = uiControllerObject.GetComponent<UIController> ();
		} else {
			Debug.Log ("Cannot Find 'UIController' script");
		}

		allowCamera = false;

		uiController.refreshCameraInfo ("Camera Control Disabled");
	}

	//Each frame... 
	void FixedUpdate () {
		
		if (Input.GetKey (KeyCode.Mouse1)) {
			allowCamera = true;
			uiController.refreshCameraInfo ("Camera Control Enabled");
		} else {
			allowCamera = false;
			uiController.refreshCameraInfo ("Camera Control Disabled");
		}

		if (allowCamera) {
			cameraHortizontal = Input.GetAxis ("Mouse X");
			transform.RotateAround (player.transform.position, Vector3.up, (cameraHortizontal * cameraSensibility));
		}
	}
}
