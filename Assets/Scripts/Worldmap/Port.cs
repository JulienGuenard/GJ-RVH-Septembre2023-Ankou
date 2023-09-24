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
            GameManager.Instance.UpdateArrow(transform.Find("arrow"), isLastPort, alreadyVisited);
        }
    }

    //[Header("Oeuvre d'art disponibles")]
    //public List<WorkOfArt> workofartList; // oeuvre d'art disponible à acheter

    //[Header("Port du client (objectif)")]
    //public List<WorkOfArt> workofartGoalList; // oeuvre d'art à ramener à ce port
}
