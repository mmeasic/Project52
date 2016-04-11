using UnityEngine;
using System.Collections;

public class AsteroidMover : MonoBehaviour
{
    public float speed;
	//public GameController temp;
	//We will use the Game Controller and the Player Controller Scripts. 
	private GameController gameController;



    void Start()
    {
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController> ();
		} else {
			Debug.Log ("Cannot Find 'GameController' script");
		}

		int dir = gameController.direction;
		Rigidbody rb = GetComponent<Rigidbody> ();
		//rb.velocity = transform.forward * speed;
		//here should be switch case
		if (dir == 1) {
			//from top
			rb.velocity = new Vector3 (Random.Range (-2, 3), 0, Random.Range (1, 3)) * speed;
		} else if (dir == 2) {
			//from bottom
			rb.velocity = new Vector3 (Random.Range (-2, 3), 0, Random.Range (-2, 0)) * speed;
		} else if (dir == 3) {
			//from left
			rb.velocity = new Vector3 (Random.Range (-2, 0), 0, Random.Range (-2, 3)) * speed;
		} else if (dir == 4) {
			//from right
			rb.velocity = new Vector3 (Random.Range (1, 3), 0, Random.Range (-2, 3)) * speed;
		}

    }
}