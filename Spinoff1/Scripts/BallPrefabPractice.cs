using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPrefabPractice : MonoBehaviour
{
    //flags
    private bool pressedAlready = false;

    //properties
    private float timePassed = 0.0f;
    private BallSituation userInput;
    private GameObject greenBallStatic;
    private GameObject yellowBallStatic;

    //assests
    [SerializeField] private Sprite greenBallSprite;
    [SerializeField] private Sprite yellowBallSprite;
    [SerializeField] private AudioClip greenTTS;
    [SerializeField] private AudioClip yellowTTS;
    [SerializeField] private AudioClip faWrong;
    [SerializeField] private AudioClip faMiss;

    //tweaks
    public bool isOral;
    public BallSituation question;
    public BallSituation answer;
    public float lifeSpan;

    // Start is called before the first frame update
    void Start()
    {
        greenBallStatic = GameObject.Find("green-ball");
        yellowBallStatic = GameObject.Find("yellow-ball");

        Debug.Log("green:" + greenTTS.length);
        Debug.Log("yellow:" + yellowTTS.length);

        if (isOral)
        {

            if (question == BallSituation.Green)
            {
                AudioSource.PlayClipAtPoint(greenTTS, Vector3.zero);
            }
            else
            {
                AudioSource.PlayClipAtPoint(yellowTTS, Vector3.zero);
            }
        }
        else
        {
            if (question == BallSituation.Green)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = greenBallSprite;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = yellowBallSprite;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed > lifeSpan)
        {
            Destroy(gameObject);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (!pressedAlready)
            {
                pressedAlready = true;
                userInput = BallSituation.Green;
                greenBallStatic.GetComponent<Animator>().SetBool("pulseMe", true);
                if (answer != BallSituation.Green)
                {
                    AudioSource.PlayClipAtPoint(faWrong, Vector3.zero);
                    GameObject.Find("GameManager").GetComponent<Spinoff1GameManager>().madeAMistake = true;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (!pressedAlready)
            {
                pressedAlready = true;
                userInput = BallSituation.Yellow;
                yellowBallStatic.GetComponent<Animator>().SetBool("pulseMe", true);
                if (answer != BallSituation.Yellow)
                {
                    AudioSource.PlayClipAtPoint(faWrong, Vector3.zero);
                    GameObject.Find("GameManager").GetComponent<Spinoff1GameManager>().madeAMistake = true;
                }
            }
        }
    }

    private void OnDisable()
    {

        if (pressedAlready)
        {
            if (userInput == BallSituation.Green)
            {
                greenBallStatic.GetComponent<Animator>().SetBool("pulseMe", false);
            }
            else
            {
                yellowBallStatic.GetComponent<Animator>().SetBool("pulseMe", false);
            }
        }
        else
        {
            AudioSource.PlayClipAtPoint(faMiss, Vector3.zero);
            GameObject.Find("GameManager").GetComponent<Spinoff1GameManager>().madeAMistake = true;
        }
    }
}
