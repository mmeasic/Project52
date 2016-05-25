using UnityEngine;
using System.Collections;

public class RotateAroundTarget : MonoBehaviour {

    public GameObject targetPlanet;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.RotateAround(targetPlanet.transform.position, Vector3.up, 20 * Time.deltaTime);
    }
}
