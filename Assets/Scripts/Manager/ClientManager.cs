using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientManager : MonoBehaviour
{
    [Header("Client")]
    public int clientAwardMoney;

    public static ClientManager Instance;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void ClientGoalAchieved()
    {
        ShipManager.Instance.shipCanTravel = true;
        MoneyManager.Instance.ClientGive(clientAwardMoney);
    }
}
