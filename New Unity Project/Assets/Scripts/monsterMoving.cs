using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterMoving : MonoBehaviour
{

    public GameObject pos;
    private float speed = 10.0f;

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, pos.transform.position, speed * Time.deltaTime);
    }

    public void OnTriggerEnter(Collider ob)
    {
        if(ob.transform.tag == "pos")
            Destroy(this);
    }

}