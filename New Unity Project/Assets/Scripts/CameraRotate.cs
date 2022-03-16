using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour {
    public float rotSpeed = 1.0f;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float MouseY = Input.GetAxis("Mouse Y");
        transform.Rotate(Vector3.left * rotSpeed * MouseY);
    }
}
