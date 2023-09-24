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
    public Sprite PlayerData;
    [SerializeField]
    private List<PersonnageData> merchantData;

    public (Sprite, string) MerchantData
    {
        get
        {
            int i = UnityEngine.Random.Range(0, merchantData.Count);
            return (merchantData[i].Sprite, merchantData[i].Name);
        }
    }

    [SerializeField]
    private float startMoney;

    private float money;
    public float Money => money;

    private void Start()
    {
        Debug.Log("StartGame");
        ResetGame();
    }

    public void ResetGame()
    {
        Debug.Log("ResetGame");
        money = startMoney;

        MinigameManager.Instance.ResetGame();
        uiManager.UpdateMoney(money);

        foreach (Port p in gamePorts)
            p.AlreadyVisited = false;

        lastPort.isLastPort = true;
        lastPort.AlreadyVisited = false;
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
        Debug.Log("update arrow " + transform.parent.name + " last ? " + isLastPort);
        if (isLastPort)
        {
            Debug.Log($"ReadyToEndGame ? {ReadyToEndGame}");

            if (!ReadyToEndGame)
                transform.gameObject.SetActive(false);
            else
                transform.gameObject.SetActive(true);

            return;
        }

        if (!alreadyVisited)
            transform.gameObject.SetActive(true);
            //transform.GetComponent<MeshRenderer>().material = notVisitedYetArrowMaterial;
        else
            transform.gameObject.SetActive(false);
            //transform.GetComponent<MeshRenderer>().material = visitedArrowMaterial;

        foreach (Port p in gamePorts)
            if (!p.AlreadyVisited)
                return;

        lastPort.AlreadyVisited = false;
    }

    public void BuyFor(float cost)
    {
        money -= cost;
        uiManager.UpdateMoney(money);
        ShipManager.Instance.playerShip.AddToCargaison(MinigameManager.Instance.Selection[0].Item1);

        if (!CanStartNegociation)
            GameOver();
    }

    public void GameOver()
    {
        MinigameManager.Instance.DeadEnd();

        foreach (Port p in gamePorts)
            p.AlreadyVisited = true;
    }

    public void ClientGive(float award)
    {
        money += award;
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

[Serializable]
public struct PersonnageData
{
    public Sprite Sprite;
    public string Name;
}