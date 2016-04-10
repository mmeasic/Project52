using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

//Script to control some of the propierties of the game. 
public class GameController : MonoBehaviour {

	//Text to display information about the state of the game.
	public Text Gameovertext;

	//We will need both cameras to switch between them. 
	public GameObject maincamera;
	public GameObject secondaryCamera;

	//If true, the game is over. 
	private bool gameover;

	//Initilization... 
	void Start () {
		Gameovertext.text = "Playing...";
	}

	//Each frame...
	void FixedUpdate () {
		if (gameover) {

			//Reset Scene... 
			if (Input.GetKeyDown (KeyCode.R)) {
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
				gameover = false;
			}

		}
	}

	//Function called when the game ends due to conditions in other algorithms.
	public void Gameover () {
		gameover = true;
		Gameovertext.text = "Game Over, Press [R] to reset";
	}

	// Witch between the main camera and the secondary camera (Or Game Over Camera). 
	public void SwitchCamera () {
		secondaryCamera.SetActive (true);
		secondaryCamera.transform.position = maincamera.transform.position;
		secondaryCamera.transform.rotation = maincamera.transform.rotation;
	}
}
