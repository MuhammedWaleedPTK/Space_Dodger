using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    private float screenHeight;
    
    public static bool isMovable = true;

    public GameObject engine;

    // Event to be invoked when the player dies
    public static UnityAction PlayerDeadAction;

    Animator anim;

    void Start()
    {
        // Initialize variables and references
        anim = GetComponent<Animator>();
        isMovable = true;
        screenHeight = Camera.main.orthographicSize;
       
    }

    void Update()
    {
        if (isMovable)
        {
            // Handle player input if the player is movable
            HandleInput();
        }
    }

    void HandleInput()
    {
        // Get vertical input based on the platform (mobile or non-mobile)
        float verticalInput = SimpleInput.GetAxis("Vertical");
        // Calculate the new position of the player based on the vertical input and speed
        Vector3 newPosition = transform.position + Vector3.up * verticalInput * speed * Time.deltaTime;

        // Clamp the player's y position to keep it within the screen bounds
        newPosition.y = Mathf.Clamp(newPosition.y, -screenHeight + 1, screenHeight - 1);

        // Update the player's position
        transform.position = newPosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player collides with an asteroid and is movable
        if (collision.transform.CompareTag("Asteroid") && isMovable)
        {
            // Start the coroutine for player death
            StartCoroutine(PlayerDied());
        }
    }

    IEnumerator PlayerDied()
    {
        // Set the player to not movable and trigger the death animation
        isMovable = false;
        anim.SetTrigger("isDead");

        // Deactivate the engine object (if it's meant to show engine flames)
        engine.SetActive(false);

        // Invoke the PlayerDeadAction event
        if (PlayerDeadAction != null)
        {
            PlayerDeadAction.Invoke();
        }

        // Wait for 1 second before finishing the death sequence
        yield return new WaitForSeconds(1);
    }
}
