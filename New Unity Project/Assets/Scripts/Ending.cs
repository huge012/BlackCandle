using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Ending : MonoBehaviour {
    private int endingN;
    public GameObject Monsters, Player, pos, TruePeople, normal;
    public AudioClip badending, click;
    private float lightStep = 0.5f, timer = 0.0f;
    private bool on = true, routineOn = false;

    // Use this for initialization
    void Start () {
        endingN = PlayerPrefs.GetInt("endingNum", 0);
        PlayerPrefs.Save();
        normal.SetActive(false);
        TruePeople.SetActive(false);
        Monsters.SetActive(false);

        if (endingN == 1) // 트루엔딩
        {
            TrueEnding();
        }
        
        else if (endingN == 3) // 배드엔딩
        {
            BadEnding();
            AudioSource.PlayClipAtPoint(badending, Player.transform.position);
        }
        
        else // 노말엔딩
        {
            NormalEnding();
        }
    }
	
	// Update is called once per frame
	void Update () {

        // 다시 시작 버튼 보여주기
        if (timer < 7.0f)
        {
            timer += Time.deltaTime;
        }
        else
        {
            if (routineOn == false)
            {
                StartCoroutine(this.pressStart());
                routineOn = true;
            }
            if (Input.GetButtonDown("Fire2")) AudioSource.PlayClipAtPoint(click, transform.position);
            if (Input.GetButtonUp("Fire2")) SceneChange();
        }

    }

    void TrueEnding()
    {
        TruePeople.SetActive(true);
    }

    void NormalEnding()
    {
        normal.SetActive(true);
    }

    void BadEnding()
    {
        Monsters.SetActive(true);
    }

    void SceneChange()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("StartScene");
    }

    // 다시 시작 버튼 깜빡이기
    IEnumerator pressStart()
    {
        while (true)
        {
            if (on == true)
            {
                Player.transform.Find("Prs").gameObject.SetActive(false);
                on = false;
            }
            else
            {
                Player.transform.Find("Prs").gameObject.SetActive(true);
                on = true;
            }
            yield return new WaitForSeconds(0.6f);
        }
    }

}
