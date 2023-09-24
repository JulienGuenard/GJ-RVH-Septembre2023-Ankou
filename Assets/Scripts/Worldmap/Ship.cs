using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Ship : MonoBehaviour
{
    [Header("FX")]
    public GameObject trailParticle;

    [Header("Cargaison")]
    public List<WorkOfArt> workofartList;

    [Header("Events")]
    public UnityEvent OnStartTravel;
    public UnityEvent OnEndTravel;

    Animator animator;
    NavMeshAgent agent;

    private void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    public void Travel()
    {
        OnStartTravel.Invoke();
        agent.SetDestination(GameManager.Instance.portActual.dock.position);
        StopCoroutine(TravelParticleUpdate());
        StartCoroutine(TravelParticleUpdate());

        agent.SetDestination(GameManager.Instance.portActual.dock.position);
        StartCoroutine(WaitForTravelEnd());
    }

    IEnumerator TravelParticleUpdate()
    {
        GameObject obj = Instantiate(trailParticle, transform.position, transform.rotation);
        yield return new WaitForSeconds(0.07f);
        StartCoroutine(TravelParticleUpdate());
    }

    private IEnumerator WaitForTravelEnd()
    {
        yield return new WaitWhile(() => agent.pathPending || agent.hasPath || agent.velocity.sqrMagnitude == 0f);

        TravelEnd();
    }

    private GameManager gm;
    private ShipManager sm;

    public void TravelEnd()
    {
        if (!gm)
            gm = GameManager.Instance;
        if (!sm)
            sm = ShipManager.Instance;

        Debug.Log("Travel End");

        // Check money dispo
        if (gm.portActual.AlreadyVisited && gm.CanStartNegociation)
        {
            sm.shipCanTravel = true;
            return;
        }

        if(gm.portActual.isLastPort)
        {
            if(gm.ReadyToEndGame)
                GameManager.Instance.LaunchScore();
            else
                sm.shipCanTravel = true;

            return;
        }

        Debug.Log("Starting a mini-game");
        MinigameManager.Instance.MinigameStart();

        OnEndTravel.Invoke();
        StopCoroutine(TravelParticleUpdate());
    }
}
