using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMeet : MonoBehaviour {

    public GameObject player, startingPoint;

    public void OnTriggerEnter(Collider ob)
    {
        if (ob.transform.tag == "player")
        {
            Timer.playTime = 0.0f;
            Timer.Days++;
            Timer.isDay = true;
            Timer.lightStep = -0.1f;
            repointing();
            Destroy(this.gameObject);
        }
    }

    void repointing()
    {
        player.transform.position = startingPoint.transform.position;
    }
}
