using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("References")]
                        public Port portActual;
    [HideInInspector]   public City cityActual;
     

    public static GameManager instance;

    void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        cityActual = City.Rome;
    }

    public void Victory()
    {

    }
}
