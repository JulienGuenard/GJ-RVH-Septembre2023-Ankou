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
        agent.SetDestination(GameManager.Instance.portActual.dock.position);
        StartCoroutine(WaitForTravelEnd());
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

        // (sm.shipCanTravel) return;
        //if (transform.position != gm.portActual.dock.position) return;

        /*// Check client
        if (gm.portActual.workofartGoalList.Count != 0)
        {
            ClientManager.instance.ClientGoalAchieved();
            return;
        }

        // Check art dispo
        if (gm.portActual.workofartList.Count == 0)
        {
            sm.shipCanTravel = true;
            return;
        }*/

        // Check money dispo
        if (GameManager.Instance.Money <= /**/ 300 /* à remplacer */)
        {
            sm.shipCanTravel = true;
            return;
        }

        Debug.Log("Starting a mini-game");
        MinigameManager.Instance.MinigameStart(); 
    }
}
