using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionIcon : MonoBehaviour {

    [SerializeField]
    private GameObject interactionIcon;

    void Start()
    {
        interactionIcon.SetActive(false); // set the interaction icon to false by default.
    }

    void Update()
    {
        if(GameManager.Instance.dialogueSystem.runningDialogue == true)
        {
            interactionIcon.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            // turn on icon if player enters trigger.
            interactionIcon.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            // turn off the icon if player leaves trigger
            interactionIcon.SetActive(false);
    }
}
