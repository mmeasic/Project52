using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {
	
	// Update is called once per frame
	public float tumble;

	void Start ()
	{
		GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * tumble; 
	}
}
