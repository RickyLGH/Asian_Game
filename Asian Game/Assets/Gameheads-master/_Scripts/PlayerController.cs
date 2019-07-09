using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float minPosition;
    public float maxPosition;

    public float moveSpeed = 1.0f;

    public float interactionRadius = 2.0f;

    // Update is called once per frame
    void Update()
    {
        // Move the player, clamping them to within the boundaries 
        // of the level.
        var movement = Input.GetAxis("Horizontal");
        movement *= (moveSpeed * Time.deltaTime);

        var newPosition = transform.position;
        newPosition.x += movement;
        newPosition.x = Mathf.Clamp(newPosition.x, minPosition, maxPosition);

        transform.position = newPosition;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CheckForNearbyNPC();
        }
    }

    public void CheckForNearbyNPC()
    {

        
    }
}

