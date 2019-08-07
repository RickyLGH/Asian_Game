using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CraftableItem : MonoBehaviour, IPointerEnterHandler
{
    public int requiredItems;
    public GameObject[] items;

    private bool hovered;

    public void Start()
    {
       
    }
    public void Update()
    {
        if(hovered == true)
        {

        }
    }
	public void OnPointerEnter(PointerEventData eventData)
    {
        hovered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hovered = false;
    }

}
