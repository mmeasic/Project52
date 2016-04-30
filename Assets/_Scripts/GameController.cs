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
	public GameObject asteroid1;


	public int MaxAsteroids;
	public int direction=1;

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
		gameover = false;
		canbegin = true;

		pickupX = pickup.transform.position.x;
		pickupZ = pickup.transform.position.z;

		GameObject uiControllerObject = GameObject.FindGameObjectWithTag ("UIController");
		if (uiControllerObject  != null) {
			uiController = uiControllerObject.GetComponent<UIController> ();
		}

		actualScore = 0;
		actualTimeLimit = 0;

		uiController.refreshGameInfo ("Game ready to Begin, Press [B] to begin");
		uiController.refreshScore (actualScore);
		uiController.refreshTimer ((int) actualTimeLimit);
		uiController.refreshPickupPosition ((int) pickupZ, (int) pickupX);

		SpawnFirstWave();
	}

	//Each frame...
	void FixedUpdate () {
		GameObject[] AsteroidCount;
		AsteroidCount = GameObject.FindGameObjectsWithTag("Asteroid");
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
			GameObject explosion = (GameObject) Instantiate (playerExplosion, player.transform.position, player.transform.rotation);
			explosion.transform.parent = GameObject.FindGameObjectWithTag ("Explosions").transform;
			Destroy (player);
			Gameover ();
		}

		if (actualTimeLimit > 0.0f) actualTimeLimit = actualTimeLimit - timerReductionVelocity;
		uiController.refreshTimer ((int) actualTimeLimit);
		
		if (MaxAsteroids > AsteroidCount.Length) {
			createAsteroid ();
		}
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
		actualScore += (int) actualTimeLimit;
		uiController.refreshScore (actualScore);
		changePickUpPosition ();
	}
		
	public void createAsteroid(){
		float newAsteroidX = Random.Range(minX,maxX);
		float newAsteroidZ = Random.Range(minZ,maxZ);

		GameObject ast = null;

		direction = (int) Random.Range(1,5);
		if (direction == 1) {
			Vector3 spawnPosition = new Vector3 (newAsteroidX, 0.0f, Random.Range(710,720));
			Quaternion spawnRotation = Quaternion.identity;
			ast = (GameObject)Instantiate (asteroid1, spawnPosition, spawnRotation);
			ast.transform.parent = GameObject.FindGameObjectWithTag ("Asteroids").transform;
		} else if (direction == 2) {
			Vector3 spawnPosition = new Vector3 (newAsteroidX, 0.0f, Random.Range(-710,-720));
			Quaternion spawnRotation = Quaternion.identity;
			ast = (GameObject)Instantiate (asteroid1, spawnPosition, spawnRotation);
			ast.transform.parent = GameObject.FindGameObjectWithTag ("Asteroids").transform;
		} else if (direction == 3) {
			Vector3 spawnPosition = new Vector3 (Random.Range(-710,-720), 0.0f, newAsteroidZ);
			Quaternion spawnRotation = Quaternion.identity;
			ast = (GameObject)Instantiate (asteroid1, spawnPosition, spawnRotation);
			ast.transform.parent = GameObject.FindGameObjectWithTag ("Asteroids").transform;
		} else if (direction == 4) {
			Vector3 spawnPosition = new Vector3 (Random.Range(710,720), 0.0f, newAsteroidZ);
			Quaternion spawnRotation = Quaternion.identity;
			ast = (GameObject)Instantiate (asteroid1, spawnPosition, spawnRotation);
			ast.transform.parent = GameObject.FindGameObjectWithTag ("Asteroids").transform;
		}
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

	private void SpawnFirstWave(){
		for (int i = 0; i < MaxAsteroids; i++)
		{
			createAsteroid ();
		}
	}


}
