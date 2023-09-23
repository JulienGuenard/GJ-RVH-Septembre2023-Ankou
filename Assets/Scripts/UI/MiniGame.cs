using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MiniGame : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField]
    private GameObject playing;
    [SerializeField]
    private GameObject lost;
    [SerializeField]
    private NegoBar negoBar;
    [SerializeField]
    private Reglette reglette;
    [SerializeField]
    private WorkOfArt workOfArt;

    public UnityEvent<bool, float> OnNegociationEnd;

    [Header("Data")]
    [SerializeField]
    private float money;
    [SerializeField]
    private float cost;
    [SerializeField]
    private float goalPrize;
    [SerializeField]
    private float currentPrize;
    [SerializeField]
    private float maxPrize;

    public void StartNegociations(WorkOfArt _workOfArt)
    {
        workOfArt = _workOfArt;
        cost = workOfArt.MaxPrize;

        reglette.SetUp(workOfArt.MinPrize, workOfArt.MaxPrize);
        negoBar.StartPlaying();
    }

    public void Negociate()
    {
        if (!negoBar.PickUpValue(out float val))
            return;

        cost -= val;
        
        if(!reglette.UpdateTextAndPosition(cost))
        {
            negoBar.Stop();
            OnNegociationEnd.Invoke(false, reglette.CurrentValue);
            playing.SetActive(false);
        }
    }

    public void Validate()
    {
        negoBar.Stop();
        OnNegociationEnd.Invoke(true, reglette.CurrentValue);
        playing.SetActive(false);
    }

    public void Leave()
    {
        lost.SetActive(false);
        gameObject.SetActive(false);
    }

    public void ShowShame()
    {
        lost.SetActive(true);
    }

    /*
    [Header("Test")]
    [SerializeField] [Range(0.1f, 0.5f)]
    private float whatIsAGoodPrice;
    [SerializeField]
    private WorkOfArt testWOA;

    public void Test(bool success, WorkOfArt woa, float _cost)
    {
        Debug.Log(success ? "Yay !" : "Oh :c");
        CoursesItemState state;

        if (success)
        {
            float diff = woa.MaxPrize - woa.MinPrize;

            if (_cost - woa.MinPrize <= diff * whatIsAGoodPrice)
                state = CoursesItemState.BoughtAtGoodPrice;
            else
                state = CoursesItemState.BoughtAtHighPrice;
        }
        else
            state = CoursesItemState.NegociationFailed;

        switch(state)
        {
            case CoursesItemState.BoughtAtGoodPrice:
                Debug.Log("You did well my friend");
                break;
            case CoursesItemState.BoughtAtHighPrice:
                Debug.Log("Have you forgotten how to negociate ? I'M RUINED BECAUSE OF YOU");
                break;
            case CoursesItemState.NegociationFailed:
                Debug.Log("You couldn't even bring it...");
                lost.gameObject.SetActive(true);
                break;
        }
    }

    private void Start()
    {
        playing.SetActive(true);
        lost.SetActive(false);
        Debug.Log("Début Test", this);
        OnNegociationEnd.AddListener(Test);
        StartNegociations(testWOA);
    }*/
}

