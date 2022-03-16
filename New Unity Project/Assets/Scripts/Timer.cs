using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Timer : MonoBehaviour {

    public Text guiTime;
    public Light light1, light2;
    public AudioClip nightSound, morningSound;
    public GameObject playerPos;
    public static float playTime;
    public static bool isDay;
    public static int Days;
    public static float lightStep;

    void Start()
    {
        playTime = 0.0f;
        isDay = true;
        // Days = 1; // full Ver.
        Days = 6; // test Ver.
        lightStep = -0.1f;
    }

    // Update is called once per frame
    void Update() {
        playTime += Time.deltaTime;
        if (isDay == true && playTime <= 0.5f) { light1.intensity = 1; light2.intensity = 1; }
        if (playTime >= 80.0f) // 어두워짐
        {

                light1.intensity += lightStep * Time.deltaTime;
                light2.intensity += lightStep * Time.deltaTime;
        }
        if (playTime >= 90.0f) // 밤or낮 바뀜
        {
            if (isDay == false) // 밤이었을 경우
            {
                AudioSource.PlayClipAtPoint(morningSound, playerPos.transform.position);
                Days++;
            }
            else AudioSource.PlayClipAtPoint(nightSound, playerPos.transform.position); // 낮이었을 경우
            isDay = !isDay; // 낮밤 바꿔줌
            playTime = 0.0f; // 낮밤 시간 초기화
            lightStep *= -1.0f;
        }

        if (Days == 8)
        {
            checkEnding();
            SceneChange();
        }

    }

    void OnGUI()
    {
        string timeStr;
        int sec = (90 - (int)playTime) % 60, min = (90 - (int)playTime) / 60;
        if (sec > 59)
        {
            min++;
            sec = 0;
        }
        timeStr = min + " : " + sec.ToString("00");
        if (isDay == true)
            guiTime.text = Days + " D " + timeStr;
        else
            guiTime.text = Days + " N " + timeStr;
    }

    void checkEnding()
    {
        if (DayNightAlgo.successHouse <= 3) // Bad Ending
            PlayerPrefs.SetInt("endingNum", 3);
        else if (DayNightAlgo.successHouse <= 7) // Normal Ending
            PlayerPrefs.SetInt("endingNum", 2);
        else
            PlayerPrefs.SetInt("endingNum", 1); // True Ending
    }

    void SceneChange()
    {
        SceneManager.LoadScene("EndingScene");
    }
}
