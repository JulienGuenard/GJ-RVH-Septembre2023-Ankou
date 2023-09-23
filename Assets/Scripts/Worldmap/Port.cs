using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Port : MonoBehaviour
{
    [Header("Dock")]
    public Transform dock;

    [Header("Oeuvre d'art disponibles")]
    public List<WorkOfArt> workofartList; // oeuvre d'art disponible à acheter

    [Header("Port du client (objectif)")]
    public List<WorkOfArt> workofartGoalList; // oeuvre d'art à ramener à ce port
}
