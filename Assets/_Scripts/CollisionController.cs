using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//Script to control the effect of the collisions. 
public class CollisionController : MonoBehaviour {

	//Explosion of an asteroid. 
	public GameObject explosion;
	//Explosion of an asteroid with sound. 
	public GameObject explosion_sound;

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
		}
		GameObject playerControllerObject = GameObject.FindGameObjectWithTag ("Player");
		if (playerControllerObject != null) {
			playerController = playerControllerObject.GetComponent<PlayerController> ();
		}
	}

	//On Collision... 
	void OnTriggerEnter(Collider other) {

		//PLAYER
		if (gameObject.tag == "Player") {

			//Case Pickup
			if (other.tag == "Pickup") {
				gameController.getPickup ();
			}
				
			//Case Asteroid
			if (other.tag == "Asteroid") {
				destroyAsteroid(other, true);
				if (playerController.checkLive (true)) {
					destroyPlayer ();
				}
			}
				
			//Case Boundary and Planet
			if (other.tag == "Boundary" || other.tag == "Planet") {
				destroyPlayer ();
			}
		}

		//ASTEROID
		if (gameObject.tag == "Asteroid") {

			//Case Asteroid
			if (other.tag == "Asteroid") {
				destroyAsteroid(other, false);
				destroyAsteroid(GetComponent<Collider>(), false);
			}

			//Case Boundary and Planet
			if (other.tag == "Boundary" || other.tag == "Planet") {
				destroyAsteroid(GetComponent<Collider>(), false);
			}
		}
	}

	private void destroyPlayer() {
		GameObject ex = (GameObject) Instantiate (playerExplosion, transform.position, transform.rotation);
		ex.transform.parent = GameObject.FindGameObjectWithTag ("Explosions").transform;
		Destroy (gameObject);
		gameController.Gameover ();
	}

	private void destroyAsteroid(Collider ob, bool sound) {
		GameObject ex = null;

		if (gameController != null) gameController.createAsteroid ();

		if (sound) ex = (GameObject) Instantiate (explosion_sound, ob.transform.position, ob.transform.rotation);
		if (!sound) ex = (GameObject) Instantiate (explosion, ob.transform.position, ob.transform.rotation);

		ex.transform.parent = GameObject.FindGameObjectWithTag ("Explosions").transform;
		Destroy(ob.gameObject);
	}
}
