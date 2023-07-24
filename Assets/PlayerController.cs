using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f; 
    private float screenHeight;
    private bool isMobilePlatform;

    void Start()
    {
        
        screenHeight = Camera.main.orthographicSize;

        
        isMobilePlatform = Application.isMobilePlatform;
        
    }

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        float verticalInput = SimpleInput.GetAxis("Vertical");
       

        Vector3 newPosition = transform.position + Vector3.up * verticalInput * speed * Time.deltaTime;

        newPosition.y = Mathf.Clamp(newPosition.y, -screenHeight+1, screenHeight-1);

        transform.position = newPosition;
    }


}
