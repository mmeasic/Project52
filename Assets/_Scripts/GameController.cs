using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

//Script to control some of the propierties of the game. 
public class GameController : MonoBehaviour {

	//We will need both cameras to switch between them. 
	public GameObject maincamera;
	public GameObject secondaryCamera;
	public GameObject pickup;
	public GameObject player;
	public GameObject playerExplosion;
	public GameObject hazard;

	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;

	public float maxZ;
	public float maxX;
	public float minZ;
	public float minX;

	public float timerReductionVelocity;
	public float difficultyFactor;

	private float pickupZ;
	private float pickupX;

	private int actualScore;
	private float actualTimeLimit;

	//If true, the game is over. 
	private bool gameover;
	//If true, we can begin the game. 
	private bool canbegin;

	private UIController uiController;

	//Initilization... 
	void Start () {
		SpawnWaves();
		gameover = false;
		canbegin = true;

		pickupX = pickup.transform.position.x;
		pickupZ = pickup.transform.position.z;

		GameObject uiControllerObject = GameObject.FindGameObjectWithTag ("UIController");
		if (uiControllerObject  != null) {
			uiController = uiControllerObject.GetComponent<UIController> ();
		} else {
			Debug.Log ("Cannot Find 'UIController' script");
		}

		actualScore = 0;
		actualTimeLimit = 0;

		uiController.refreshGameInfo ("Game ready to Begin, Press [B] to begin");
		uiController.refreshScore (actualScore);
		uiController.refreshTimer ((int) actualTimeLimit);
		uiController.refreshPickupPosition ((int) pickupZ, (int) pickupX);

	}

	//Each frame...
	void FixedUpdate () {

		//Begin Game... 
		if (canbegin && Input.GetKeyDown (KeyCode.B)) {
			canbegin = false;
			uiController.refreshGameInfo ("Playing...");
			uiController.refreshScore (actualScore);
			changePickUpPosition ();
		}

		//Reset Scene... 
		if (gameover && Input.GetKeyDown (KeyCode.R)) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			gameover = false;
		}

		if (!canbegin && !gameover && actualTimeLimit < 1.0f) {
			Instantiate (playerExplosion, player.transform.position, player.transform.rotation);
			Destroy (player);
			Gameover ();
		}

		if (actualTimeLimit > 0.0f) actualTimeLimit = actualTimeLimit - timerReductionVelocity;
		uiController.refreshTimer ((int) actualTimeLimit);
	}

	//Function called when the game ends due to conditions in other algorithms.
	public void Gameover () {
		gameover = true;
		SwitchCamera();
		uiController.refreshGameInfo ("Game Over, Press [R] to reset");
	}

	// Witch between the main camera and the secondary camera (Or Game Over Camera). 
	private void SwitchCamera () {
		secondaryCamera.SetActive (true);
		secondaryCamera.transform.position = maincamera.transform.position;
		secondaryCamera.transform.rotation = maincamera.transform.rotation;
		maincamera.SetActive (false);
	}

	public void getPickup () {
		actualScore++;
		uiController.refreshScore (actualScore);
		changePickUpPosition ();
	}


	private void changePickUpPosition () {

		float newPickupX = Random.Range(minX,maxX);
		float newPickupZ = Random.Range(minZ,maxZ);
		float inverterX = Random.value;
		float inverterZ = Random.value;
		if (inverterX < 0.5) newPickupX = -newPickupX;
		if (inverterZ < 0.5) newPickupZ = -newPickupZ;

		pickup.transform.position = new Vector3 (newPickupX, 0.0f, newPickupZ);

		pickupX = newPickupX;
		pickupZ = newPickupZ;

		uiController.refreshPickupPosition ((int) pickupZ, (int) pickupX);

		//Set Timer
		float xDifference = Mathf.Abs(player.transform.position.x - pickup.transform.position.x);
		float yDifference = Mathf.Abs(player.transform.position.z - pickup.transform.position.z);
		actualTimeLimit = (xDifference + yDifference)*difficultyFactor;
	}

	IEnumerator SpawnWaves(){
		yield return new WaitForSeconds (startWait);
		while (true)
		{
			for (int i = 0; i < hazardCount; i++)
			{
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);

			}
		}

	}
}
