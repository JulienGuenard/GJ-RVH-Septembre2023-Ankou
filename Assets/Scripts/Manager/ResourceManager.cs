using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceManager : MonoBehaviour
{
    [Header("Player Resources")]
    public int money;

    [Header("References")]
    public TextMeshProUGUI UIText;

    public static ResourceManager instance;

    void Awake()
    {
        if (instance == null) instance = this;
    }

    void Update()
    {
        UIText.text = " : " + money;
    }
}
