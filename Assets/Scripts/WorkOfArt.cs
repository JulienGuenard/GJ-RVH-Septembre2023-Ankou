using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Work of Art")]
public class WorkOfArt : ScriptableObject
{
    public float MinPrize;
    public float MaxPrize;
    [Multiline]
    public string Description;
    public Sprite Sprite;
}
