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

    public void Travel()
    {
        agent.SetDestination(GameManager.instance.portActual.dock.position);
    }

    public void TravelEnd()
    {
        MinigameManager.instance.MinigameStart();
        ShipManager.instance.shipCanTravel = false;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Dock")
        {
            if (!collision.gameObject == GameManager.instance.portActual.dock) return;

            TravelEnd();
        }
    }
}
