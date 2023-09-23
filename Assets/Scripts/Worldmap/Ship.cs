using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [Header("Ship Movement")]
    public float speed;

    [Header("Cargaison")]
    public List<WorkOfArt> workofartList;

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.speed = 1000f;
        StartCoroutine(StartDelayed());
    }

    IEnumerator StartDelayed()
    {
        yield return new WaitForSeconds(0.1f);
        animator.speed = speed;
    }

    public void TravelTo(City destination)
    {
        switch (destination)
        {
            case City.Rome:     animator.SetTrigger("Rome");    break;
            case City.Athenes:  animator.SetTrigger("Athenes"); break;
            case City.Lucet:    animator.SetTrigger("Lucet");   break;
        }
    }

    public void TravelEnd()
    {
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
