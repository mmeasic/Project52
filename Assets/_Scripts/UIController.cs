using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	//Player UI
	public Text info_energy;
	public Text info_lifes;
	public Text info_speed;
	public Text score_number;
	public Text timer_number;
	public Text pickup_info;
	public Text spaceship_info;
	public Slider energy_slider;

	//Camera UI
	public Text info_camera;

	//Game UI
	public Text game_info;

	public void refreshPlayerInfo (float current_energy, float current_number_of_lifes, float current_speed) {
		info_energy.text = "Energy: " + ((int) current_energy).ToString() + "%";
		info_lifes.text = "Lifes: " + current_number_of_lifes.ToString ();
		info_speed.text = "Speed: " + ((int) current_speed).ToString ();
		energy_slider.normalizedValue = (((int) current_energy) / 100.0f);
	}

	public void refreshCameraInfo (string info) {
		info_camera.text = info;
	}

	public void refreshGameInfo (string info) {
		game_info.text = info;
	}

	public void refreshScore (int score) {
		score_number.text = score.ToString();
	}

	public void refreshPickupPosition (int Z, int X) {
		pickup_info.text = "Pickup (X,Z) = (" + X.ToString () + "," + Z.ToString () + ")"; 
	}

	public void refreshSpaceshipPosition (int Z, int X) {
		spaceship_info.text = "Spaceship (X,Z) = (" + X.ToString () + "," + Z.ToString () + ")"; 
	}

	public void refreshTimer (int timer) {

		timer_number.text = timer.ToString ();

	}
}
