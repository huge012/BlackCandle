using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Starting : MonoBehaviour {
    public float rotSpeed = 1.0f;
    public static float playTime = 0.0f;
    public AudioClip click;
    private bool on = true;

    void Start()
    {
        StartCoroutine(this.pressStart());
    }
    // Update is called once per frame
    void Update() {
        //float MouseX = Input.GetAxis("Mouse X");
        //transform.Rotate(Vector3.up * rotSpeed * MouseX);

        if (Input.GetButtonDown("Fire2")) AudioSource.PlayClipAtPoint(click, transform.position);
        if (Input.GetButtonUp("Fire2")) SceneChange();
    }

    void SceneChange()
    {
        SceneManager.LoadScene("proto0.0.1");
    }

    IEnumerator pressStart()
    {
        while (true)
        {
            if (on == true)
            {
                transform.Find("Prs").gameObject.SetActive(false);
                on = false;
            }
            else
            {
                transform.Find("Prs").gameObject.SetActive(true);
                on = true;
            }
            yield return new WaitForSeconds(0.6f);
        }
    }
}
