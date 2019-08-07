using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDisplay : MonoBehaviour
{

    public Word word;
    public Sprite icon;
    public Image image;
    public void Setup(Word _word)
    {
        Debug.Log(_word);
        word = _word;
        icon = _word.icon;

        image  = gameObject.GetComponent<Image>();
        image.sprite = icon;
    }
}
