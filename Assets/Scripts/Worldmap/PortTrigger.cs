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
        PortManager pm = PortManager.Instance;

        if (pm.portActual == port) return;
        if (!ShipManager.Instance.shipCanTravel) return;

        pm.portActual = port;
        ShipManager.Instance.playerShip.Travel();

        FMODUnity.RuntimeManager.PlayOneShot("event:/UI_Clic");
    }

    private void OnMouseEnter()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI_Hovering");
    }
}
