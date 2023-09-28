using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
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

    [Serializable]
    public struct PersonnageData
    {
        public Sprite Sprite;
        public string Name;
    }

    [Header("Refs")]
                        public UIManager uiManager;
    [SerializeField]    private Material notVisitedYetArrowMaterial;
    [SerializeField]    private Material visitedArrowMaterial;

    [Header("Data")]
    [SerializeField]    public Sprite PlayerData;
    [SerializeField]    private List<PersonnageData> merchantData;

    public (Sprite, string) MerchantData
    {
        get
        {
            int i = UnityEngine.Random.Range(0, merchantData.Count);
            return (merchantData[i].Sprite, merchantData[i].Name);
        }
    }
    
    private void Start()
    {
        Debug.Log("StartGame");
        ResetGame();
    }

    public void ResetGame()
    {
        MoneyManager mm = MoneyManager.Instance;

        Debug.Log("ResetGame");
        mm.money = mm.startMoney;

        MinigameManager.Instance.ResetGame();
        uiManager.UpdateMoney(mm.money);

        PortManager pm = PortManager.Instance;

        ShipSpawn();

        foreach (Port p in pm.gamePorts)
            p.AlreadyVisited = false;

        pm.lastPort.isLastPort = true;
        pm.lastPort.AlreadyVisited = false;
    }

    public bool ReadyToEndGame
    {
        get
        {
            PortManager pm = PortManager.Instance;
            foreach (Port p in pm.gamePorts)
                if (!p.AlreadyVisited)
                    return false;

            return true;
        }
    }

    public void GameOver()
    {
        PortManager pm = PortManager.Instance;
        MinigameManager.Instance.DeadEnd();

        foreach (Port p in pm.gamePorts)
            p.AlreadyVisited = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            MusicManager.Instance.MusicClear();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void LaunchScore()
    {
        uiManager.LaunchScore(MinigameManager.Instance.ComputeScore());
    }

    private void ShipSpawn()
    {
        Transform ship = ShipManager.Instance.playerShip.transform;
        Transform dock = PortManager.Instance.lastPort.dock.transform;

        ship.localPosition = dock.position;
        ship.rotation = dock.rotation;

        ship.GetComponent<NavMeshAgent>().enabled = true;
    }

}