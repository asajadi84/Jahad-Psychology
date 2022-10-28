using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroceryAnimation : MonoBehaviour
{
    [SerializeField] private float speed = 3.0f;

    // Update is called once per frame
    void Update()
    {
        float frameValue = 0.3f + (Mathf.Abs(Mathf.Cos(Time.time * speed)/10));
        transform.localScale = new Vector3(frameValue, frameValue, transform.localScale.z);
    }
}
