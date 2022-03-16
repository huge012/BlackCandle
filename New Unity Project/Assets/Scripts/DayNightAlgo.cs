using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DayNightAlgo : MonoBehaviour {

    public Text resHouses;
    public Text resCandles;
    public GameObject[] LivingHouse = new GameObject[10];
    public GameObject[] MonsterHouse = new GameObject[10];
    public GameObject player, startingPoint;
    public AudioClip fire, gettingCandle, drawingSign, cleanSign, errorSound, monsterSound, morningSound;
    public int candles;
    public static int successHouse; // 불빛 나눠준 집

    public GameObject[] givingCandle = new GameObject[10];
    private int nextIndex = successHouse, eraseIndex = 0;

    // Use this for initialization
    void Start () {
        // candles = 3; // full Ver.
        candles = 5; // test Ver.
        successHouse = 3;
        showLiving(); // 사람 사는 흔적 보여줌
        for(int i = 0; i < successHouse; i++)
        {
            givingCandle[i] = LivingHouse[i];
            givingCandle[i].GetComponent<HouseStatu>().isCandle = true;
        }
        for(int i = successHouse; i < 10; i++)
        {
            givingCandle[i] = null;
        }
        StartCoroutine(Sounds());
    }
	
	// Update is called once per frame
	void Update () {

        if (Timer.isDay == true) // 낮일 경우 괴물의 흔적 나타남
        {
            if (Timer.playTime <= 1.0f) showDarkMatter();
        }
        else // 밤일 경우 괴물의 흔적 사라짐
        {
            if (Timer.playTime <= 1.0f) hideDarkMatter();
        }
        if (Input.GetButtonDown("Fire2")) // D버튼 누를 때
        {
            GameObject t;
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit; // 레이캐스팅
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                t = hit.collider.gameObject; // 레이캐스팅에 맞은 오브젝트 저장
                Debug.Log(t.transform.name);

                if (Timer.isDay == true) // 낮일 때
                {
                    if (t.transform.name == "DarkMatter") // 괴물의 흔적을 찾았다
                    {
                        AudioSource.PlayClipAtPoint(gettingCandle, player.transform.position);
                        candles++; // 보유한 양초 추가
                        Debug.Log("Candles : " + candles);
                        t.SetActive(false); // 괴물 흔적 사라짐. 나중에 다시 나타날 수 있음.
                    }
                    else if (t.CompareTag("House")) // 사인 on/off
                    {
                        if (t.GetComponent<HouseStatu>().isSign == false)
                        {
                            AudioSource.PlayClipAtPoint(drawingSign, player.transform.position);
                            t.transform.Find("sign").gameObject.SetActive(true);
                            t.GetComponent<HouseStatu>().isSign = true;
                        }
                        else
                        {
                            AudioSource.PlayClipAtPoint(cleanSign, player.transform.position);
                            t.transform.Find("sign").gameObject.SetActive(false);
                            t.GetComponent<HouseStatu>().isSign = false;
                        }
                    }
                }
                else // 밤일 때
                {
                    if (t.transform.name == "Stick")
                    {
                        if (candles > 0) // 나눠줄 초가 남아있을 때
                        {
                            if (t.transform.parent.gameObject.GetComponent<HouseStatu>().isCandle == false) // 불이 안 밝혀진 집
                            {
                                t.transform.parent.gameObject.GetComponent<HouseStatu>().isCandle = true;
                                candles--;
                                t.transform.Find("CandleStick").gameObject.SetActive(true);
                                AudioSource.PlayClipAtPoint(fire, player.transform.position);
                                string houseName;
                                houseName = t.transform.parent.name;
                                checkLivingHouse(houseName);
                                checkMonsterHouse(houseName);
                            }
                            else AudioSource.PlayClipAtPoint(errorSound, player.transform.position);
                        }
                        else // 초 없을 때
                            AudioSource.PlayClipAtPoint(errorSound, player.transform.position);
                    }
                }
            }
        }

        if (successHouse == 0)
        {
            PlayerPrefs.SetInt("endingNum", 4);
            SceneChange();
        }
        else if (successHouse == 10)
        {
            PlayerPrefs.SetInt("endingNum", 1);
            SceneChange();
        }
    }

    void showLiving() // 사람 사는 흔적 보여주기
    {
        foreach (GameObject i in LivingHouse)
        {
            GameObject trace;
            i.GetComponent<HouseStatu>().isLivingHouse = true;
            i.GetComponent<HouseStatu>().isCandle = false;
            if (i.transform.Find("LivingTrace") == true)
            {
                trace = i.transform.Find("LivingTrace").gameObject;
                trace.SetActive(true);
            }
        }
    }

    void showDarkMatter() // 괴물의 흔적 보이기(낮)
    {
        foreach (GameObject i in MonsterHouse)
        {
            GameObject trace;
            i.GetComponent<HouseStatu>().isMonster = true;
            if (i.transform.Find("DarkMatter") == true)
            {
                trace = i.transform.Find("DarkMatter").gameObject;
                trace.SetActive(true);
            }
        }
    }

    void hideDarkMatter() // 괴물의 흔적 숨기기
    {
        foreach (GameObject i in MonsterHouse)
        {
            GameObject trace;
            if (i.transform.Find("DarkMatter") == true)
            {
                trace = i.transform.Find("DarkMatter").gameObject;
                trace.SetActive(false);
            }
        }
    }

    void checkLivingHouse(string name) // 사람 사는 흔적이 있는 집인지 검사
    {
        foreach (GameObject i in LivingHouse)
        {
            if (i.name == name) // 사람 사는 흔적이 있는 집이면
            {
                Debug.Log("Lighting!!");
                givingCandle[nextIndex] = i;
                nextIndex = (nextIndex + 1) % 10;
                successHouse++;
                break;
            }
        }
    }

    void checkMonsterHouse(string name) // 괴물 사는 흔적이 있는 집인지 검사
    {
        foreach (GameObject i in MonsterHouse)
        {
            if (i.name == name) // 괴물이 있는 집이면
            {
                GameObject findCandle;
                Debug.Log("Wrong!!");
                successHouse--;
                findCandle = givingCandle[eraseIndex].transform.Find("Stick").gameObject;
                findCandle.transform.Find("CandleStick").gameObject.SetActive(false);
                givingCandle[eraseIndex].GetComponent<HouseStatu>().isCandle = false;
                givingCandle[eraseIndex] = null;
                eraseIndex = (eraseIndex + 1) % 10;
                i.transform.Find("Monster_bit").gameObject.SetActive(true);

                break;
            }
        }
    }

    void OnGUI()
    {
        resHouses.text = successHouse + "/10" ;
        resCandles.text = " " + candles;
    }

    void SceneChange()
    {
        SceneManager.LoadScene("EndingScene");
    }

    void repointing()
    {
        player.transform.position = startingPoint.transform.position;
    }

    IEnumerator Sounds()
    {
        while (true)
        {
            if (Timer.isDay == true)
            {
                AudioSource.PlayClipAtPoint(morningSound, player.transform.position);
            }
            else
            {
                AudioSource.PlayClipAtPoint(monsterSound, player.transform.position);
            }
            yield return new WaitForSeconds(32.0f);
        }
    }
}
