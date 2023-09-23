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
        if (GameManager.instance.cityActual == port.city)   return;
        if (!ShipManager.instance.shipCanTravel)            return;

        GameManager.instance.cityActual = port.city;
        ShipManager.instance.playerShip.TravelTo(port.city);
        ShipManager.instance.shipCanTravel = false;
    }
}
