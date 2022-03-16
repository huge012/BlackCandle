using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class light : MonoBehaviour {

    public Light light1, light2;
    public float lightStep = -0.3f;
    private bool isDay = true;
    public float playTime = 0.0f;
	
	// Update is called once per frame
	void Update () {
        playTime += Time.deltaTime;
        light1.intensity += lightStep * Time.deltaTime;
        light2.intensity += lightStep * Time.deltaTime;
        if (playTime >= 5.0f) // 어두워짐 or 밝아짐
        {
            isDay = !isDay; // 낮밤 바꿔줌
            playTime = 0.0f; // 낮밤 시간 초기화
            lightStep *= -1.0f;
        }
    }
}
