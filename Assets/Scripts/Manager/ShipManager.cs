using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipManager : MonoBehaviour
{
    [Header("References")]
    public Ship playerShip;
    [HideInInspector] public bool shipCanTravel = true;

    public static ShipManager instance;

    void Awake()
    {
        if (instance == null) instance = this;
    }
}
