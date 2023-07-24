using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    public float asteroidSpeed=10f;
    
    private void Update()
    {
        CheckIfOffScreen();
    }
    void CheckIfOffScreen()
    {
        float leftmostPositionX = Camera.main.orthographicSize+5;

        if (transform.position.x < -leftmostPositionX)
        {
            gameObject.SetActive(false);
        }
    }

}
