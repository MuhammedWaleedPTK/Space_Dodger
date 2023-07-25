using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource bgm;
    public AudioSource playerDeathSound;
    private void OnEnable()
    {
        PlayerController.PlayerDeadAction += PlayerDied;
    }
    void Start()
    {
        
    }
    void PlayerDied()
    {
        playerDeathSound.Play();
    }
    private void OnDisable()
    {
        PlayerController.PlayerDeadAction -= PlayerDied;
    }


}
