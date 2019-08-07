using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    private static GameManager _instance;

    public GameObject player;


    public GameObject item;

    public DialogueSystem dialogueSystem;

    public DialogueUI dialogueUI;

    public Canvas canvas;

    public Dictionary<string, Transform> dialogueBoxPosition = new Dictionary<string, Transform>();

    public Dictionary<string, GameObject> speechBubble = new Dictionary<string, GameObject>();

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameManager>();
            }

            return _instance;
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        dialogueSystem = Object.FindObjectOfType<DialogueSystem>();
        dialogueUI = Object.FindObjectOfType<DialogueUI>();
        canvas = Object.FindObjectOfType<Canvas>();
        player = GameObject.FindWithTag("Player");
    }
}