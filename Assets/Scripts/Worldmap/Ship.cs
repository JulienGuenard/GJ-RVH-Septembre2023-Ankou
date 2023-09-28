using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.UI;

public class Ship : MonoBehaviour
{
    [Header("FX")]
    public GameObject trailParticle;

    [Header("Events")]
    public UnityEvent OnStartTravel;
    public UnityEvent OnEndTravel;

    NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void Travel()
    {
        PortManager pm = PortManager.Instance;

        OnStartTravel.Invoke();
        agent.SetDestination(pm.portActual.dock.position);
        StopCoroutine(TravelParticleUpdate());
        StartCoroutine(TravelParticleUpdate());

        agent.SetDestination(pm.portActual.dock.position);
        StartCoroutine(WaitForTravelEnd());
    }

    IEnumerator TravelParticleUpdate()
    {
        if(trailParticle)
            Instantiate(trailParticle, transform.position, transform.rotation);
        
        yield return new WaitForSeconds(0.07f);
        StartCoroutine(TravelParticleUpdate());
    }

    private IEnumerator WaitForTravelEnd()
    {
        yield return new WaitWhile(() => agent.pathPending || agent.hasPath || agent.velocity.sqrMagnitude <= 0.1f);

        TravelEnd();
    }

    private GameManager gm;
    private ShipManager sm;

    public void TravelEnd()
    {
        PortManager pm = PortManager.Instance;
        MoneyManager mm = MoneyManager.Instance;

        if (!gm)
            gm = GameManager.Instance;
        if (!sm)
            sm = ShipManager.Instance;

        Debug.Log("Travel End");

        OnStartTravel.Invoke();
        StopAllCoroutines();

        // Check money dispo
        if (pm.portActual.AlreadyVisited && mm.CanStartNegociation)
        {
            sm.shipCanTravel = true;
            return;
        }

        CameraManager.Instance.Zoom();
    }

    public void TravelEndEvent()
    {
        PortManager pm = PortManager.Instance;

        if (pm.portActual.isLastPort)
        {
            if (gm.ReadyToEndGame)
                GameManager.Instance.LaunchScore();
            else
                UIManager.Instance.PNJPanelShow();

            return;
        }

        Debug.Log("Starting a mini-game");
        MinigameManager.Instance.MinigameStart();

        OnEndTravel.Invoke();
        StopCoroutine(TravelParticleUpdate());
        ShipManager.Instance.shipCanTravel = false;
    }
}
