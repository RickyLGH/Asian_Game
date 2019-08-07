using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Karma
{
    GOOD,
    NEUTRAL,
    BAD
};

[CreateAssetMenu(menuName = "DialogueOption")]
public class DialogueOption : ScriptableObject
{
    [TextArea(4, 100)]
    public string line;
    public Sprite sprite;
    public AudioClip lineAudio;
    public string Speaker;
    public Karma karma;
  
    public DialogueOption[] responses;
};
