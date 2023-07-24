using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    public float asteroidSpeed = 10f;

    // Update is called once per frame
    private void Update()
    {
        CheckIfOffScreen();
    }

    // Check if the asteroid is off the left side of the screen and deactivate it if true
    void CheckIfOffScreen()
    {
        // Calculate the leftmost position of the screen in world coordinates, adding 5 for extra buffer
        float leftmostPositionX = Camera.main.orthographicSize + 5;

        // Check if the asteroid's x position is beyond the left side of the screen
        if (transform.position.x < -leftmostPositionX)
        {
            // Deactivate the asteroid when it leaves the screen
            gameObject.SetActive(false);
        }
    }
}
