using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
[CreateAssetMenu(fileName = "New Item",menuName = "Word")]
public class Word : ScriptableObject
{
    public Sprite icon;

    public Color customColor = Color.black;

}
