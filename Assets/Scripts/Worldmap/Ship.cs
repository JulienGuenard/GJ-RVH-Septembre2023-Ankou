using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ship : MonoBehaviour
{
    [Header("FX")]
    public GameObject trailParticle;

    [Header("Cargaison")]
    public List<WorkOfArt> workofartList;

    bool isTraveling;

    Animator animator;
    NavMeshAgent agent;

    private void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    public void Travel()
    {
        MusicManager.instance.SFXTravelStart();
        agent.SetDestination(GameManager.instance.portActual.dock.position);
        StopCoroutine(TravelParticleUpdate());
        StartCoroutine(TravelParticleUpdate());
    }

    IEnumerator TravelParticleUpdate()
    {
        GameObject obj = Instantiate(trailParticle, transform.position, transform.rotation);
        yield return new WaitForSeconds(0.07f);
        StartCoroutine(TravelParticleUpdate());
    }

    public void TravelEnd()
    {
        MusicManager.instance.SFXTravelStop();
        FMODUnity.RuntimeManager.PlayOneShot("event:/Boat_Arrived");
        StopCoroutine(TravelParticleUpdate());
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
