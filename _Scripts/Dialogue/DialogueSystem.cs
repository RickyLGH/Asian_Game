using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GH;

public class DialogueUIOption : GH.Event
{
    public DialogueOption line;
    public UnityAction[] onclickEvent;

    int[] numbers;
}

public class ShowSpokenLine : GH.Event
{
    public string line;
    public string ID;
}

public class HideUI : GH.Event
{

}

public class SetLine : GH.Event
{
    public DialogueOption dialogueOption;
}

public class DialogueSystem : MonoBehaviour
{
    public DialogueDatabase database;
    public float m_dialogueWaitTime = 0.5f;

    private float m_currentTime = 0.0f;
    private bool m_timerStarted = false;

    public bool runningDialogue = false;
    AudioSource source;
    Dictionary<string, DialogueOption> dialogueTable = new Dictionary<string, DialogueOption>();

    public DialogueOption startingLine;
    public DialogueOption m_currentLine = null;

    void Awake()
    {
        LoadDatabase(database);
        source = GetComponent<AudioSource>();
        StartLine(startingLine.name);
    }

    public void LoadDatabase(DialogueDatabase newDatabase)
    {
        database = newDatabase;

        //dialogueTable.Clear();
        
        foreach (DialogueOption option in database.dialogueLines)
        {
            dialogueTable.Add(option.name, option);
        }
    }

    void Update()
    {
        if (m_timerStarted)
        {
            m_currentTime += Time.deltaTime;
          
        }
        if (Input.GetMouseButtonDown(0)|| Input.GetKeyDown(KeyCode.Return))
        {
            if (runningDialogue == true)
            {
                CheckResponses();
            }
        }
    }

    void CheckResponses()
    {      
       //if (m_currentTime >= m_dialogueWaitTime)
       {
           m_currentTime = 0.0f;
           m_timerStarted = false;
            if(GameManager.Instance.dialogueUI.isPrinting != true)
            {
                //Debug.Log("Current Line UI: " + m_currentLine);
                //Debug.Log("Line Responses UI: " + m_currentLine.responses.Length);
                if (m_currentLine.responses.Length == 1)
                {
                    //Debug.Log("1 line of dialogue is called");
                    DialogueUIOption data = new DialogueUIOption();
                    data.line = m_currentLine;
                    Debug.Log(m_currentLine);
                    int index = 0;
                    NextLine(index);
           
                    GH.EventSystem.instance.RaiseEvent(data);
                }
                else if (m_currentLine.responses.Length > 1)
                {
                    //Debug.Log("Choices called");
                    DialogueUIOption data = new DialogueUIOption();
                    data.line = m_currentLine;
                    data.onclickEvent = new UnityAction[m_currentLine.responses.Length];
                    //Debug.Log(data.line);
                    int index = 0;
                    foreach (DialogueOption response in m_currentLine.responses)
                    {
                        int currentIndex = index;
                       // Debug.Log(index);
                        data.onclickEvent[index] = () => { this.NextLine(currentIndex); };
                        //Debug.Log(data.onclickEvent[currentIndex]);
                        index++;
                    }

                    GH.EventSystem.instance.RaiseEvent(data);
                }
                else
                {
                    GH.EventSystem.instance.RaiseEvent(new HideUI());
                    runningDialogue = false;
                }
            }
       }
        
    }
    void StartLine(DialogueOption option)
    {
        if (option != null)
        {
            m_currentLine = option;

            //Debug.Log("New Line: " + m_currentLine);
            if (m_currentLine.lineAudio != null)
            {
                source.PlayOneShot(m_currentLine.lineAudio);
                m_dialogueWaitTime = m_currentLine.lineAudio.length;
            }
            //Debug.Log("SHow the Fucking Dialogue: " + m_currentLine);
            
            GH.EventSystem.instance.RaiseEvent(new ShowSpokenLine { line = m_currentLine.line, ID = m_currentLine.Speaker } );
            
            m_timerStarted = true;
            m_currentTime = 0.0f;
            runningDialogue = true;
        }
    }

    public void StartLine(string dialogueLineName)
    {
        DialogueOption option = null;
        dialogueTable.TryGetValue(dialogueLineName, out option);
        StartLine(option);
    }

    public void NextLine(int lineIndex)
    {
        
        GH.EventSystem.instance.RaiseEvent(new HideUI());
        //Debug.Log(string.Format("Next Line called: {0}", lineIndex));
        //Debug.Log("Actual Line: " + m_currentLine);
        if (lineIndex == m_currentLine.responses.Length)
        {
            StartLine(m_currentLine.responses[lineIndex - 1]);
        }
        else if (lineIndex < m_currentLine.responses.Length)
        {
            StartLine(m_currentLine.responses[lineIndex]);
        }
    }
}
