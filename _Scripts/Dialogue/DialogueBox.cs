using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBox : MonoBehaviour {

    public Transform dialogueTransform;

    public string ID;
    public GameObject box;
    void Awake()
    {
        
        FindTransform();
    }
    
    void FindTransform()
    {
        dialogueTransform = gameObject.transform;
        GameManager.Instance.dialogueBoxPosition.Add(ID, dialogueTransform);
        GameManager.Instance.speechBubble.Add(ID, box);
    }
}
