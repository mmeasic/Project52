using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

	public float tumble;

	private Vector3 rotation;

	void Start () {
		rotation = Random.insideUnitSphere;
	}

	// Update is called once per frame
	void Update () {
		GetComponent<Rigidbody>().angularVelocity = rotation * tumble; 
	}
}
