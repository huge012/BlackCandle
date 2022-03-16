using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseStatu : MonoBehaviour {

    public bool isCandle;
    public bool isLivingHouse;
    public bool isMonster;
    public bool isSign;

    // Use this for initialization
    void Start()
    {
        isCandle = false;
        isLivingHouse = false;
        isMonster = false;
        isSign = false;
    }
}
