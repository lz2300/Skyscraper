using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//aaa123.     123456 keystroy

public class GameCtroller : MonoBehaviour
{
    public GameManager gameManager;
    public AudioManager audioManager;
    public TimeManager timeManager;
    public static GameCtroller Ins;
    // Start is called before the first frame update
    void Start()
    {
        Ins = this;
    }
}
