using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f; 
    private float screenHeight;
    private bool isMobilePlatform;
    public static bool isMovable = true;

    public GameObject engine;
    public static Action PlayerDeadAction;

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        isMovable = true;

        screenHeight = Camera.main.orthographicSize;

        
        isMobilePlatform = Application.isMobilePlatform;
        
    }

    void Update()
    {
        if(isMovable)
        {
            HandleInput();
        }
        
        
    }

    void HandleInput()
    {
        float verticalInput = SimpleInput.GetAxis("Vertical");
       

        Vector3 newPosition = transform.position + Vector3.up * verticalInput * speed * Time.deltaTime;

        newPosition.y = Mathf.Clamp(newPosition.y, -screenHeight+1, screenHeight-1);

        transform.position = newPosition;
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Asteroid")&&isMovable)
        {
            StartCoroutine(PlayerDied());
        }
    }
    IEnumerator PlayerDied()
    {
        isMovable = false;
        anim.SetTrigger("isDead");
        engine.SetActive(false);
        PlayerDeadAction();
        yield return new WaitForSeconds(1);
    }


}
