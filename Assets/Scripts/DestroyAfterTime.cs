using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [Header("Time")]
    public float time;
        
        void Start()
    {
        StartCoroutine(DestroyAfterXTime());
    }

    IEnumerator DestroyAfterXTime()
    {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }
}
