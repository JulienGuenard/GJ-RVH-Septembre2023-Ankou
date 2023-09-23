using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    #region Singleton
    private static MinigameManager instance;
    public static MinigameManager Instance => instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;

        }
        else
            Destroy(this);
    }
    #endregion

    [Header("Refs")]
    [SerializeField]
    private MiniGame miniGame;
    [SerializeField]
    private List<WorkOfArt> availableWorkOfArts;
    private List<(WorkOfArt, CoursesItemState)> selectedWorkOfArts;

    [Header("Data")]
    [SerializeField] [Range(0.1f, 0.5f)]
    private float whatIsAGoodPrice;

    private void Start()
    {
        selectedWorkOfArts = new List<(WorkOfArt, CoursesItemState)>();

        for (int i=0; i < 4; i++)
            selectedWorkOfArts.Add((availableWorkOfArts[Random.Range(0,availableWorkOfArts.Count)], CoursesItemState.NotYetProcessed));

        miniGame.OnNegociationEnd.AddListener(MinigameEnd);
    }

    public void MinigameStart()
    {
        int i;
        do
        {
            i = Random.Range(0, selectedWorkOfArts.Count);
        }
        while (selectedWorkOfArts[i].Item2 != CoursesItemState.NotYetProcessed);

        selectedWorkOfArts[i] = (selectedWorkOfArts[i].Item1, CoursesItemState.Processing);

        miniGame.gameObject.SetActive(true);
        miniGame.StartNegociations(selectedWorkOfArts[i].Item1);
        MusicManager.instance.MusicNegociation();
    }

    public void MinigameEnd(bool success, float cost)
    {
        int i;
        do
        {
            i = Random.Range(0, selectedWorkOfArts.Count);
        }
        while (selectedWorkOfArts[i].Item2 != CoursesItemState.Processing);

        WorkOfArt woa = selectedWorkOfArts[i].Item1;
        CoursesItemState state;

        if (success)
        {
            GameManager.Instance.BuyFor(cost);

            float diff = woa.MaxPrize - woa.MinPrize;

            if (cost - woa.MinPrize <= diff * whatIsAGoodPrice)
                state = CoursesItemState.BoughtAtGoodPrice;
            else
                state = CoursesItemState.BoughtAtHighPrice;
        }
        else
            state = CoursesItemState.NegociationFailed;

        selectedWorkOfArts[i] = (woa, state);

        if (state == CoursesItemState.NegociationFailed)
            miniGame.ShowShame();

        ShipManager.Instance.shipCanTravel = true;
        MusicManager.instance.MusicWorldmap();
    }
}

public enum CoursesItemState
{
    NotYetProcessed,
    Processing,
    BoughtAtGoodPrice,
    BoughtAtHighPrice,
    NegociationFailed
}