using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using TMPro;


public class TextScroller : MonoBehaviour
{
    [TextArea(4, 100)]
    public string bigDialogueLine;

    public TextMeshProUGUI textField = null;
    public string[] dialogueLines;
    public int maxLines = 7;

    private int lineNumber = 0;
    private int appendedLines = 0;
    private StringBuilder dialogue = new StringBuilder();

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            dialogue.AppendLine(dialogueLines[lineNumber]);
            ++lineNumber;
            ++appendedLines;

            if (appendedLines > maxLines)
            {
                string dialogueString = dialogue.ToString();
                Debug.Log(dialogueString);

                int firstNewLineIndex = dialogueString.IndexOf("\n") + 1;
                dialogue.Remove(0, firstNewLineIndex);
                --appendedLines;
            }

            textField.text = dialogue.ToString();
        }
    }
}