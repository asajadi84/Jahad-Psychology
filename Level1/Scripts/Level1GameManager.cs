using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Level1GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] groceries;
    [SerializeField] private Sprite[] grocerySprites;
    [SerializeField] private GameObject basket;
    [SerializeField] private GameObject trash;
    [SerializeField] private GameObject testPrefab;
    [SerializeField] private Text resultText;
    [SerializeField] private Text stageText;
    [SerializeField] private GameObject restartButton;
    [SerializeField] private GameObject clickToStartButton;
    
    // Flags
    public static bool tutorialMode = true;
    public static bool eligibleToPlay = true;
    public static int tempAct = 0;
    
    public static int negativeScore = 0;
    public static bool negativeScoreLocked = false;
    public static bool noMistake = true;
    
    public static bool basketHovered = false;
    public static bool trashHovered = false;
    
    // Level 1 Data
    // tutorialStages[stage][act][grocery]
    private GameObject[][][] tutorialStages;
    // actStages[stage][act][grocery]
    private GroceryStatus[][][] actStages = new GroceryStatus[][][]
    {
        //stage2
        new GroceryStatus[][]
        {
            //act1
            new GroceryStatus[]
            {
                new GroceryStatus(9, GrocerySituation.Right),
                new GroceryStatus(1, GrocerySituation.WrongSelection),
                new GroceryStatus(11, GrocerySituation.Right),
                new GroceryStatus(9, GrocerySituation.WrongRepetition)
            },
            //act2
            new GroceryStatus[]
            {
                new GroceryStatus(10, GrocerySituation.WrongSelection),
                new GroceryStatus(5, GrocerySituation.Right),
                new GroceryStatus(3, GrocerySituation.Right),
                new GroceryStatus(5, GrocerySituation.WrongRepetition)
            },
        },
        //stage3
        new GroceryStatus[][]
        {
            //act1
            new GroceryStatus[]
            {
                new GroceryStatus(9, GrocerySituation.Right),
                new GroceryStatus(14, GrocerySituation.WrongSelection),
                new GroceryStatus(10, GrocerySituation.Right),
                new GroceryStatus(9, GrocerySituation.WrongRepetition),
                new GroceryStatus(7, GrocerySituation.Right),
                new GroceryStatus(10, GrocerySituation.WrongRepetition)
            },
            //act2
            new GroceryStatus[]
            {
                new GroceryStatus(6, GrocerySituation.Right),
                new GroceryStatus(13, GrocerySituation.Right),
                new GroceryStatus(12, GrocerySituation.WrongSelection),
                new GroceryStatus(13, GrocerySituation.WrongRepetition),
                new GroceryStatus(8, GrocerySituation.Right),
                new GroceryStatus(3, GrocerySituation.WrongSelection)
            },
        },
        //stage4
        new GroceryStatus[][]
        {
            //act1
            new GroceryStatus[]
            {
                new GroceryStatus(0, GrocerySituation.Right),
                new GroceryStatus(1, GrocerySituation.Right),
                new GroceryStatus(11, GrocerySituation.Right),
                new GroceryStatus(8, GrocerySituation.WrongSelection),
                new GroceryStatus(0, GrocerySituation.WrongRepetition),
                new GroceryStatus(9, GrocerySituation.Right),
                new GroceryStatus(1, GrocerySituation.WrongRepetition),
                new GroceryStatus(12, GrocerySituation.WrongSelection)
            },
            //act2
            new GroceryStatus[]
            {
                new GroceryStatus(5, GrocerySituation.Right),
                new GroceryStatus(13, GrocerySituation.WrongSelection),
                new GroceryStatus(2, GrocerySituation.Right),
                new GroceryStatus(10, GrocerySituation.Right),
                new GroceryStatus(5, GrocerySituation.WrongRepetition),
                new GroceryStatus(3, GrocerySituation.WrongSelection),
                new GroceryStatus(14, GrocerySituation.Right),
                new GroceryStatus(2, GrocerySituation.WrongRepetition)
            },
        },
        //stage5
        new GroceryStatus[][]
        {
            //act1
            new GroceryStatus[]
            {
                new GroceryStatus(14, GrocerySituation.Right),
                new GroceryStatus(8, GrocerySituation.WrongSelection),
                new GroceryStatus(12, GrocerySituation.Right),
                new GroceryStatus(7, GrocerySituation.Right),
                new GroceryStatus(14, GrocerySituation.WrongRepetition),
                new GroceryStatus(9, GrocerySituation.Right),
                new GroceryStatus(7, GrocerySituation.WrongRepetition),
                new GroceryStatus(3, GrocerySituation.WrongSelection),
                new GroceryStatus(10, GrocerySituation.Right)
            },
            //act2
            new GroceryStatus[]
            {
                new GroceryStatus(11, GrocerySituation.Right),
                new GroceryStatus(5, GrocerySituation.WrongSelection),
                new GroceryStatus(0, GrocerySituation.Right),
                new GroceryStatus(6, GrocerySituation.Right),
                new GroceryStatus(3, GrocerySituation.WrongSelection),
                new GroceryStatus(0, GrocerySituation.WrongRepetition),
                new GroceryStatus(1, GrocerySituation.Right),
                new GroceryStatus(6, GrocerySituation.WrongRepetition),
                new GroceryStatus(4, GrocerySituation.Right)
            },
        },
        //stage6
        new GroceryStatus[][]
        {
            //act1
            new GroceryStatus[]
            {
                new GroceryStatus(9, GrocerySituation.WrongSelection),
                new GroceryStatus(14, GrocerySituation.Right),
                new GroceryStatus(11, GrocerySituation.WrongSelection),
                new GroceryStatus(6, GrocerySituation.Right),
                new GroceryStatus(13, GrocerySituation.Right),
                new GroceryStatus(4, GrocerySituation.WrongSelection),
                new GroceryStatus(6, GrocerySituation.WrongRepetition),
                new GroceryStatus(10, GrocerySituation.Right),
                new GroceryStatus(14, GrocerySituation.WrongRepetition),
                new GroceryStatus(2, GrocerySituation.Right),
                new GroceryStatus(3, GrocerySituation.Right)
            },
            //act2
            new GroceryStatus[]
            {
                new GroceryStatus(2, GrocerySituation.WrongSelection),
                new GroceryStatus(11, GrocerySituation.Right),
                new GroceryStatus(5, GrocerySituation.Right),
                new GroceryStatus(14, GrocerySituation.WrongSelection),
                new GroceryStatus(8, GrocerySituation.Right),
                new GroceryStatus(0, GrocerySituation.Right),
                new GroceryStatus(5, GrocerySituation.WrongRepetition),
                new GroceryStatus(1, GrocerySituation.Right),
                new GroceryStatus(0, GrocerySituation.WrongRepetition),
                new GroceryStatus(12, GrocerySituation.Right),
                new GroceryStatus(11, GrocerySituation.WrongRepetition)
            },
        },
        //stage7
        new GroceryStatus[][]
        {
            //act1
            new GroceryStatus[]
            {
                new GroceryStatus(4, GrocerySituation.Right),
                new GroceryStatus(14, GrocerySituation.WrongSelection),
                new GroceryStatus(3, GrocerySituation.Right),
                new GroceryStatus(4, GrocerySituation.WrongRepetition),
                new GroceryStatus(0, GrocerySituation.Right),
                new GroceryStatus(9, GrocerySituation.WrongSelection),
                new GroceryStatus(10, GrocerySituation.Right),
                new GroceryStatus(3, GrocerySituation.WrongRepetition),
                new GroceryStatus(2, GrocerySituation.Right),
                new GroceryStatus(8, GrocerySituation.Right),
                new GroceryStatus(5, GrocerySituation.WrongSelection),
                new GroceryStatus(6, GrocerySituation.Right)
            },
            //act2
            new GroceryStatus[]
            {
                new GroceryStatus(13, GrocerySituation.Right),
                new GroceryStatus(1, GrocerySituation.Right),
                new GroceryStatus(10, GrocerySituation.WrongSelection),
                new GroceryStatus(7, GrocerySituation.Right),
                new GroceryStatus(11, GrocerySituation.Right),
                new GroceryStatus(9, GrocerySituation.Right),
                new GroceryStatus(1, GrocerySituation.WrongRepetition),
                new GroceryStatus(13, GrocerySituation.WrongRepetition),
                new GroceryStatus(12, GrocerySituation.Right),
                new GroceryStatus(2, GrocerySituation.WrongSelection),
                new GroceryStatus(14, GrocerySituation.Right),
                new GroceryStatus(9, GrocerySituation.WrongRepetition)
            },
        },
        //stage8
        new GroceryStatus[][]
        {
            //act1
            new GroceryStatus[]
            {
                new GroceryStatus(11, GrocerySituation.WrongSelection),
                new GroceryStatus(6, GrocerySituation.Right),
                new GroceryStatus(10, GrocerySituation.Right),
                new GroceryStatus(14, GrocerySituation.WrongSelection),
                new GroceryStatus(3, GrocerySituation.Right),
                new GroceryStatus(6, GrocerySituation.WrongRepetition),
                new GroceryStatus(2, GrocerySituation.Right),
                new GroceryStatus(7, GrocerySituation.Right),
                new GroceryStatus(10, GrocerySituation.WrongRepetition),
                new GroceryStatus(12, GrocerySituation.Right),
                new GroceryStatus(9, GrocerySituation.Right),
                new GroceryStatus(2, GrocerySituation.WrongRepetition),
                new GroceryStatus(1, GrocerySituation.Right)
            },
            //act2
            new GroceryStatus[]
            {
                new GroceryStatus(1, GrocerySituation.Right),
                new GroceryStatus(14, GrocerySituation.Right),
                new GroceryStatus(11, GrocerySituation.WrongSelection),
                new GroceryStatus(2, GrocerySituation.Right),
                new GroceryStatus(14, GrocerySituation.WrongRepetition),
                new GroceryStatus(9, GrocerySituation.WrongSelection),
                new GroceryStatus(0, GrocerySituation.Right),
                new GroceryStatus(12, GrocerySituation.Right),
                new GroceryStatus(5, GrocerySituation.Right),
                new GroceryStatus(1, GrocerySituation.WrongRepetition),
                new GroceryStatus(0, GrocerySituation.Right),
                new GroceryStatus(10, GrocerySituation.WrongSelection),
                new GroceryStatus(8, GrocerySituation.Right)
            },
        },
        //stage9
        new GroceryStatus[][]
        {
            //act1
            new GroceryStatus[]
            {
                new GroceryStatus(9, GrocerySituation.WrongSelection),
                new GroceryStatus(0, GrocerySituation.Right),
                new GroceryStatus(2, GrocerySituation.Right),
                new GroceryStatus(10, GrocerySituation.Right),
                new GroceryStatus(0, GrocerySituation.WrongRepetition),
                new GroceryStatus(14, GrocerySituation.WrongSelection),
                new GroceryStatus(11, GrocerySituation.Right),
                new GroceryStatus(8, GrocerySituation.Right),
                new GroceryStatus(12, GrocerySituation.WrongSelection),
                new GroceryStatus(13, GrocerySituation.Right),
                new GroceryStatus(6, GrocerySituation.Right),
                new GroceryStatus(4, GrocerySituation.Right),
                new GroceryStatus(2, GrocerySituation.WrongRepetition),
                new GroceryStatus(3, GrocerySituation.Right)
            },
            //act2
            new GroceryStatus[]
            {
                new GroceryStatus(7, GrocerySituation.Right),
                new GroceryStatus(6, GrocerySituation.Right),
                new GroceryStatus(2, GrocerySituation.Right),
                new GroceryStatus(6, GrocerySituation.WrongRepetition),
                new GroceryStatus(0, GrocerySituation.WrongSelection),
                new GroceryStatus(12, GrocerySituation.Right),
                new GroceryStatus(5, GrocerySituation.Right),
                new GroceryStatus(8, GrocerySituation.WrongSelection),
                new GroceryStatus(10, GrocerySituation.Right),
                new GroceryStatus(1, GrocerySituation.Right),
                new GroceryStatus(7, GrocerySituation.WrongRepetition),
                new GroceryStatus(3, GrocerySituation.Right),
                new GroceryStatus(9, GrocerySituation.Right),
                new GroceryStatus(12, GrocerySituation.WrongRepetition)
            },
        }
        
    };

    // Scores
    public static int FarakhnaActualScore = 0;
    public static int EffortScore = 0;
    public static int DeletionError = 0;
    public static int SelectionError = 0;
    public static int RepetitionError = 0;

    private void Awake()
    {
        tutorialStages = new GameObject[][][]
        {
            //stage2
            new GameObject[][]
            {
                new GameObject[]{groceries[11], groceries[9]},
                new GameObject[]{groceries[3], groceries[5]}
            },
            //stage3
            new GameObject[][]
            {
                new GameObject[]{groceries[7], groceries[9], groceries[10]},
                new GameObject[]{groceries[13], groceries[6], groceries[8]}
            },
            //stage4
            new GameObject[][]
            {
                new GameObject[]{groceries[0], groceries[11], groceries[1], groceries[9]},
                new GameObject[]{groceries[10], groceries[14], groceries[5], groceries[2]}
            },
            //stage5
            new GameObject[][]
            {
                new GameObject[]{groceries[7], groceries[12], groceries[14], groceries[9], groceries[10]},
                new GameObject[]{groceries[1], groceries[11], groceries[4], groceries[0], groceries[6]}
            },
            //stage6
            new GameObject[][]
            {
                new GameObject[]{groceries[13], groceries[6], groceries[14], groceries[2], groceries[3], groceries[10]},
                new GameObject[]{groceries[8], groceries[1], groceries[0], groceries[5], groceries[12], groceries[11]}
            },
            //stage7
            new GameObject[][]
            {
                new GameObject[]{groceries[3], groceries[10], groceries[6], groceries[4], groceries[0], groceries[2], groceries[8]},
                new GameObject[]{groceries[11], groceries[7], groceries[12], groceries[13], groceries[1], groceries[9], groceries[14]}
            },
            //stage8
            new GameObject[][]
            {
                new GameObject[]{groceries[12], groceries[9], groceries[1], groceries[7], groceries[6], groceries[10], groceries[3], groceries[2]},
                new GameObject[]{groceries[8], groceries[14], groceries[1], groceries[0], groceries[5], groceries[2], groceries[0], groceries[12]}
            },
            //stage9
            new GameObject[][]
            {
                new GameObject[]{groceries[8], groceries[3], groceries[10], groceries[11], groceries[4], groceries[0], groceries[2], groceries[13], groceries[6]},
                new GameObject[]{groceries[2], groceries[12], groceries[5], groceries[1], groceries[7], groceries[6], groceries[10], groceries[3], groceries[9]}
            },
        };
    }

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(StartLevel1());
    }

    public void StartLevel1Wrapper()
    {
        clickToStartButton.SetActive(false);
        basket.SetActive(true);
        stageText.gameObject.SetActive(true);
        StartCoroutine(StartLevel1());
    }

    IEnumerator StartLevel1()
    {
        yield return new WaitForSeconds(1.0f);
        NextMove();
    }

    void NextMove()
    {
        if (eligibleToPlay && FarakhnaActualScore < 8)
        {
            if (tutorialMode)
            {
                StartCoroutine(DisplaySelectedGroceries(tutorialStages[FarakhnaActualScore][tempAct]));
            }
            else
            {
                StartCoroutine(GenerateTest(actStages[FarakhnaActualScore][tempAct]));
            }
        }
        else
        {
            basket.SetActive(false);
            restartButton.SetActive(true);
            stageText.gameObject.SetActive(false);
            resultText.text = "Farakhna Score: " + (FarakhnaActualScore + 1) + "\n"
                              + "Effort Score: " + EffortScore + "\n"
                              + "Deletion Error: " + DeletionError + "\n"
                              + "Selection Error: " + SelectionError + "\n"
                              + "Repetition Error: " + RepetitionError;
        }
    }

    IEnumerator DisplaySelectedGroceries(GameObject[] selectedGroceries)
    {
        ShowAllGroceries();
        yield return new WaitForSeconds(1.0f);
        foreach (GameObject tempGameObject in selectedGroceries)
        {
            tempGameObject.AddComponent<GroceryAnimation>();
            tempGameObject.GetComponent<SpriteRenderer>().sortingOrder = 2;
            yield return new WaitForSeconds(2.0f);
            StartCoroutine(TranslateToBasket(tempGameObject, 1));
            yield return new WaitForSeconds(1.0f);
        }

        yield return new WaitForSeconds(2.0f);
        HideAllGroceries();
        tutorialMode = false;
        NextMove();
        yield break;
    }

    IEnumerator TranslateToBasket(GameObject transformGameObject, int totalSeconds)
    {
        int totalFrames = Mathf.RoundToInt(totalSeconds/Time.deltaTime);
        Vector3 tgoCurrentPosition = transformGameObject.transform.position;
        int frameCount = 0;
        Destroy(transformGameObject.GetComponent<GroceryAnimation>());
        while (true)
        {
            float frameContinuum = (float)frameCount / totalFrames;
            Vector3 framePosition = Vector3.Lerp(
                tgoCurrentPosition, basket.transform.position, frameContinuum);
            transformGameObject.transform.position = framePosition;
            frameCount++;
            if (frameCount<totalFrames)
            {
                yield return new WaitForEndOfFrame();
            }
            else
            {
                transformGameObject.SetActive(false);
                transformGameObject.transform.position = tgoCurrentPosition;
                transformGameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
                transformGameObject.transform.localScale = new Vector3(
                    0.3f, 0.3f, 1.0f);
                yield break;
            }
        }
    }

    void HideAllGroceries()
    {
        foreach (GameObject grocery in groceries)
        {
            grocery.SetActive(false);
        }
    }
    
    void ShowAllGroceries()
    {
        foreach (GameObject grocery in groceries)
        {
            grocery.SetActive(true);
        }
    }

    void GenerateTestGrocery(GroceryStatus groceryStatus)
    {
        GameObject testInstance = Instantiate(testPrefab);
        testInstance.GetComponent<SpriteRenderer>().sprite = grocerySprites[groceryStatus.grocerySpriteId];
        if (groceryStatus.Situation == GrocerySituation.Right)
        {
            testInstance.GetComponent<TestPrefab>().isRight = true;
        }else if (groceryStatus.Situation == GrocerySituation.WrongSelection)
        {
            testInstance.GetComponent<TestPrefab>().isWrongSelection = true;
        }else if (groceryStatus.Situation == GrocerySituation.WrongRepetition)
        {
            testInstance.GetComponent<TestPrefab>().isWrongRepetition = true;
        }
    }

    IEnumerator GenerateTest(GroceryStatus[] testItems)
    {
        trash.SetActive(true);
        int testItemId = 0;
        while (true)
        {
            if (testItemId < testItems.Length)
            {
                if (GameObject.Find("Test Prefab(Clone)"))
                {
                    yield return new WaitForEndOfFrame();
                }
                else
                {
                    GenerateTestGrocery(testItems[testItemId]);
                    testItemId++;
                    yield return new WaitForEndOfFrame();
                }
            }
            else
            {
                if (GameObject.Find("Test Prefab(Clone)"))
                {
                    yield return new WaitForEndOfFrame();
                }
                else
                {
                    trash.SetActive(false);
                    tutorialMode = true;
                    yield return new WaitForSeconds(2.0f);
                    if (tempAct == 0)
                    {
                        tempAct++;
                    }
                    else
                    {
                        if (negativeScore > 1)
                        {
                            eligibleToPlay = false;
                        }
                        else
                        {
                            tempAct = 0;
                            negativeScore = 0;
                            FarakhnaActualScore++;
                            stageText.text = "Farakhna: " + (FarakhnaActualScore + 2);
                        }
                    }

                    negativeScoreLocked = false;
                    if (noMistake)
                    {
                        EffortScore++;
                    }
                    else
                    {
                        noMistake = true;
                    }

                    NextMove();
                    yield break;
                }

            }
        }
    }
}
