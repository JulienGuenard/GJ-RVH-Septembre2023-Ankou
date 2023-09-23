using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    public static MinigameManager instance;

    void Awake()
    {
        if (instance == null) instance = this;
    }

    public void MinigameStart()
    {
        /**/
        MinigameEnd(); /* à remplacer */
    }

    public void MinigameEnd()
    {
        ShipManager.instance.shipCanTravel = true;
    }
}
