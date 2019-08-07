using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

struct DialogueButtonData
{
    public GameObject button;
    public Button unityButton;
    public TMPro.TextMeshProUGUI buttonText;
};

public class DialogueUI : MonoBehaviour
{
	private Transform owner;
    [SerializeField]
    private TextMeshProUGUI textRenderer;
    [SerializeField]
    private RectTransform dialoguePanel;
    [SerializeField]
    private GameObject dialogueBody;
    [SerializeField]
    private float textSpeed = 0.0001f;
    public bool isPrinting;
    private Vector2 originalDimensions, originalTextDimensions;
    public GameObject buttonPrefab;
    private Queue<DialogueButtonData> createdButtons = new Queue<DialogueButtonData>();
    private List<DialogueButtonData> activeButtons = new List<DialogueButtonData>();

    private GameObject instance = null;
    private GameObject speechBubble;
    public TMPro.TextMeshProUGUI lineUI;

    [SerializeField]
    public const int numButtons = 2;

    void Awake()
    {
 
        
        if (buttonPrefab)
        {
            for (int i = 0; i < numButtons; i++)
            {
                GameObject button = GameObject.Instantiate(buttonPrefab, gameObject.transform);
                button.SetActive(false);

                DialogueButtonData data = new DialogueButtonData();
                data.button = button;
                data.unityButton = button.GetComponent<Button>();
                data.buttonText = button.GetComponentInChildren<TMPro.TextMeshProUGUI>();

                createdButtons.Enqueue(data);
            }
        }

        if (lineUI != null)
        {
            lineUI.enabled = false;
        }
    }

    private void OnEnable()
    {
        GH.EventSystem.instance.AddListener<DialogueUIOption>(ShowUI);
        GH.EventSystem.instance.AddListener<ShowSpokenLine>(ShowSpeakerLine);
        GH.EventSystem.instance.AddListener<HideUI>(HideDialogueUI);
    
    }

    private void OnDisable()
    {
        GH.EventSystem.instance.RemoveListener<DialogueUIOption>(ShowUI);
        GH.EventSystem.instance.RemoveListener<ShowSpokenLine>(ShowSpeakerLine);
        GH.EventSystem.instance.RemoveListener<HideUI>(HideDialogueUI);

    }

    private void ShowSpeakerLine(ShowSpokenLine data)
    {
        //Debug.Log("it showed the line");
        var canvas = GameManager.Instance.canvas;
        GameManager.Instance.dialogueBoxPosition.TryGetValue(data.ID, out owner);
        GameManager.Instance.speechBubble.TryGetValue(data.ID, out speechBubble);
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(owner.position);
        if(instance == null)
        {
                instance = Object.Instantiate(speechBubble, screenPoint, Quaternion.identity, canvas.transform);
        }
        lineUI = instance.GetComponentInChildren<TextMeshProUGUI>();
        if (lineUI)
        {
            //SetLayerPosition();
            lineUI.SetText(data.line);
            //CorrectOverflow();
            lineUI.enabled = true;
            StartCoroutine(WriteText(data.line));
            //Debug.Log("It showed the fucking dialogue");
        }
    }

    private void HideDialogueUI(HideUI data)
    {
        if (lineUI)
        {
            lineUI.enabled = false;
        }

        foreach(DialogueButtonData button in activeButtons)
        {
            button.button.SetActive(false);
            button.unityButton.onClick.RemoveAllListeners();
            createdButtons.Enqueue(button);
        }
        Destroy(instance);
        instance = null;
        activeButtons.Clear();
    }

    //SHOW DIALOGUE
    void ShowUI(DialogueUIOption option)
    {
        if(option.line.responses.Length > 1)
        {

            int index = 0;
            
            foreach (DialogueOption choice in option.line.responses)
            {
                try
                {
                    DialogueButtonData button = createdButtons.Dequeue();
                    button.buttonText.SetText(choice.line);

                    button.unityButton.onClick.AddListener(option.onclickEvent[index]);

                    button.button.SetActive(true);

                    activeButtons.Add(button);
                    index++;
                }
                catch(System.Exception e)
                {
                      Debug.Log(e);
                }
               
            } 
        } 
    }

    private IEnumerator WriteText(string line)
    {
        isPrinting = true;

        for (int i = 0; i < lineUI.text.Length; i++)
        {
            lineUI.maxVisibleCharacters = i + 1;

            yield return new WaitForSeconds(textSpeed);
        }

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            if (isPrinting == true)
            {
                lineUI.maxVisibleCharacters = lineUI.text.Length;
            }

            isPrinting = false;
        }

        
    }

    private void MoveDialogueBox()
    {
        transform.position = owner.position;
    }
    public void SetOwner(Transform newOwner)
    {
        owner = newOwner;
    }

    private void CorrectOverflow()
    {
        if (textRenderer.isTextOverflowing)
        {
            dialoguePanel.sizeDelta += new Vector2(0, textRenderer.fontSize + 5f); // the 5f is a constant that makes the bubble larger than the text area.
            textRenderer.rectTransform.sizeDelta += new Vector2(0, textRenderer.fontSize);
        }
    }

    private void SetLayerPosition()
    {
        transform.SetAsLastSibling();
    }


}
