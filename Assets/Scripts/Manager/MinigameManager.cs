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
        ResetGame();
        miniGame.OnNegociationEnd.AddListener(MinigameEnd);
    }

    public void ResetGame()
    {
        selectedWorkOfArts.Clear();

        List<WorkOfArt> tmp = new(availableWorkOfArts.ToArray());

        for (int i = 0; i < 4; i++)
        {
            int j = Random.Range(0, tmp.Count);
            selectedWorkOfArts.Add((tmp[j], CoursesItemState.NotYetProcessed));
            tmp.RemoveAt(j);
        }
    }

    public void MinigameStart()
    {
        MusicManager.Instance.MusicNegociation();

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
            Debug.Log("réussite");
            MoneyManager.Instance.BuyFor(cost);

            float diff = woa.MaxPrize - woa.MinPrize;

            if (cost - woa.MinPrize <= diff * whatIsAGoodPrice)
            {
                Debug.Log("bon prix");
                state = CoursesItemState.BoughtAtGoodPrice;
            }
            else
            {
                Debug.Log("prix haut");
                state = CoursesItemState.BoughtAtHighPrice;
            }
        }
        else
        {
            Debug.Log("échec");
            state = CoursesItemState.NegociationFailed;
        }

        selectedWorkOfArts[i] = (woa, state);

        if (state == CoursesItemState.NegociationFailed)
            miniGame.ShowShame();

        OnMiniGameEnd.Invoke(state);

        MusicManager.Instance.MusicWorldmap();
        CameraManager.Instance.ZoomOut();
        PortManager.Instance.portActual.AlreadyVisited = true;
        ShipManager.Instance.shipCanTravel = true;
    }

    public void DeadEnd()
    {
        for (int i = 0; i < selectedWorkOfArts.Count; i++)
            if (selectedWorkOfArts[i].Item2 == CoursesItemState.NotYetProcessed || selectedWorkOfArts[i].Item2 == CoursesItemState.Processing)
                selectedWorkOfArts[i] = (selectedWorkOfArts[i].Item1, CoursesItemState.NegociationFailed);
    }

    public int ComputeScore()
    {
        int score = 0;

        foreach((WorkOfArt woa , CoursesItemState state) in selectedWorkOfArts)
        {
            Debug.Log(woa.name + " : " + state);
            switch(state)
            {
                case CoursesItemState.NotYetProcessed:
                    return -1;
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
        }

        return score;
    }
}