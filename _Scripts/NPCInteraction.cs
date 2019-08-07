using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : MonoBehaviour {

    public DialogueOption option;

    public bool canTalk = false;
    void Update()
    {
        if(GameManager.Instance.dialogueSystem.runningDialogue != true)
        {
            if(canTalk == true)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    GameManager.Instance.dialogueSystem.StartLine(option.name);   // Player input to interact with an NPC
                }
            }                  
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
 
        canTalk = true;
        Debug.Log(canTalk);
    }
    void OnTriggerExit2D(Collider2D other)
    {
        canTalk = false;
    }
}
