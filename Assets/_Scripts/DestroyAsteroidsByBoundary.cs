using UnityEngine;
using System.Collections;

public class DestroyAsteroidsByBoundary : MonoBehaviour {

	void OnTriggerExit(Collider other){
		if (other.tag == "Asteroid") {
			Destroy(other.gameObject);		//Destroy (Asteroid);
		}
}
}