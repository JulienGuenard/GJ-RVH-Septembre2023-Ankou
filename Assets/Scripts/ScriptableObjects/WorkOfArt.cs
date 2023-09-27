using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "ScriptableObjects/Work of Art")]
public class WorkOfArt : ScriptableObject
{
    public float MinPrize;
    public float MaxPrize;
    [Multiline]
    public string Description;
    public Sprite Illustration;
    public Sprite Picture;
    public string Lien;
}
