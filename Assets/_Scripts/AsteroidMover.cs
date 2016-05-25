using UnityEngine;
using System.Collections;

public class AsteroidMover : MonoBehaviour
{
    public float speed;

	private float maxSpeed;
	private GameController gameController;


	private Rigidbody rb;

    void Start()
    {
		GameObject gC = GameObject.FindGameObjectWithTag ("GameController");
		if (gC != null) gameController = gC.GetComponent<GameController> ();

		maxSpeed = gameController.difficultyFactor * 10.0f;

		int dir = gameController.asteroid_direction;
		rb = GetComponent<Rigidbody> ();

		if (dir <= 1) rb.velocity = new Vector3 (Random.Range (-2, 3), 0.0f, Random.Range (-2, 0)) * speed;
		if (dir == 2) rb.velocity = new Vector3 (Random.Range (-2, 3), 0.0f, Random.Range (1, 3)) * speed;
		if (dir == 3) rb.velocity = new Vector3 (Random.Range (-2, 0), 0.0f, Random.Range (-2, 3)) * speed;
		if (dir >= 4) rb.velocity = new Vector3 (Random.Range (1, 3), 0.0f, Random.Range (-2, 3)) * speed;
    }

	void FixedUpdate(){
		if ( rb.velocity.magnitude >= maxSpeed) {
			rb.velocity = maxSpeed / rb.velocity.magnitude * rb.velocity;
		}
	}
}