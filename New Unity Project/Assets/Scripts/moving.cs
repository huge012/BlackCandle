using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moving : MonoBehaviour {

    public static float moveSpeed = 20.0f;
    public float rotSpeed = 0.5f;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        h = h * rotSpeed;
        v = v * moveSpeed * Time.deltaTime;

        transform.Rotate(Vector3.up * h);
        transform.Translate(Vector3.forward * v);

        //float MouseX = Input.GetAxis("Mouse X");
        //transform.Rotate(Vector3.up * rotSpeed * MouseX);
    }
}
