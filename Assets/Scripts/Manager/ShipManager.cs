using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipManager : MonoBehaviour
{
    [Header("References")]
    public Ship playerShip;
    public bool shipCanTravel = true;

    private static ShipManager instance;
    public static ShipManager Instance => instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }
}
