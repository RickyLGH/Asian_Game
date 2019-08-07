using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public bool hasMop = false;

    bool lockedPlayerMovement;

    public float interactionRadius = 2.0f;

    public Animator anim;

    public float minPosition = -5.3f;
    public float maxPosition = 5.3f;

    public float moveSpeed = 1.0f;

    public float movementFromButtons { get; set; }

    void Awake()
    {
        anim = GetComponent<Animator>();
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;

        // Flatten the sphere into a disk, which looks nicer in 2D games
        Gizmos.matrix = Matrix4x4.TRS(transform.position, Quaternion.identity, new Vector3(1, 1, 0));

        // Need to draw at position zero because we set position in the line above
        Gizmos.DrawWireSphere(Vector3.zero, interactionRadius);
    }

    void FixedUpdate()
    {
        if (lockedPlayerMovement)
        {
            return;
        }
        // Move the player, clamping them to within the boundaries 
        // of the level.
        var movement = Input.GetAxis("Horizontal");
        movement += movementFromButtons;
        movement *= (moveSpeed * Time.deltaTime);
        var newPosition = transform.position;
        newPosition.x += movement;
        newPosition.x = Mathf.Clamp(newPosition.x, minPosition, maxPosition);

        transform.position = newPosition;
    }



    void LockPlayerMovement()
    {
        lockedPlayerMovement = true;
    }

    void UnlockPlayerMovement()
    {
        lockedPlayerMovement = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Player has collided");
        if (hasMop == true && other.tag == "Dust")
        {
            other.gameObject.SetActive(false);
        }
    }
}
