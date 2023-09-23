using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortTrigger : MonoBehaviour
{
    Port port;

    private void Start()
    {
        port = GetComponentInParent<Port>();
    }

    private void OnMouseDown()
    {
        if (GameManager.instance.portActual == port)    return;
        if (!ShipManager.instance.shipCanTravel)        return;

        GameManager.instance.portActual = port;
        ShipManager.instance.playerShip.Travel();
    }
}
