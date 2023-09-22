using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BallSituation { 
    Green, Yellow
}

public class BallPrefab : MonoBehaviour
{
    //flags
    private bool pressedAlready = false;

    //properties
    private float timePassed = 0.0f;
    private float responseTime = 0.0f;
    private int answerStatus = 0; //0 = unanswered, 1 = right, 2 = wrong
    private BallSituation userInput;
    private GameObject greenBallStatic;
    private GameObject yellowBallStatic;

    //assests
    [SerializeField] private Sprite greenBallSprite;
    [SerializeField] private Sprite yellowBallSprite;
    [SerializeField] private AudioClip greenTTS;
    [SerializeField] private AudioClip yellowTTS;

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

        if (isOral) {
            if (question == BallSituation.Green) {
                AudioSource.PlayClipAtPoint(greenTTS, Vector3.zero);
            } else {
                AudioSource.PlayClipAtPoint(yellowTTS, Vector3.zero);
            }
        } else {
            if (question == BallSituation.Green) {
                gameObject.GetComponent<SpriteRenderer>().sprite = greenBallSprite;
            } else {
                gameObject.GetComponent<SpriteRenderer>().sprite = yellowBallSprite;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed > lifeSpan) {
            Destroy(gameObject);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (!pressedAlready)
            {
                pressedAlready = true;
                userInput = BallSituation.Green;
                responseTime = timePassed;
                greenBallStatic.GetComponent<Animator>().SetBool("pulseMe", true);
                if (answer == BallSituation.Green) {
                    answerStatus = 1;
                } else {
                    answerStatus = 2;

                    if ((GameObject.Find("GameManager").GetComponent<Spinoff1GameManager>().step == 3 && !isOral) || (GameObject.Find("GameManager").GetComponent<Spinoff1GameManager>().step == 7 && isOral)) {
                        GameObject.Find("GameManager").GetComponent<Spinoff1GameManager>().mistakeTolerance++;
                    }

                    if (GameObject.Find("GameManager").GetComponent<Spinoff1GameManager>().mistakeTolerance > 12) {
                        GameObject.Find("GameManager").GetComponent<Spinoff1GameManager>().mistakeTolerance = 0;
                        if (GameObject.Find("GameManager").GetComponent<Spinoff1GameManager>().step == 3) {
                            GameObject.Find("GameManager").GetComponent<Spinoff1GameManager>().stage1tooManyErrors = true;
                        } else {
                            GameObject.Find("GameManager").GetComponent<Spinoff1GameManager>().stage2tooManyErrors = true;
                        }
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            if (!pressedAlready) {
                pressedAlready = true;
                userInput = BallSituation.Yellow;
                responseTime = timePassed;
                yellowBallStatic.GetComponent<Animator>().SetBool("pulseMe", true);
                if (answer == BallSituation.Yellow) {
                    answerStatus = 1;
                } else {
                    answerStatus = 2;

                    if ((GameObject.Find("GameManager").GetComponent<Spinoff1GameManager>().step == 3 && !isOral) || (GameObject.Find("GameManager").GetComponent<Spinoff1GameManager>().step == 7 && isOral)) {
                        GameObject.Find("GameManager").GetComponent<Spinoff1GameManager>().mistakeTolerance++;
                    }

                    if (GameObject.Find("GameManager").GetComponent<Spinoff1GameManager>().mistakeTolerance > 12) {
                        GameObject.Find("GameManager").GetComponent<Spinoff1GameManager>().mistakeTolerance = 0;
                        if (GameObject.Find("GameManager").GetComponent<Spinoff1GameManager>().step == 3) {
                            GameObject.Find("GameManager").GetComponent<Spinoff1GameManager>().stage1tooManyErrors = true;
                        } else {
                            GameObject.Find("GameManager").GetComponent<Spinoff1GameManager>().stage2tooManyErrors = true;
                        }
                    }
                }
            }
        }
    }

    private void OnDisable()
    {
        int val1;
        float val2;

        if (pressedAlready) {
            if (userInput == BallSituation.Green) {
                greenBallStatic.GetComponent<Animator>().SetBool("pulseMe", false);
            } else {
                yellowBallStatic.GetComponent<Animator>().SetBool("pulseMe", false);
            }

            val1 = answerStatus;
            val2 = responseTime;
        } else {
            val1 = answerStatus;
            val2 = lifeSpan;

            if ((GameObject.Find("GameManager").GetComponent<Spinoff1GameManager>().step == 3 && !isOral) || (GameObject.Find("GameManager").GetComponent<Spinoff1GameManager>().step == 7 && isOral)) {
                GameObject.Find("GameManager").GetComponent<Spinoff1GameManager>().mistakeTolerance++;
            }

            if (GameObject.Find("GameManager").GetComponent<Spinoff1GameManager>().mistakeTolerance > 12)
            {
                GameObject.Find("GameManager").GetComponent<Spinoff1GameManager>().mistakeTolerance = 0;
                if (GameObject.Find("GameManager").GetComponent<Spinoff1GameManager>().step == 3)
                {
                    GameObject.Find("GameManager").GetComponent<Spinoff1GameManager>().stage1tooManyErrors = true;
                }
                else
                {
                    GameObject.Find("GameManager").GetComponent<Spinoff1GameManager>().stage2tooManyErrors = true;
                }
            }
        }

        if (isOral)
        {
                    if (userInput == BallSituation.Green)
                    {
                        val2 -= greenTTS.length;
                    } else if (userInput == BallSituation.Yellow)
                    {
                        val2 -= yellowTTS.length;
                    } else
                    {
                        val2 -= Spinoff1GameManager.globalSpareTime;
                    }
        }

        val2 = Mathf.Clamp(val2, 0.0f, lifeSpan);

        if (GameObject.Find("GameManager").GetComponent<Spinoff1GameManager>().step == 3) {
            GameObject.Find("GameManager").GetComponent<Spinoff1GameManager>().answers1.Add(new BallAnswers(val1, val2));
        } else {
            GameObject.Find("GameManager").GetComponent<Spinoff1GameManager>().answers2.Add(new BallAnswers(val1, val2));
        }
    }
}
