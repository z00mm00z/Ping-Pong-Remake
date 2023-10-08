using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCollision : MonoBehaviour
{

    public GameObject audioManager;

    private void OnCollisionEnter2D(Collision2D other) {
        audioManager.GetComponent<AudioManaager>().playMenuObectHit();
    }
}
