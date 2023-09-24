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
        ShipManager sm = ShipManager.Instance;

        switch (clientAward)
        {
            case Award.Money:
                {
                    sm.playerShip.workofartList.Clear();
                    sm.shipCanTravel = true;
                    GameManager.Instance.ClientGive(clientAwardMoney);
                    break;
                }

            case Award.Victory:
                {
                    sm.shipCanTravel = true;
                    /* à remplir */
                    break;
                }
        }
    }
}
