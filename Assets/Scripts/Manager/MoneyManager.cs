using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    [SerializeField]
    public float startMoney;

    public float money;
    public float Money => money;
    [SerializeField] private float minimumMoneyToNegociate;
    public bool CanStartNegociation => money >= minimumMoneyToNegociate;

    public static MoneyManager Instance;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void BuyFor(float cost)
    {
        GameManager gm = GameManager.Instance;
        money -= cost;
        gm.uiManager.UpdateMoney(money);

        if (!CanStartNegociation)
            gm.GameOver();
    }

    public void ClientGive(float award)
    {
        money += award;
        GameManager.Instance.uiManager.UpdateMoney(money);
    }
}
