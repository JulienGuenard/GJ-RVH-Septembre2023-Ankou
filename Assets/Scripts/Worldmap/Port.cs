using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Port : MonoBehaviour
{
    [Header("Dock")]
    public Transform dock;

    public bool isLastPort;
    
    private bool alreadyVisited;

    public bool AlreadyVisited
    {
        get
        {
            return alreadyVisited;
        }
        set
        {
            alreadyVisited = value;
            PortManager.Instance.UpdateArrow(transform.Find("arrow"), isLastPort, alreadyVisited);
        }
    }
}
