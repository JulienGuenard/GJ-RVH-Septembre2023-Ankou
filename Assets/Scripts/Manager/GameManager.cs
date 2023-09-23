using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Start")]
    public Port portActual;
     
    public static GameManager instance;

    void Awake()
    {
        if (instance == null) instance = this;
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
