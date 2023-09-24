using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI moneyText;

    [SerializeField]
    private GameObject listeDeCourses;

    [SerializeField]
    private GameObject EndPanel;

    [SerializeField]
    private Glossaire glossaire;

    private bool glossaireOpen = false;

    private void Start()
    {
        UpdateMoney(GameManager.Instance.Money);
    }

    public void UpdateListeDeCourses()
    {
        MinigameManager mm = MinigameManager.Instance;

        for(int i=0; i < listeDeCourses.transform.childCount; i++)
        {
            Transform child = listeDeCourses.transform.GetChild(i);
            child.GetChild(0).GetComponent<TextMeshProUGUI>().text = mm.Selection[i].Item1.name;
            child.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = mm.Selection[i].Item2 switch
            {
                CoursesItemState.NotYetProcessed => "NP",
                CoursesItemState.Processing => "P...",
                CoursesItemState.BoughtAtGoodPrice => "GP",
                CoursesItemState.BoughtAtHighPrice => "HP",
                _ => "NF",
            };
        }
    }
    
    public void UpdateMoney(float money)
    {
        moneyText.text = $" {money}";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (!glossaireOpen)
            {
                EndPanel.SetActive(true);
                glossaire.Open();
                glossaireOpen = true;
            }
            else
            {
                glossaire.Close();
                EndPanel.SetActive(false);
                glossaireOpen = false;
            }
        }
    }
}
