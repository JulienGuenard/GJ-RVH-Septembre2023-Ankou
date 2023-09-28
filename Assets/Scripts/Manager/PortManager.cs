using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortManager : MonoBehaviour
{
    public Port lastPort;
    public List<Port> gamePorts;
    public List<Port> GamePorts => gamePorts;
    public Port portActual;

    public static PortManager Instance;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        portActual = lastPort;
    }

    public void UpdateArrow(Transform transform, bool isLastPort, bool alreadyVisited)
    {
        if (isLastPort)
        {
            if (!GameManager.Instance.ReadyToEndGame)
                transform.gameObject.SetActive(false);
            else
                transform.gameObject.SetActive(true);

            return;
        }

        if (!alreadyVisited)
            transform.gameObject.SetActive(true);
        else
            transform.gameObject.SetActive(false);

        foreach (Port p in gamePorts)
            if (!p.AlreadyVisited)
                return;

        lastPort.AlreadyVisited = false;
    }
}
