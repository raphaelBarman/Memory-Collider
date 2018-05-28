using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetWithCamera : MonoBehaviour {
    public Camera cam;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(transform.position.x, cam.transform.position.y, transform.position.z);
	}
}
