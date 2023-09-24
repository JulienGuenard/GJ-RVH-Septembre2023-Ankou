using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager instance;
    public static GameManager Instance => instance;

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
    private UIManager uiManager;
    [SerializeField]
    private Port lastPort;
    [SerializeField]
    private List<Port> gamePorts;
    public List<Port> GamePorts => gamePorts;
    [SerializeField]
    private Material notVisitedYetArrowMaterial;
    [SerializeField]
    private Material visitedArrowMaterial;

    [Header("Data")]
    public Port portActual;
    [SerializeField]
    private float minimumMoneyToNegociate;
    public bool CanStartNegociation => money >= minimumMoneyToNegociate;

    [SerializeField]
    private float startMoney;

    private float money;
    public float Money => money;

    public void ResetGame()
    {
        money = startMoney;

        MinigameManager.Instance.ResetGame();

        foreach (Port p in gamePorts)
            p.AlreadyVisited = false;
    }

    public bool ReadyToEndGame
    {
        get
        {
            foreach (Port p in gamePorts)
                if (!p.AlreadyVisited)
                    return false;

            return true;
        }
    }

    public void UpdateArrow(Transform transform, bool isLastPort, bool alreadyVisited)
    {
        if (isLastPort && !ReadyToEndGame)
            transform.gameObject.SetActive(false);

        if(!isLastPort)
        {
            if (!alreadyVisited)
                transform.GetComponent<MeshRenderer>().material = notVisitedYetArrowMaterial;
            else
                transform.GetComponent<MeshRenderer>().material = visitedArrowMaterial;
        }
    }

    public void BuyFor(float cost)
    {
        money -= cost;
        uiManager.UpdateMoney(money);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            MusicManager.instance.MusicClear();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void LaunchScore()
    {
        uiManager.LaunchScore(MinigameManager.Instance.ComputeScore());
    }
}
