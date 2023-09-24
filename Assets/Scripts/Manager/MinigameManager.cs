using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    private List<(WorkOfArt, CoursesItemState)> selectedWorkOfArts = new List<(WorkOfArt, CoursesItemState)>();
    public List<(WorkOfArt, CoursesItemState)> Selection => selectedWorkOfArts;

    [Header("Data")]
    [SerializeField] [Range(0.1f, 0.5f)]
    private float whatIsAGoodPrice;

    [Header("Events")]
    public UnityEvent OnMiniGameStart;
    public UnityEvent<CoursesItemState> OnMiniGameEnd;

    private void Start()
    {
        

        miniGame.OnNegociationEnd.AddListener(MinigameEnd);
    }

    public void ResetGame()
    {
        selectedWorkOfArts.Clear();

        for (int i = 0; i < 4; i++)
        {
            int j = Random.Range(0, availableWorkOfArts.Count);
            selectedWorkOfArts.Add((availableWorkOfArts[j], CoursesItemState.NotYetProcessed));
            availableWorkOfArts.RemoveAt(j);
        }
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
        OnMiniGameStart.Invoke();
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
        OnMiniGameEnd.Invoke(state);
        GameManager gm = GameManager.Instance;
        
        gm.portActual.AlreadyVisited = true;
    }

    public int ComputeScore()
    {
        int score = 0;

        foreach((WorkOfArt woa , CoursesItemState state) in selectedWorkOfArts)
        {
            switch(state)
            {
                case CoursesItemState.NotYetProcessed:
                case CoursesItemState.Processing:
                    return -1;
                case CoursesItemState.BoughtAtGoodPrice:
                    score += 25;
                    break;
                case CoursesItemState.BoughtAtHighPrice:
                    score += 15;
                    break;
                default:
                    score += 5;
                    break;
            }

            return score;
        }

        return -1;
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