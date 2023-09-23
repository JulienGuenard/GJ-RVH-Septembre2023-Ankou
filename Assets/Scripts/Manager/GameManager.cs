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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            MusicManager.instance.MusicClear();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
    }
}
