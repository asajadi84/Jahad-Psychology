using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketPulse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        StartCoroutine(SomethingEnteredAnimation());
    }

    IEnumerator SomethingEnteredAnimation()
    {
        GetComponent<Animator>().SetBool("SomethingEntered", true);
        yield return new WaitForSeconds(0.5f);
        GetComponent<Animator>().SetBool("SomethingEntered", false);
    }
}
