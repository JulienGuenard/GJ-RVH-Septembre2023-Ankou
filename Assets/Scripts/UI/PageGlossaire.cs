using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PageGlossaire : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> elements;

    public void SetUp(List<WorkOfArt> list, Action<Sprite> changeSize)
    {
        for(int i=0; i<list.Count; i++)
        {
            Transform t = elements[i].transform;

            Sprite illu = list[i].Illustration;
            t.GetChild(0).GetComponent<Image>().sprite = illu;
            t.GetChild(0).GetComponent<Button>().onClick.AddListener(() => changeSize.Invoke(illu));
            Sprite pic = list[i].Picture;
            t.GetChild(1).GetComponent<Image>().sprite = pic;
            t.GetChild(1).GetComponent<Button>().onClick.AddListener(() => changeSize.Invoke(pic));
            t.GetChild(2).GetComponent<TextMeshProUGUI>().text = list[i].Description;
            string link = list[i].Lien;
            t.GetChild(3).GetComponent<Button>().onClick.AddListener(() => Application.OpenURL(link));
        }

        if(elements.Count>list.Count)
            for (int i = list.Count; i < elements.Count; i++)
                elements[i].SetActive(false);
    }
}
