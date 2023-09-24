using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class UIPlayerOneShot : MonoBehaviour
{


    public EventReference soundEvent;
    public void PlaySoundEvent()
    {
        RuntimeManager.PlayOneShot(soundEvent);
    }
}
