using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private StudioEventEmitter soundEmitter;

    public void Play()
    {
        soundEmitter.Play();
    }

    public void Stop()
    {
        soundEmitter.Stop();
    }
}
