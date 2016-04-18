using UnityEngine;
using System.Collections;

public class FollowObjectInPlane : MonoBehaviour {

	public GameObject followedObject;
	private Vector3 offset;

	void Start () {
		offset = transform.position - followedObject.transform.position;
	}

	void Update () {

		if (followedObject != null) {
			transform.position = new Vector3 (
				followedObject.transform.position.x + offset.x, 
				offset.y, 
				followedObject.transform.position.z + offset.z);

			transform.rotation = followedObject.transform.rotation;
		}
	}
}
