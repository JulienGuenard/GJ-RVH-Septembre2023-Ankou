using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Port : MonoBehaviour
{
    [Header("Data")]
    public City city;

    [Header("Oeuvre d'art disponibles")]
    public List<WorkOfArt> workofartList; // oeuvre d'art disponible � acheter

    [Header("Port du client (objectif)")]
    public List<WorkOfArt> workofartGoalList; // oeuvre d'art � ramener � ce port
}
