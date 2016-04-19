using UnityEngine;
using System.Collections;

public class AsteroidRandomRotator : MonoBehaviour {

	public float tumble;

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody> ().angularVelocity = Random.insideUnitSphere * tumble;
	}
	

}
