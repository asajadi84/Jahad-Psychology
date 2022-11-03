using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashHandler : MonoBehaviour
{
    // AudioClips
    [SerializeField] private AudioClip rightChoice;
    [SerializeField] private AudioClip wrongChoice;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        Level1GameManager.trashHovered = true;
        col.gameObject.GetComponent<Animator>().SetBool("EnteredTrash", true);
    }
    
    private void OnTriggerExit2D(Collider2D col)
    {
        GetComponent<SpriteRenderer>().color = Color.white;
        Level1GameManager.trashHovered = false;
        col.gameObject.GetComponent<Animator>().SetBool("EnteredTrash", false);
    }

    private void OnMouseUp()
    {
        GameObject currentTestPrefab = GameObject.Find("Test Prefab(Clone)");
        if (currentTestPrefab)
        {
            StartCoroutine(TranslateToTrash(currentTestPrefab, 0.3f));
        }
    }
    
    IEnumerator TranslateToTrash(GameObject transformGameObject, float totalSeconds)
    {
        int totalFrames = Mathf.RoundToInt(totalSeconds/Time.deltaTime);
        Vector3 tgoCurrentPosition = transformGameObject.transform.position;
        int frameCount = 0;
        while (true)
        {
            float frameContinuum = (float)frameCount / totalFrames;
            Vector3 framePosition = Vector3.Lerp(
                tgoCurrentPosition, gameObject.transform.position, frameContinuum);
            transformGameObject.transform.position = framePosition;
            frameCount++;
            if (frameCount<totalFrames)
            {
                yield return new WaitForEndOfFrame();
            }
            else
            {
                if (transformGameObject.GetComponent<TestPrefab>().isRight)
                {
                    Level1GameManager.DeletionError++;
                    Level1GameManager.noMistake = false;
                    AudioSource.PlayClipAtPoint(wrongChoice, Camera.main.transform.position);
                    if (!Level1GameManager.negativeScoreLocked)
                    {
                        Level1GameManager.negativeScoreLocked = true;
                        Level1GameManager.negativeScore++;
                    }
                }else if (transformGameObject.GetComponent<TestPrefab>().isWrongSelection)
                {
                    AudioSource.PlayClipAtPoint(rightChoice, Camera.main.transform.position);
                }else if (transformGameObject.GetComponent<TestPrefab>().isWrongRepetition)
                {
                    AudioSource.PlayClipAtPoint(rightChoice, Camera.main.transform.position);
                }

                Destroy(transformGameObject);
                yield break;
            }
        }
    }
}
