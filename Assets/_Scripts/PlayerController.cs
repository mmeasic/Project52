using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public float min_speed;
	public float max_speed;
	public float max_energy;
	public float rotation_speed;
	public float acceleration_factor;
	public float deceleration_factor;
	public float energy_consumption_factor;
	public float energy_reload_factor;
	public int max_player_lifes;

	//Spaceship effects
	public GameObject turboparticles;
	public GameObject smoke1;
	public GameObject smoke2;

	private float current_speed;
	private float current_energy;
	private int current_number_of_lifes; 

	private UIController uiController;

	//Initialization...
	void Start () {

		GameObject uiControllerObject = GameObject.FindGameObjectWithTag ("UIController");
		if (uiControllerObject  != null) {
			uiController = uiControllerObject.GetComponent<UIController> ();
		}

		current_energy = max_energy;
		current_speed = min_speed;
		current_number_of_lifes = max_player_lifes;

		//Refreshing the information from Player IU.
		uiController.refreshPlayerInfo (current_energy, current_number_of_lifes, current_speed);
	}

	//Each frame...
	void FixedUpdate ()
	{
		
		//We get the input to decide the rotation of the spaceship. 
		float rotation_horizontal = Input.GetAxis ("Horizontal");

		//We call this function to control the acceleration (And in consequence, the speed) of the spaceship.
		acceleration_control ();

		//We define the velocity of the spaceship in the forward direction. 
		GetComponent<Rigidbody>().velocity = (transform.forward * current_speed);

		//We define the rotation of the spaceship taking in account the rotation speed. 
		transform.Rotate (0.0f, rotation_horizontal * rotation_speed, 0.0f);	

		//Refreshing the information from Player IU.
		uiController.refreshPlayerInfo (current_energy, current_number_of_lifes, current_speed);

		//TEMPORAL FUNCTION
		uiController.refreshSpaceshipPosition((int) transform.position.z, (int) transform.position.x);
	}


	private void acceleration_control () {

		bool turbo_activated = Input.GetKey (KeyCode.LeftShift);
		turboparticles.SetActive (turbo_activated);

		if (current_energy > 0.0f && turbo_activated) {

			current_speed += acceleration_factor;

			current_energy = current_energy - energy_consumption_factor;

		} else {
			
			current_speed -= deceleration_factor;

			if (!turbo_activated) current_energy = current_energy + energy_reload_factor;
			turboparticles.SetActive (false);
		}

		//Boundary Control
		if (current_speed >= max_speed) current_speed = max_speed;
		if (current_speed <= min_speed) current_speed = min_speed;
		if (current_energy >= max_energy) current_energy = max_energy;
	}

	//We check the state of the live and reduces in one the number of lifes if we detected a collision. 
	public bool checkLive (bool collision) {
		
		if (collision) current_number_of_lifes--;

		//Refreshing the information from Player IU.
		uiController.refreshPlayerInfo (current_energy, current_number_of_lifes, current_speed);

		if (current_number_of_lifes == 0) {
			
			//Refreshing the information from Player IU.
			uiController.refreshPlayerInfo (current_energy, current_number_of_lifes, current_speed);

			return true;

		} else {
			
			//Smoke effects for different number of lifes.
			if (current_number_of_lifes > 1 && current_number_of_lifes < max_player_lifes) smoke1.SetActive (true);
			if (current_number_of_lifes == 1) smoke2.SetActive (true);

			return false;
		}
	}
}