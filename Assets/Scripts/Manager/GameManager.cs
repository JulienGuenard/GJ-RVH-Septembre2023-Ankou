using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [Header("Start")]
    public Port portActual;

    [SerializeField]
    private float money;
    public float Money => money;

    [SerializeField]
    private UIManager uiManager;

    public void BuyFor(float cost)
    {
        money -= cost;
        uiManager.UpdateMoney(money);
    }

    public void Victory()
    {

    }
}
