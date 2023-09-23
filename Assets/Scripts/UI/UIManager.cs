using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI moneyText;

    private void Start()
    {
        UpdateMoney(GameManager.Instance.Money);
    }

    public void UpdateMoney(float money)
    {
        moneyText.text = $" {money}";
    }
}
