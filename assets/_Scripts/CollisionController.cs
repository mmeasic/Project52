using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//Script to control the effect of the collisions. 
public class CollisionController : MonoBehaviour {

	//Explosion of an asteroid. 
	public GameObject explosion;

	//Explosion of the player. 
	public GameObject playerExplosion;

	//We will use the Game Controller and the Player Controller Scripts. 
	private GameController gameController;
	private PlayerController playerController;

	//Initialization... 
	void Start() {
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController> ();
		} else {
			Debug.Log ("Cannot Find 'GameController' script");
		}
		GameObject playerControllerObject = GameObject.FindGameObjectWithTag ("Player");
		if (playerControllerObject != null) {
			playerController = playerControllerObject.GetComponent<PlayerController> ();
		} else {
			Debug.Log ("Cannot Find 'PlayerController' script");
		}
	}

	//On Collision... 
	void OnTriggerEnter(Collider other) 
	{

		//Collision with the asteroids...
		if (other.tag == "Asteroid") {

			//Destruction of the asteroid.
			Instantiate (explosion, other.transform.position, other.transform.rotation);
			Destroy(other.gameObject);

			//We check if we have life left... 
			if (playerController.checkLive (true)) {

				//We need to switch the camera used because the main camera depends on the player object. 
				gameController.SwitchCamera ();

				//Destruction of the player.
				Instantiate (playerExplosion, transform.position, transform.rotation);
				Destroy (gameObject);

				//Game over. 
				gameController.Gameover ();
			}

		}

	}
}
