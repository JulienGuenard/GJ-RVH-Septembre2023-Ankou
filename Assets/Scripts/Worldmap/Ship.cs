using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ship : MonoBehaviour
{
    [Header("Ship Movement")]
    public float speed;

    [Header("Cargaison")]
    public List<WorkOfArt> workofartList;

    Animator animator;
    NavMeshAgent agent;

    private void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        TravelEnd();
    }

    public void Travel()
    {
        agent.SetDestination(GameManager.instance.portActual.dock.position);
    }

    public void TravelEnd()
    {
        if (ShipManager.instance.shipCanTravel) return;
        if (transform.position != GameManager.instance.portActual.dock.position) return;

        // Check client
        if (GameManager.instance.portActual.workofartGoalList.Count != 0)
        {
            ClientManager.instance.ClientGoalAchieved();
            return;
        }

        // Check art dispo
        if (GameManager.instance.portActual.workofartList.Count == 0)
        {
            ShipManager.instance.shipCanTravel = true;
            return;
        }

        // Check money dispo
        if (ResourceManager.instance.money <= /**/ 300 /* à remplacer */)
        {
            ShipManager.instance.shipCanTravel = true;
            return;
        }

        MinigameManager.instance.MinigameStart(); 
    }
}
