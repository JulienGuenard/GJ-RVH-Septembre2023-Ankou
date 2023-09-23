using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    public List<GameObject> minigameGMBList;
    public MiniGame minigame;
    [SerializeField]
    private WorkOfArt testWOA;

    public static MinigameManager instance;

    void Awake()
    {
        if (instance == null) instance = this;
    }

    public void MinigameStart()
    {
        MusicManager.instance.MusicNegociation();
        foreach (GameObject obj in minigameGMBList) obj.SetActive(true);
        minigame.StartNegociations(testWOA);
    }

    public void MinigameEnd()
    {
        MusicManager.instance.MusicWorldmap();
        foreach (GameObject obj in minigameGMBList) obj.SetActive(false);
        ShipManager.instance.shipCanTravel = true;
    }
}
