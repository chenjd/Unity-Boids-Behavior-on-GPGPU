using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateForDemo : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.localRotation = Quaternion.AngleAxis(10 * Time.deltaTime, Vector3.up) * transform.localRotation;
	}
}
