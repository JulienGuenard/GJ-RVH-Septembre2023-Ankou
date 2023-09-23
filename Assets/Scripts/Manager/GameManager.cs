using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Start")]
    public Port portActual;
     
    public static GameManager instance;

    void Awake()
    {
        if (instance == null) instance = this;
    }

    public void Victory()
    {

    }
}
