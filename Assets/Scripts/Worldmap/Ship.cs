using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.UI;

public class Ship : MonoBehaviour
{
    public Camera camera2;

    [Header("FX")]
    public GameObject trailParticle;

    [Header("Events")]
    public UnityEvent OnTravelStart;
    public UnityEvent OnTravelEnd;
    public UnityEvent OnDock;
    public Action OnDockOut;

    NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //OnDock.AddListener(CameraManager.Instance.ZoomIn);
        OnTravelStart.AddListener(CameraManager.Instance.ZoomOut);
    }

    public void Travel()
    {
        PortManager pm = PortManager.Instance;

        OnTravelStart.Invoke();
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
        Camera.SetupCurrent(camera2);
        PortManager pm = PortManager.Instance;
        MoneyManager mm = MoneyManager.Instance;

        if (!gm)
            gm = GameManager.Instance;
        if (!sm)
            sm = ShipManager.Instance;

        Debug.Log("Travel End");
        OnTravelEnd.Invoke();
        Debug.Log("OnDock");
        OnDock.Invoke();
        StopAllCoroutines();

        // Check money dispo
        if (pm.portActual.AlreadyVisited && !mm.CanStartNegociation)
        {
            sm.shipCanTravel = true;
            return;
        }

        if(pm.portActual.isLastPort)
        {
            if (gm.ReadyToEndGame)
                GameManager.Instance.LaunchScore();
            else
                sm.shipCanTravel = true;

            return;
        }

        StopCoroutine(TravelParticleUpdate());
        ShipManager.Instance.shipCanTravel = false;
    }
}
