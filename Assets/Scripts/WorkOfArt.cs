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
    public Texture2D Sprite;
    public Sprite Picture;
    public string lien;
}
