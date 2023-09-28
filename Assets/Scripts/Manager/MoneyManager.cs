using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoneyManager : MonoBehaviour
{
    [SerializeField]
    public float startMoney;

    [SerializeField]
    private float money;
    public float Money
    {
        get => money;
        set { money = value; }
    }

    [SerializeField] private float minimumMoneyToNegociate;

    public bool CanStartNegociation => money >= minimumMoneyToNegociate;

    public static MoneyManager Instance;

    public UnityEvent OnBought;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public bool BuyFor(float cost)
    {
        if (cost > money)
            return false;

        GameManager gm = GameManager.Instance;
        money -= cost;
        gm.uiManager.UpdateMoney(money);

        if (!CanStartNegociation)
            gm.GameOver();

        OnBought.Invoke();
        return true;
    }

    public void ClientGive(float award)
    {
        money += award;
        GameManager.Instance.uiManager.UpdateMoney(money);
    }
}
