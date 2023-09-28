using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private bool cheatCodeActive;

    [Header("HUD")]
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private GameObject PNJPanel;

    [SerializeField]
    private GameObject listeDeCourses;

    [Header("Game End")]
    [SerializeField] private GameObject EndPanel;

    [Space]
    [SerializeField] private GameObject recapCiceron;
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private TextMeshProUGUI text75;
    [SerializeField] private TextMeshProUGUI text50;
    [SerializeField] private TextMeshProUGUI text25;
    [SerializeField] private TextMeshProUGUI text20;
    [Space]
    [SerializeField]
    private GameObject recapOeuvres;
    [SerializeField] private GameObject woa1;
    [SerializeField] private GameObject woa2;
    [SerializeField] private GameObject woa3;
    [SerializeField] private GameObject woa4;
    [Space]
    [SerializeField] private Glossaire glossaire;

    public static UIManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        UpdateMoney(MoneyManager.Instance.Money);
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
                CoursesItemState.NegociationFailed => "NF",
                _ => "1?"
            };
        }
    }
    
    public void UpdateMoney(float money)
    {
        moneyText.text = $" {money}";
    }

    public void CiceronToRecap()
    {
        text75.gameObject.SetActive(false);
        text50.gameObject.SetActive(false);
        text25.gameObject.SetActive(false);
        text20.gameObject.SetActive(false);

        recapCiceron.SetActive(false);
        recapOeuvres.SetActive(true);

        FillRecapWoa(woa1, MinigameManager.Instance.Selection[0]);
        FillRecapWoa(woa2, MinigameManager.Instance.Selection[1]);
        FillRecapWoa(woa3, MinigameManager.Instance.Selection[2]);
        FillRecapWoa(woa4, MinigameManager.Instance.Selection[3]);
    }

    private void FillRecapWoa(GameObject woaGO, (WorkOfArt data, CoursesItemState state) woa)
    {
        switch(woa.state)
        {
            case CoursesItemState.NotYetProcessed:
            case CoursesItemState.Processing:
            case CoursesItemState.NegociationFailed:
                woaGO.SetActive(false);
                break;
            default:
                woaGO.SetActive(true);
                woaGO.transform.GetChild(0).GetComponent<Image>().sprite = woa.data.Illustration;
                woaGO.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = woa.data.Description;
                break;
        }
    }

    public void LaunchScore(int v)
    {
        Debug.Log($"launch score={v}");
        EndPanel.SetActive(true);

        text75.gameObject.SetActive(false);
        text50.gameObject.SetActive(false);
        text25.gameObject.SetActive(false);
        text20.gameObject.SetActive(false);

        recapCiceron.SetActive(true);

        score.text = $"Complété à {v}%";

        if (v >= 75)
            text75.gameObject.SetActive(true);
        else if (v >= 50)
            text50.gameObject.SetActive(true);
        else if(v >= 25)
            text25.gameObject.SetActive(true);
        else
            text20.gameObject.SetActive(true);
    }

    public void PNJPanelShow()
    {
        PNJPanel.SetActive(true);
    }

    public void PNJPanelHide()
    {
        PNJPanel.SetActive(false);
        CameraManager.Instance.Zoom();
    }

    private void Update()
    {
        if (!cheatCodeActive)
            return;

        if (Input.GetKeyDown(KeyCode.G))
        {
            EndPanel.SetActive(true);
            LaunchScore(100);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            EndPanel.SetActive(true);
            LaunchScore(60);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            EndPanel.SetActive(true);
            LaunchScore(5 * 3 + 15);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            EndPanel.SetActive(true);
            LaunchScore(20);
        }
    }
}
