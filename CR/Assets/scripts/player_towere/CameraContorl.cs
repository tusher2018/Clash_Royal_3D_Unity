using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraContorl : MonoBehaviour {

// [SerializeField] float nearPoint;
// [SerializeField] float farPoint;

Vector3 Vec;
[SerializeField] float speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    Vec = transform.localPosition;
       
       Vec.z -= Input.GetAxis("Vertical") * Time.deltaTime * speed;
        transform.localPosition = Vec;
	}
}
