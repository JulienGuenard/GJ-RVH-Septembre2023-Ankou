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
        if (GameManager.Instance.portActual == port) return;
        if (!ShipManager.Instance.shipCanTravel) return;

        GameManager.Instance.portActual = port;
        ShipManager.Instance.playerShip.Travel();

        FMODUnity.RuntimeManager.PlayOneShot("event:/UI_Clic");
    }

    private void OnMouseEnter()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI_Hovering");
    }
}
