using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private FMOD.Studio.EventInstance music;
    private FMOD.Studio.EventInstance shipTravel;

    public static MusicManager Instance;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        MusicWorldmap();
    }

    public void MusicClear()
    {
        music.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        shipTravel.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void MusicWorldmap()
    {
        music.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        music.release();
        music = FMODUnity.RuntimeManager.CreateInstance("event:/Music_Navigation_Map");
        music.start();
        music.release();
    }

    public void MusicNegociation()
    {
        music.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        music.release();
        music = FMODUnity.RuntimeManager.CreateInstance("event:/Music_Negociation");
        music.start();
        music.release();
    }

    public void SFXTravelStart()
    {
        shipTravel.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        shipTravel.release();
        shipTravel = FMODUnity.RuntimeManager.CreateInstance("event:/Sailing_Boat_Moving");
        shipTravel.start();
        shipTravel.release();
    }

    public void SFXTravelStop()
    {
        shipTravel.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        shipTravel.release();
    }
}
