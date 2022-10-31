using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashHandler : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        Level1GameManager.trashHovered = true;
    }
    
    private void OnTriggerExit2D(Collider2D col)
    {
        GetComponent<SpriteRenderer>().color = Color.white;
        Level1GameManager.trashHovered = false;
    }
}
