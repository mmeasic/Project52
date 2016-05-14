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
	public GameObject asteroid2;
	public GameObject asteroid3;

	public int MaxAsteroids;

	public float maxZ;
	public float maxX;
	public float minZ;
	public float minX;

	public float timerReductionVelocity;
	public float minimunTimeLimit;
	public int asteroid_direction;
	public int difficultyFactor;

	private int actualScore;
	private float actualTimeLimit;
	private float normalTime;

	//If true, the game is over. 
	private bool gameover;

	//If true, the game is paused. 
	private bool gamepaused;

	private UIController uiController;

	//Initilization... 
	void Start () {
		gameover = false;
		normalTime = Time.timeScale;

		difficultyFactor = SimulationController.difficulty;

		MaxAsteroids = (int) difficultyFactor * MaxAsteroids;

		GameObject uiControllerObject = GameObject.FindGameObjectWithTag ("UIController");
		if (uiControllerObject  != null) {
			uiController = uiControllerObject.GetComponent<UIController> ();
		}

		actualScore = 0;
		actualTimeLimit = 0;

		uiController.refreshScore (actualScore);
		uiController.refreshTimer ((int) actualTimeLimit);

		SpawnFirstWave();
	}

	//Each frame...
	void FixedUpdate () {

		//I have to put it here because it doesn't work in the start... 
		if (pickup.transform.position == new Vector3 (0.0f,0.0f,0.0f)) changePickUpPosition();

		if (!gameover) {

			//Pause Game...
			if (Input.GetKeyDown (KeyCode.Escape)) PauseGame ();

			if (actualTimeLimit < 1.0f) {
				GameObject explosion = (GameObject)Instantiate (playerExplosion, player.transform.position, player.transform.rotation);
				explosion.transform.parent = GameObject.FindGameObjectWithTag ("Explosions").transform;
				Destroy (player);
				Gameover ();
			} else {
				actualTimeLimit = actualTimeLimit - timerReductionVelocity;
			}

			uiController.refreshTimer ((int) actualTimeLimit);
		}



		//Asteroid Generation

		GameObject[] AsteroidCount;
		AsteroidCount = GameObject.FindGameObjectsWithTag("Asteroid");

		for (int i = 0; i < (MaxAsteroids - AsteroidCount.Length); i++) {
			createAsteroid (false);
		}

	}

	//Function called when the game ends due to conditions in other algorithms.
	public void Gameover () {
		gameover = true;
		uiController.showGameOverPanel ();
		SwitchCamera ();
	}

	//Function called to pause the game. 
	public void PauseGame () {
		uiController.showPausePanel ();
		Time.timeScale = 0.0f;
	}

	//Function called to continue the game. 
	public void ContinueGame () {
		GetComponent<AudioSource>().Play ();
		uiController.hidePausePanel ();
		Time.timeScale = normalTime;
	}

	//Function called when we want to return to the main screen.
	public void Returnmainscreen() {
		Time.timeScale = normalTime;
		SceneManager.LoadScene("Animation", LoadSceneMode.Single);
	}

	//Function called when we want to reset the game.
	public void Resetgame() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	//Function called when we want to quit the game. 
	public void Quitgame() {
		Application.Quit ();
	}

	// Witch between the main camera and the secondary camera (Or Game Over Camera). 
	private void SwitchCamera () {
		secondaryCamera.SetActive (true);
		secondaryCamera.transform.position = maincamera.transform.position;
		secondaryCamera.transform.rotation = maincamera.transform.rotation;
		maincamera.SetActive (false);
	}

	public void getPickup () {
		actualScore += (int) actualTimeLimit * difficultyFactor;
		uiController.refreshScore (actualScore);
		changePickUpPosition ();
	}
		
	public void createAsteroid(bool first_wave){
		float newAsteroidX = Random.Range(minX,maxX);
		float newAsteroidZ = Random.Range(minZ,maxZ);

		Vector3 spawnPosition = new Vector3 (newAsteroidX, 0.0f, newAsteroidZ);;
		Quaternion spawnRotation = Random.rotation;

		GameObject ast = null;
		GameObject ast_type = null;

		float asteroid_type = (int) Random.Range(1,4);

		if (asteroid_type == 1) ast_type = asteroid1;
		if (asteroid_type == 2) ast_type = asteroid2;
		if (asteroid_type == 3) ast_type = asteroid3;

		asteroid_direction = (int) Random.Range(1,5);

		if (first_wave) {
			float inverterX = Random.value;
			float inverterZ = Random.value;
			if (inverterX < 0.5) newAsteroidX = -newAsteroidX;
			if (inverterZ < 0.5) newAsteroidZ = -newAsteroidZ;
			spawnPosition = new Vector3 (newAsteroidX, 0.0f, newAsteroidZ);
		} else {
			if (asteroid_direction <= 1) spawnPosition = new Vector3 (newAsteroidX, 0.0f, Random.Range(800,850));
			if (asteroid_direction == 2) spawnPosition = new Vector3 (newAsteroidX, 0.0f, Random.Range(-800,-850));
			if (asteroid_direction == 3) spawnPosition = new Vector3 (Random.Range(-800,-850), 0.0f, newAsteroidZ);
			if (asteroid_direction >= 4) spawnPosition = new Vector3 (Random.Range(800,850), 0.0f, newAsteroidZ);
		}

		ast = (GameObject) Instantiate (ast_type, spawnPosition, spawnRotation);

		float r = ((int) Random.Range (1, 20))/10;
		ast.transform.localScale += new Vector3 (r, r, r);

		ast.transform.parent = GameObject.FindGameObjectWithTag ("Asteroids").transform;
	}

	private void changePickUpPosition () {

		float newPickupX = Random.Range(minX,maxX);
		float newPickupZ = Random.Range(minZ,maxZ);
		float inverterX = Random.value;
		float inverterZ = Random.value;
		if (inverterX < 0.5) newPickupX = -newPickupX;
		if (inverterZ < 0.5) newPickupZ = -newPickupZ;

		pickup.transform.position = new Vector3 (newPickupX, 0.0f, newPickupZ);

		//Set Timer
		float xDifference = Mathf.Abs(player.transform.position.x - pickup.transform.position.x);
		float yDifference = Mathf.Abs(player.transform.position.z - pickup.transform.position.z);
		actualTimeLimit = (xDifference + yDifference)/(difficultyFactor*difficultyFactor);

		if (actualTimeLimit < minimunTimeLimit) actualTimeLimit = minimunTimeLimit;
	}

	private void SpawnFirstWave(){
		for (int i = 0; i < MaxAsteroids; i++)
		{
			createAsteroid (true);
		}
	}


}
