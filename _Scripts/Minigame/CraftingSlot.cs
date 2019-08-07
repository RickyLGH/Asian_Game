using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class CraftingSlot : MonoBehaviour, IDropHandler
{
    public int slot;


    //define custome methods called but are treated as variables
    public GameObject itemObj
    {
        get
        {
            if (transform.childCount > 0)
            {
                return transform.GetChild(0).gameObject;
            }
            return null;
        }

    }

    //Attaches the object to the inventory
    public void OnDrop(PointerEventData eventData)
    {
        if (!itemObj)
        {
            DragHandler.itemBeingDragged.transform.SetParent(transform);

            Combiner.instance.AddWord(DragHandler.GetWordBeingDragged(), slot);

            Debug.Log(DragHandler.GetWordBeingDragged());
        }
    }

    void Update()
    {
        if (itemObj != null)
        {
            if (itemObj.transform.parent != transform)
            {
                Combiner.instance.RemoveWord(slot);
            }
        }
    }
}