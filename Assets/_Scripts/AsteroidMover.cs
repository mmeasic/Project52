using UnityEngine;
using System.Collections;

public class AsteroidMover : MonoBehaviour
{
    public float speed;
	private GameController gameController;

    void Start()
    {
		GetComponent<Rigidbody> ().velocity = new Vector3(Random.value, 0.0f, Random.value) * speed;
    }
}