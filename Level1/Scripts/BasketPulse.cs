using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketPulse : MonoBehaviour
{
    // AudioClips
    [SerializeField] private AudioClip rightChoice;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (Level1GameManager.tutorialMode)
        {
            GetComponent<Animator>().SetBool("SomethingEntered", true);
            AudioSource.PlayClipAtPoint(rightChoice, Camera.main.transform.position);
        }
        else
        {
            GetComponent<Animator>().SetBool("SomethingHovered", true);
            Level1GameManager.basketHovered = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D col)
    {
        if (Level1GameManager.tutorialMode)
        {
            GetComponent<Animator>().SetBool("SomethingEntered", false);
        }
        else
        {
            GetComponent<Animator>().SetBool("SomethingHovered", false);
            Level1GameManager.basketHovered = false;
        }
    }
}
