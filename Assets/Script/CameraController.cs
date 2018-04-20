using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}


	// Update is called once per frame
	void Update () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		float wheel = Input.GetAxis("Mouse ScrollWheel");

		transform.position += new Vector3 (moveHorizontal*50, moveVertical*50, wheel*1000);
	}
}
