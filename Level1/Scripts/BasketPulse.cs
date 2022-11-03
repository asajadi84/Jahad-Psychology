using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketPulse : MonoBehaviour
{
    // AudioClips
    [SerializeField] private AudioClip rightChoice;
    [SerializeField] private AudioClip wrongChoice;

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
    
    private void OnMouseUp()
    {
        GameObject currentTestPrefab = GameObject.Find("Test Prefab(Clone)");
        if (currentTestPrefab)
        {
            StartCoroutine(TranslateToBasketAuto(currentTestPrefab, 0.3f));
        }
    }
    
    IEnumerator TranslateToBasketAuto(GameObject transformGameObject, float totalSeconds)
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
                    AudioSource.PlayClipAtPoint(rightChoice, Camera.main.transform.position);
                }else if (transformGameObject.GetComponent<TestPrefab>().isWrongSelection)
                {
                    Level1GameManager.SelectionError++;
                    Level1GameManager.noMistake = false;
                    AudioSource.PlayClipAtPoint(wrongChoice, Camera.main.transform.position);
                    if (!Level1GameManager.negativeScoreLocked)
                    {
                        Level1GameManager.negativeScoreLocked = true;
                        Level1GameManager.negativeScore++;
                    }
                }else if (transformGameObject.GetComponent<TestPrefab>().isWrongRepetition)
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

                Destroy(transformGameObject);
                yield break;
            }
        }
    }
}
