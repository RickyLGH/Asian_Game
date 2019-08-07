using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    //Reference to Object
    public static GameObject itemBeingDragged;
    //Object Starting Position
    Vector3 startPosition;
    Transform startParent;
    public Camera mainCamera = null;
    private Ray ray;
    public BoxCollider2D box;
    public CharacterController player;
    public ItemDisplay itemDisplay;
    
    public static Word GetWordBeingDragged()
    {
        return itemBeingDragged.GetComponent<ItemDisplay>().word;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        //Sets item to the gameobject
        itemBeingDragged = gameObject;
        //References their starting transform
        startPosition = transform.position;
        startParent = transform.parent;
        //Prevents Raycasts from screwing things
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        //Seceond Parent reference
        transform.SetParent(transform.root);
        itemDisplay = gameObject.GetComponent<ItemDisplay>();
    
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Moves the Item
        transform.position = Input.mousePosition;

        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        bool hitPlayer = false;

        //Sets it back to the starting position
        itemBeingDragged = null;
        //Checks for a new transform
        if (transform.parent == startParent || transform.parent == transform.root)
        {
            transform.position = startPosition;
            transform.SetParent(startParent);
        }
        
        if(itemDisplay.word.name == "Broom")
        {
            var player = GameManager.Instance.player;
            box = player.GetComponent<BoxCollider2D>();
            var hitbox = box.size * box.transform.lossyScale;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos = new Vector2(worldPos.x, worldPos.y);
            Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.y);
            Ray2D ray = new Ray2D(mousePos, playerPos - mousePos);

            RaycastHit2D rh;
            int playerLayerMask = 1 << LayerMask.NameToLayer("Player");

            rh = Physics2D.Raycast(mousePos, ray.direction, hitbox.magnitude, playerLayerMask);
            if (rh.collider != null)
            {
                Debug.Log("Hit " + rh.transform.name);
                hitPlayer = true;

                CharacterController cc = rh.transform.gameObject.GetComponent<CharacterController>();
                cc.hasMop = true;
                Debug.Log("Player gets mop: " + cc.hasMop);
                Vector3 PositionForScreen = Camera.main.WorldToScreenPoint(rh.transform.position);
                Object.Instantiate(GameManager.Instance.item, rh.transform);
            }
        }
       

        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void OnDrawGizmos()
    {
        
        Vector3 end = ray.origin + ray.direction * 100f;
        Debug.DrawLine(ray.origin, end, Color.green);
    }
}
