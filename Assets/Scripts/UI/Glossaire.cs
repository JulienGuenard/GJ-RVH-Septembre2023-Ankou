using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Glossaire : MonoBehaviour
{
    [Header("Data Refs")]
    [SerializeField]
    private List<WorkOfArt> workOfArts;
    [SerializeField]
    private GameObject pagePrefab;
    [SerializeField]
    private GameObject feuillet;
    [SerializeField]
    private GameObject util;
    [SerializeField]
    private Button prec;
    [SerializeField]
    private Button suiv;

    [Header("Zoom")]
    [SerializeField]
    private GameObject aggParent;
    [SerializeField]
    private float zoomingTime;
    [SerializeField]
    private RectTransform bigImageTransform;
    [SerializeField]
    private GameObject growingImage;
    [SerializeField]
    private Button btn;

    private List<PageGlossaire> pages = new();
    private RectTransform lastRectTransform;
    private int index = 0;

    private void Start()
    {
        for(int i=0; i<(workOfArts.Count/4+(workOfArts.Count % 4 > 0 ? 1 : 0)); i++)
        {
            GameObject go = Instantiate(pagePrefab, feuillet.transform);

            List<WorkOfArt> list = new();

            for (int j = 0; j < 4 && i * 4 + j < workOfArts.Count; j++)
                list.Add(workOfArts[i * 4 + j]);

            go.GetComponent<PageGlossaire>().SetUp(list, ChangeSize);
            go.SetActive(false);

            pages.Add(go.GetComponent<PageGlossaire>());
        }
    }

    public void ChangeSize(Sprite sprite)
    {
        aggParent.SetActive(true);
        growingImage.GetComponent<Image>().sprite = sprite;
        btn.enabled = true;
    }

    public void Precedente()
    {
        if(index==0)
        {
            Close();
            return;
        }

        pages[index].gameObject.SetActive(false);
        index--;
        pages[index].gameObject.SetActive(true);
    }

    public void Suivante()
    {
        if (index == pages.Count-1)
            return;

        pages[index].gameObject.SetActive(false);
        index++;
        pages[index].gameObject.SetActive(true);
    }

    public void Open()
    {
        pages[0].gameObject.SetActive(true);
        util.SetActive(true);
    }

    public void Close()
    {
        pages[0].gameObject.SetActive(false);
        util.SetActive(false);
    }

    //Test
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
            Open();
    }
}