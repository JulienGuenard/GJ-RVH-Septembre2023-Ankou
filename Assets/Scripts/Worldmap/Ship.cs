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

    [Header("Cargaison")]
    public List<WorkOfArt> workofartList;
    public List<GameObject> itemGMBList;

    [Header("Events")]
    public UnityEvent OnStartTravel;
    public UnityEvent OnEndTravel;

    Animator animator;
    NavMeshAgent agent;

    private void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        foreach(GameObject obj in itemGMBList)
        {
            obj.SetActive(false);
        }
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
        if(trailParticle)
            Instantiate(trailParticle, transform.position, transform.rotation);
        
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

        OnStartTravel.Invoke();
        StopAllCoroutines();

        // Check money dispo
        if (gm.portActual.AlreadyVisited && gm.CanStartNegociation)
        {
            sm.shipCanTravel = true;
            return;
        }

        if(gm.portActual.isLastPort)
        {
            Debug.Log("bonjour");
            if (gm.ReadyToEndGame)
            {
                GameManager.Instance.LaunchScore();
                Debug.Log("au revoir");
            }
            else
                sm.shipCanTravel = true;

            return;
        }

        Debug.Log("Starting a mini-game");
        MinigameManager.Instance.MinigameStart();

        OnEndTravel.Invoke();
        StopCoroutine(TravelParticleUpdate());
        ShipManager.Instance.shipCanTravel = false;
    }

    public void AddToCargaison(WorkOfArt art)
    {
        /*if (workofartList.Count == 3) return;

        workofartList.Add(art);
        itemGMBList[workofartList.Count - 1].SetActive(true);
        itemGMBList[workofartList.Count - 1].GetComponentInChildren<TextMeshProUGUI>().text = art.name;
        itemGMBList[workofartList.Count - 1].GetComponentInChildren<Image>().sprite = art.Illustration;*/
    }
}
