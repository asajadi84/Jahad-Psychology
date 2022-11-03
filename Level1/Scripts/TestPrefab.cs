using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPrefab : MonoBehaviour
{
    public bool isRight = false;
    public bool isWrongSelection = false;
    public bool isWrongRepetition = false;

    private Vector3 initialPosition;
    
    // AudioClips
    [SerializeField] private AudioClip rightChoice;
    [SerializeField] private AudioClip wrongChoice;

    private void Awake()
    {
        initialPosition = transform.position;
    }

    private void OnMouseDrag()
    {
        transform.position = new Vector3(
            Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
            Camera.main.ScreenToWorldPoint(Input.mousePosition).y,
            transform.position.z
        );
    }

    private void OnMouseUp()
    {
        if (Level1GameManager.basketHovered)
        {
            if (isRight)
            {
                AudioSource.PlayClipAtPoint(rightChoice, Camera.main.transform.position);
            }else if (isWrongSelection)
            {
                Level1GameManager.SelectionError++;
                Level1GameManager.noMistake = false;
                AudioSource.PlayClipAtPoint(wrongChoice, Camera.main.transform.position);
                if (!Level1GameManager.negativeScoreLocked)
                {
                    Level1GameManager.negativeScoreLocked = true;
                    Level1GameManager.negativeScore++;
                }
            }else if (isWrongRepetition)
            {
                Level1GameManager.RepetitionError++;
                Level1GameManager.noMistake = false;
                AudioSource.PlayClipAtPoint(wrongChoice, Camera.main.transform.position);
                if (!Level1GameManager.negativeScoreLocked)
                {
                    Level1GameManager.negativeScoreLocked = true;
                    Level1GameManager.negativeScore++;
                }
            }

            Destroy(gameObject);
        }
        else if (Level1GameManager.trashHovered)
        {
            if (isRight)
            {
                Level1GameManager.DeletionError++;
                Level1GameManager.noMistake = false;
                AudioSource.PlayClipAtPoint(wrongChoice, Camera.main.transform.position);
                if (!Level1GameManager.negativeScoreLocked)
                {
                    Level1GameManager.negativeScoreLocked = true;
                    Level1GameManager.negativeScore++;
                }
            }else if (isWrongSelection)
            {
                AudioSource.PlayClipAtPoint(rightChoice, Camera.main.transform.position);
            }else if (isWrongRepetition)
            {
                AudioSource.PlayClipAtPoint(rightChoice, Camera.main.transform.position);
            }

            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(DragBackToPlace(20));
        }
    }
    
    IEnumerator DragBackToPlace(int totalFrames)
    {
        Vector3 gameObjectCurrentPos = transform.position;
        int frameCount = 0;
        while (true)
        {
            float frameContinuum = (float)frameCount / totalFrames;
            Vector3 framePosition = Vector3.Lerp(
                gameObjectCurrentPos, initialPosition, frameContinuum);
            transform.position = framePosition;
            frameCount++;
            if (frameCount<totalFrames)
            {
                yield return new WaitForEndOfFrame();
            }
            else
            {
                yield break;
            }
        }
    }
}
