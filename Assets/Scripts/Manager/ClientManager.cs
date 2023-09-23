using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientManager : MonoBehaviour
{
    [Header("Client")]
    public Award clientAward;
    public int clientAwardMoney;

    public static ClientManager instance;

    void Awake()
    {
        if (instance == null) instance = this;
    }

    public void ClientGoalAchieved()
    {
        switch (clientAward)
        {
            case Award.Money:
                {
                    ShipManager.instance.playerShip.workofartList.Clear();
                    ShipManager.instance.shipCanTravel = true;
                    ResourceManager.instance.money += clientAwardMoney;
                    break;
                }

            case Award.Victory:
                {
                    ShipManager.instance.shipCanTravel = true;
                    /* à remplir */
                    break;
                }
        }
    }
}
