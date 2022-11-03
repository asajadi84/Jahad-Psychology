using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroceryAnimation : MonoBehaviour
{
    [SerializeField] private float speed = 3.0f;
    private float componentLifeSpan = 0.0f;
    private float initialScale;

    private void Awake()
    {
        initialScale = gameObject.transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        float frameValue = initialScale + (Mathf.Abs(Mathf.Sin(componentLifeSpan * speed)/10));
        transform.localScale = new Vector3(frameValue, frameValue, transform.localScale.z);
        componentLifeSpan += Time.deltaTime;
    }
}
