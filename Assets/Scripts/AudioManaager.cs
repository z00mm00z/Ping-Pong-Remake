using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManaager : MonoBehaviour
{
    /*
    To-Do If you want to update:

    
    Win Audio
    Counter Tick Audio
    Paddle Moving Audio? (Possibly sounds like large piece of stone sliding)
    
    
    */


    public AudioSource audioSource;
    public AudioSource audioSourceKB;

    public AudioClip[] keyClicks;
    public AudioClip[] ballHits;
    public AudioClip menuHighlight;
    public AudioClip menuObjectHit;
    public AudioClip menuButtonSelect;
    public AudioClip ballDeath;

    public void playRandomKey() {
        audioSourceKB.PlayOneShot(keyClicks[Random.Range(0, keyClicks.Length)]);
    }

    public void playButtonHighlight() {
        audioSource.PlayOneShot(menuHighlight);
    }

    public void playBallHit() {
        audioSource.PlayOneShot(ballHits[Random.Range(0, ballHits.Length)]);
    }

    public void playMenuObectHit() {
        audioSource.PlayOneShot(menuObjectHit);
    }

    public void playMenuSelect() {
        audioSource.PlayOneShot(menuButtonSelect);
    }

    public void playBallDestroy() {
        audioSource.PlayOneShot(ballDeath);
    }
}
