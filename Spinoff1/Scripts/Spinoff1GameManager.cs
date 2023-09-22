using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Windows;
using Input = UnityEngine.Input;

public class Spinoff1GameManager : MonoBehaviour
{

    //assets
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private GameObject ballPrefabPractice;
    [SerializeField] private GameObject greenBallStatic;
    [SerializeField] private GameObject yellowBallStatic;
    [SerializeField] private Sprite greenBallSprite;
    [SerializeField] private Sprite yellowBallSprite;
    [SerializeField] private Text counter;
    [SerializeField] private Text rightScore;
    [SerializeField] private Text leftScore;
    [SerializeField] private GameObject resultTable;

    [SerializeField] private AudioClip faIntro1;
    [SerializeField] private AudioClip faIntro2;
    [SerializeField] private AudioClip faIntro3;
    [SerializeField] private AudioClip faIntro4;
    [SerializeField] private AudioClip faIntro5;
    [SerializeField] private AudioClip faIntro6;
    [SerializeField] private AudioClip faIntro7;
    [SerializeField] private AudioClip faIntro8;
    [SerializeField] private AudioClip faIntro9;
    [SerializeField] private AudioClip faIntro10;
    [SerializeField] private AudioClip faSpace;
    [SerializeField] private AudioClip faFastaccurate;
    [SerializeField] private AudioClip faS;
    [SerializeField] private AudioClip faEnd;

    //datasets
    private BallPrefabStatus[] practice1;
    private BallPrefabStatus[] practice2;
    private BallPrefabStatus[] practice3;
    private BallPrefabStatus[] practice4;
    private BallPrefabStatus[] practice5;
    private BallPrefabStatus[] practice6;

    private BallPrefabStatus[] stage1;
    private BallPrefabStatus[] stage2;

    public List<BallAnswers> answers1;
    public List<BallAnswers> answers2;

    [SerializeField] private InputField nameInputField;
    [SerializeField] private InputField ageInputField;
    [SerializeField] private InputField gradeInputField;
    [SerializeField] private GameObject startForm;

    //flags
    public int step = 0;
    private bool allowToPressSpace = false;
    public int stageCounter = 0;
    private bool isPaused = false;
    private bool formCompleted = false;
    public static float globalSpareTime = 0.3f;

    public int mistakeTolerance = 0;
    public bool stage1tooManyErrors = false;
    public bool stage2tooManyErrors = false;
    public bool madeAMistake = false;

    private void Awake()
    {
        answers1 = new List<BallAnswers>();
        answers2 = new List<BallAnswers>();

        practice1 = new BallPrefabStatus[] {
            new BallPrefabStatus(false, BallSituation.Yellow, BallSituation.Yellow, 1.85f + globalSpareTime, 1.5f),
            new BallPrefabStatus(false, BallSituation.Green, BallSituation.Green, 1.35f + globalSpareTime, 1.5f),
            new BallPrefabStatus(false, BallSituation.Yellow, BallSituation.Yellow, 1.35f + globalSpareTime, 1.5f),
            new BallPrefabStatus(false, BallSituation.Green, BallSituation.Green, 1.85f + globalSpareTime, 1.5f),
            new BallPrefabStatus(false, BallSituation.Green, BallSituation.Green, 1.85f + globalSpareTime, 1.5f)
        };

        practice2 = new BallPrefabStatus[] {
            new BallPrefabStatus(true, BallSituation.Green, BallSituation.Yellow, 1.35f + globalSpareTime, 1.5f),
            new BallPrefabStatus(true, BallSituation.Green, BallSituation.Yellow, 1.85f + globalSpareTime, 1.5f),
            new BallPrefabStatus(true, BallSituation.Yellow, BallSituation.Green, 1.85f + globalSpareTime, 1.5f),
            new BallPrefabStatus(true, BallSituation.Yellow, BallSituation.Green, 1.35f + globalSpareTime, 1.5f),
            new BallPrefabStatus(true, BallSituation.Green, BallSituation.Yellow, 1.85f + globalSpareTime, 1.5f)
        };

        practice3 = new BallPrefabStatus[] {
            new BallPrefabStatus(false, BallSituation.Yellow, BallSituation.Yellow, 1.85f + globalSpareTime, 1.5f),
            new BallPrefabStatus(true, BallSituation.Green, BallSituation.Yellow, 1.35f + globalSpareTime, 1.5f),
            new BallPrefabStatus(false, BallSituation.Green, BallSituation.Green, 1.85f + globalSpareTime, 1.5f),
            new BallPrefabStatus(false, BallSituation.Green, BallSituation.Green, 1.85f + globalSpareTime, 1.5f),
            new BallPrefabStatus(true, BallSituation.Yellow, BallSituation.Green, 1.85f + globalSpareTime, 1.5f),
            new BallPrefabStatus(true, BallSituation.Yellow, BallSituation.Green, 1.35f + globalSpareTime, 1.5f),
            new BallPrefabStatus(false, BallSituation.Yellow, BallSituation.Yellow, 1.35f + globalSpareTime, 1.5f),
            new BallPrefabStatus(true, BallSituation.Green, BallSituation.Yellow, 1.35f + globalSpareTime, 1.5f),
            new BallPrefabStatus(false, BallSituation.Green, BallSituation.Green, 1.85f + globalSpareTime, 1.5f),
            new BallPrefabStatus(true, BallSituation.Green, BallSituation.Yellow, 1.85f + globalSpareTime, 1.5f)
        };

        practice4 = new BallPrefabStatus[] {
            new BallPrefabStatus(true, BallSituation.Green, BallSituation.Green, 1.35f + globalSpareTime, 1.5f),
            new BallPrefabStatus(true, BallSituation.Green, BallSituation.Green, 1.85f + globalSpareTime, 1.5f),
            new BallPrefabStatus(true, BallSituation.Yellow, BallSituation.Yellow, 1.85f + globalSpareTime, 1.5f),
            new BallPrefabStatus(true, BallSituation.Green, BallSituation.Green, 1.35f + globalSpareTime, 1.5f),
            new BallPrefabStatus(true, BallSituation.Yellow, BallSituation.Yellow, 1.85f + globalSpareTime, 1.5f)
        };

        practice5 = new BallPrefabStatus[] {
            new BallPrefabStatus(false, BallSituation.Yellow, BallSituation.Green, 1.85f + globalSpareTime, 1.5f),
            new BallPrefabStatus(false, BallSituation.Green, BallSituation.Yellow, 1.35f + globalSpareTime, 1.5f),
            new BallPrefabStatus(false, BallSituation.Green, BallSituation.Yellow, 1.85f + globalSpareTime, 1.5f),
            new BallPrefabStatus(false, BallSituation.Yellow, BallSituation.Green, 1.35f + globalSpareTime, 1.5f),
            new BallPrefabStatus(false, BallSituation.Green, BallSituation.Yellow, 1.85f + globalSpareTime, 1.5f)
        };

        practice6 = new BallPrefabStatus[] {
            new BallPrefabStatus(true, BallSituation.Green, BallSituation.Green, 1.85f + globalSpareTime, 1.5f),
            new BallPrefabStatus(false, BallSituation.Green, BallSituation.Yellow, 1.85f + globalSpareTime, 1.5f),
            new BallPrefabStatus(false, BallSituation.Yellow, BallSituation.Green, 1.85f + globalSpareTime, 1.5f),
            new BallPrefabStatus(true, BallSituation.Yellow, BallSituation.Yellow, 1.35f + globalSpareTime, 1.5f),
            new BallPrefabStatus(true, BallSituation.Green, BallSituation.Green, 1.85f + globalSpareTime, 1.5f),
            new BallPrefabStatus(true, BallSituation.Yellow, BallSituation.Yellow, 1.85f + globalSpareTime, 1.5f),
            new BallPrefabStatus(false, BallSituation.Yellow, BallSituation.Green, 1.35f + globalSpareTime, 1.5f),
            new BallPrefabStatus(false, BallSituation.Green, BallSituation.Yellow, 1.35f + globalSpareTime, 1.5f),
            new BallPrefabStatus(true, BallSituation.Green, BallSituation.Green, 1.35f + globalSpareTime, 1.5f),
            new BallPrefabStatus(false, BallSituation.Green, BallSituation.Yellow, 1.85f + globalSpareTime, 1.5f)
        };

        stage1 = new BallPrefabStatus[] {
            new BallPrefabStatus(false, BallSituation.Yellow, BallSituation.Yellow, 1.85f + globalSpareTime, 1.5f),
            new BallPrefabStatus(false, BallSituation.Green, BallSituation.Green, 1.85f + globalSpareTime, 1.5f),
            new BallPrefabStatus(false, BallSituation.Green, BallSituation.Green, 1.35f + globalSpareTime, 1.5f),
            new BallPrefabStatus(true, BallSituation.Yellow, BallSituation.Green, 1.85f + globalSpareTime, 0.75f), //3
            new BallPrefabStatus(true, BallSituation.Green, BallSituation.Yellow, 1.35f + globalSpareTime, 1.5f),
            new BallPrefabStatus(false, BallSituation.Green, BallSituation.Green, 1.35f + globalSpareTime, 1.5f),
            new BallPrefabStatus(false, BallSituation.Yellow, BallSituation.Yellow, 1.85f + globalSpareTime, 0.75f),
            new BallPrefabStatus(true, BallSituation.Yellow, BallSituation.Green, 1.35f + globalSpareTime, 0.75f),
            new BallPrefabStatus(true, BallSituation.Yellow, BallSituation.Green, 1.35f + globalSpareTime, 1.5f),
            new BallPrefabStatus(true, BallSituation.Green, BallSituation.Yellow, 1.85f + globalSpareTime, 1.5f),
            new BallPrefabStatus(false, BallSituation.Green, BallSituation.Green, 1.85f + globalSpareTime, 1.5f),
            new BallPrefabStatus(false, BallSituation.Yellow, BallSituation.Yellow, 1.35f + globalSpareTime, 1.5f),
            new BallPrefabStatus(true, BallSituation.Green, BallSituation.Yellow, 1.85f + globalSpareTime, 1.5f),
            new BallPrefabStatus(false, BallSituation.Yellow, BallSituation.Yellow, 1.85f + globalSpareTime, 0.75f), //13
            new BallPrefabStatus(true, BallSituation.Yellow, BallSituation.Green, 1.35f + globalSpareTime, 1.5f),
            new BallPrefabStatus(false, BallSituation.Green, BallSituation.Green, 1.35f + globalSpareTime, 1.5f),
            new BallPrefabStatus(true, BallSituation.Green, BallSituation.Yellow, 1.85f + globalSpareTime, 1.5f),
            new BallPrefabStatus(false, BallSituation.Yellow, BallSituation.Yellow, 1.35f + globalSpareTime, 1.5f),
            new BallPrefabStatus(true, BallSituation.Yellow, BallSituation.Green, 1.85f + globalSpareTime, 1.5f),
            new BallPrefabStatus(false, BallSituation.Green, BallSituation.Green, 1.35f + globalSpareTime, 1.5f),
            new BallPrefabStatus(false, BallSituation.Yellow, BallSituation.Yellow, 1.85f + globalSpareTime, 1.5f),
            new BallPrefabStatus(false, BallSituation.Green, BallSituation.Green, 1.85f + globalSpareTime, 0.75f), //21
            new BallPrefabStatus(true, BallSituation.Yellow, BallSituation.Green, 1.35f + globalSpareTime, 1.5f),
            new BallPrefabStatus(false, BallSituation.Yellow, BallSituation.Yellow, 1.85f + globalSpareTime, 1.5f),
            new BallPrefabStatus(true, BallSituation.Yellow, BallSituation.Green, 1.85f + globalSpareTime, 0.75f),
            new BallPrefabStatus(true, BallSituation.Green, BallSituation.Yellow, 1.35f + globalSpareTime, 0.75f),
            new BallPrefabStatus(true, BallSituation.Green, BallSituation.Yellow, 1.35f + globalSpareTime, 1.5f),
            new BallPrefabStatus(true, BallSituation.Yellow, BallSituation.Green, 1.85f + globalSpareTime, 1.5f),
            new BallPrefabStatus(false, BallSituation.Green, BallSituation.Green, 1.35f + globalSpareTime, 1.5f),
            new BallPrefabStatus(false, BallSituation.Green, BallSituation.Green, 1.85f + globalSpareTime, 0.75f), //29
            new BallPrefabStatus(true, BallSituation.Yellow, BallSituation.Green, 1.35f + globalSpareTime, 1.5f),
            new BallPrefabStatus(false, BallSituation.Yellow, BallSituation.Yellow, 1.35f + globalSpareTime, 1.5f),
            new BallPrefabStatus(true, BallSituation.Green, BallSituation.Yellow, 1.85f + globalSpareTime, 1.5f),
            new BallPrefabStatus(false, BallSituation.Yellow, BallSituation.Yellow, 1.35f + globalSpareTime, 0.75f),
            new BallPrefabStatus(true, BallSituation.Yellow, BallSituation.Green, 1.35f + globalSpareTime, 1.5f),
            new BallPrefabStatus(false, BallSituation.Green, BallSituation.Green, 1.35f + globalSpareTime, 1.5f),
            new BallPrefabStatus(true, BallSituation.Yellow, BallSituation.Green, 1.85f + globalSpareTime, 1.5f),
            new BallPrefabStatus(false, BallSituation.Yellow, BallSituation.Yellow, 1.85f + globalSpareTime, 0.75f),
            new BallPrefabStatus(true, BallSituation.Green, BallSituation.Yellow, 1.35f + globalSpareTime, 1.5f),
            new BallPrefabStatus(true, BallSituation.Yellow, BallSituation.Green, 1.85f + globalSpareTime, 1.5f),
            new BallPrefabStatus(false, BallSituation.Yellow, BallSituation.Yellow, 1.85f + globalSpareTime, 1.5f),
            new BallPrefabStatus(false, BallSituation.Green, BallSituation.Green, 1.35f + globalSpareTime, 0.75f),
            new BallPrefabStatus(true, BallSituation.Yellow, BallSituation.Green, 1.35f + globalSpareTime, 1.5f),
            new BallPrefabStatus(false, BallSituation.Yellow, BallSituation.Yellow, 1.35f + globalSpareTime, 1.5f),
            new BallPrefabStatus(true, BallSituation.Green, BallSituation.Yellow, 1.85f + globalSpareTime, 1.5f),
            new BallPrefabStatus(false, BallSituation.Green, BallSituation.Green, 1.35f + globalSpareTime, 1.5f),
            new BallPrefabStatus(false, BallSituation.Yellow, BallSituation.Yellow, 1.85f + globalSpareTime, 1.5f),
            new BallPrefabStatus(true, BallSituation.Yellow, BallSituation.Green, 1.85f + globalSpareTime, 0.75f),
            new BallPrefabStatus(true, BallSituation.Yellow, BallSituation.Green, 1.35f + globalSpareTime, 0.75f),
            new BallPrefabStatus(true, BallSituation.Yellow, BallSituation.Green, 1.35f + globalSpareTime, 1.5f),
            new BallPrefabStatus(true, BallSituation.Green, BallSituation.Yellow, 1.85f + globalSpareTime, 1.5f),
            new BallPrefabStatus(false, BallSituation.Yellow, BallSituation.Yellow, 1.35f + globalSpareTime, 1.5f),
            new BallPrefabStatus(false, BallSituation.Green, BallSituation.Green, 1.85f + globalSpareTime, 1.5f),
            new BallPrefabStatus(false, BallSituation.Yellow, BallSituation.Yellow, 1.85f + globalSpareTime, 1.5f),
            new BallPrefabStatus(true, BallSituation.Yellow, BallSituation.Green, 1.85f + globalSpareTime, 1.5f),
            new BallPrefabStatus(false, BallSituation.Green, BallSituation.Green, 1.85f + globalSpareTime, 0.75f),
            new BallPrefabStatus(true, BallSituation.Green, BallSituation.Yellow, 1.35f + globalSpareTime, 1.5f),
            new BallPrefabStatus(false, BallSituation.Yellow, BallSituation.Yellow, 1.35f + globalSpareTime, 1.5f),
            new BallPrefabStatus(true, BallSituation.Green, BallSituation.Yellow, 1.85f + globalSpareTime, 1.5f),
            new BallPrefabStatus(false, BallSituation.Green, BallSituation.Green, 1.35f + globalSpareTime, 1.5f),
            new BallPrefabStatus(false, BallSituation.Green, BallSituation.Green, 1.85f + globalSpareTime, 0.75f),
            new BallPrefabStatus(true, BallSituation.Green, BallSituation.Yellow, 1.35f + globalSpareTime, 0.75f), //61
            new BallPrefabStatus(true, BallSituation.Yellow, BallSituation.Green, 1.35f + globalSpareTime, 1.5f),
            new BallPrefabStatus(false, BallSituation.Yellow, BallSituation.Yellow, 1.85f + globalSpareTime, 1.5f),
            new BallPrefabStatus(true, BallSituation.Green, BallSituation.Yellow, 1.85f + globalSpareTime, 1.5f),
            new BallPrefabStatus(false, BallSituation.Yellow, BallSituation.Yellow, 1.35f + globalSpareTime, 1.5f),
            new BallPrefabStatus(false, BallSituation.Green, BallSituation.Green, 1.85f + globalSpareTime, 0.75f),
            new BallPrefabStatus(true, BallSituation.Green, BallSituation.Yellow, 1.35f + globalSpareTime, 1.5f),
            new BallPrefabStatus(true, BallSituation.Green, BallSituation.Yellow, 1.85f + globalSpareTime, 1.5f),
            new BallPrefabStatus(false, BallSituation.Green, BallSituation.Green, 1.35f + globalSpareTime, 0.75f),
            new BallPrefabStatus(true, BallSituation.Green, BallSituation.Yellow, 1.35f + globalSpareTime, 1.5f),
            new BallPrefabStatus(false, BallSituation.Yellow, BallSituation.Yellow, 1.85f + globalSpareTime, 1.5f),
            new BallPrefabStatus(true, BallSituation.Green, BallSituation.Yellow, 1.85f + globalSpareTime, 1.5f),
            new BallPrefabStatus(false, BallSituation.Yellow, BallSituation.Yellow, 1.35f + globalSpareTime, 1.5f),
            new BallPrefabStatus(true, BallSituation.Green, BallSituation.Yellow, 1.85f + globalSpareTime, 1.5f),
            new BallPrefabStatus(false, BallSituation.Yellow, BallSituation.Yellow, 1.85f + globalSpareTime, 0.75f),
            new BallPrefabStatus(true, BallSituation.Yellow, BallSituation.Green, 1.35f + globalSpareTime, 0.75f), //76
            new BallPrefabStatus(true, BallSituation.Yellow, BallSituation.Green, 1.35f + globalSpareTime, 1.5f),
            new BallPrefabStatus(true, BallSituation.Green, BallSituation.Yellow, 1.85f + globalSpareTime, 1.5f),
            new BallPrefabStatus(false, BallSituation.Yellow, BallSituation.Yellow, 1.85f + globalSpareTime, 1.5f),
            new BallPrefabStatus(false, BallSituation.Green, BallSituation.Green, 1.85f + globalSpareTime, 1.5f),
            new BallPrefabStatus(false, BallSituation.Green, BallSituation.Green, 1.35f + globalSpareTime, 1.5f),
            new BallPrefabStatus(true, BallSituation.Yellow, BallSituation.Green, 1.85f + globalSpareTime, 0.75f), //82
            new BallPrefabStatus(true, BallSituation.Green, BallSituation.Yellow, 1.35f + globalSpareTime, 1.5f),
            new BallPrefabStatus(false, BallSituation.Green, BallSituation.Green, 1.35f + globalSpareTime, 1.5f),
            new BallPrefabStatus(false, BallSituation.Yellow, BallSituation.Yellow, 1.85f + globalSpareTime, 0.75f),
            new BallPrefabStatus(true, BallSituation.Yellow, BallSituation.Green, 1.35f + globalSpareTime, 0.75f),
            new BallPrefabStatus(true, BallSituation.Yellow, BallSituation.Green, 1.35f + globalSpareTime, 1.5f),
            new BallPrefabStatus(true, BallSituation.Green, BallSituation.Yellow, 1.85f + globalSpareTime, 1.5f),
            new BallPrefabStatus(false, BallSituation.Green, BallSituation.Green, 1.85f + globalSpareTime, 1.5f),
            new BallPrefabStatus(false, BallSituation.Yellow, BallSituation.Yellow, 1.35f + globalSpareTime, 1.5f),
            new BallPrefabStatus(true, BallSituation.Green, BallSituation.Yellow, 1.85f + globalSpareTime, 1.5f),
            new BallPrefabStatus(false, BallSituation.Yellow, BallSituation.Yellow, 1.85f + globalSpareTime, 0.75f),
            new BallPrefabStatus(true, BallSituation.Yellow, BallSituation.Green, 1.35f + globalSpareTime, 1.5f),
            new BallPrefabStatus(false, BallSituation.Green, BallSituation.Green, 1.35f + globalSpareTime, 1.5f),
            new BallPrefabStatus(true, BallSituation.Green, BallSituation.Yellow, 1.85f + globalSpareTime, 1.5f),
            new BallPrefabStatus(false, BallSituation.Yellow, BallSituation.Yellow, 1.35f + globalSpareTime, 1.5f),
            new BallPrefabStatus(true, BallSituation.Yellow, BallSituation.Green, 1.85f + globalSpareTime, 1.5f),
            new BallPrefabStatus(false, BallSituation.Green, BallSituation.Green, 1.35f + globalSpareTime, 1.5f),
            new BallPrefabStatus(false, BallSituation.Yellow, BallSituation.Yellow, 1.85f + globalSpareTime, 1.5f),
            new BallPrefabStatus(false, BallSituation.Green, BallSituation.Green, 1.85f + globalSpareTime, 0.75f), //100
            new BallPrefabStatus(true, BallSituation.Green, BallSituation.Yellow, 1.35f + globalSpareTime, 1.5f),
            new BallPrefabStatus(false, BallSituation.Yellow, BallSituation.Yellow, 1.85f + globalSpareTime, 1.5f),
            new BallPrefabStatus(true, BallSituation.Yellow, BallSituation.Green, 1.85f + globalSpareTime, 0.75f),
            new BallPrefabStatus(true, BallSituation.Green, BallSituation.Yellow, 1.35f + globalSpareTime, 0.75f),
            new BallPrefabStatus(true, BallSituation.Green, BallSituation.Yellow, 1.35f + globalSpareTime, 1.5f),
            new BallPrefabStatus(true, BallSituation.Yellow, BallSituation.Green, 1.85f + globalSpareTime, 1.5f),
            new BallPrefabStatus(false, BallSituation.Green, BallSituation.Green, 1.35f + globalSpareTime, 1.5f),
            new BallPrefabStatus(false, BallSituation.Green, BallSituation.Green, 1.85f + globalSpareTime, 0.75f), //108
            new BallPrefabStatus(true, BallSituation.Yellow, BallSituation.Green, 1.35f + globalSpareTime, 1.5f),
            new BallPrefabStatus(false, BallSituation.Yellow, BallSituation.Yellow, 1.35f + globalSpareTime, 1.5f),
            new BallPrefabStatus(true, BallSituation.Green, BallSituation.Yellow, 1.85f + globalSpareTime, 1.5f),
            new BallPrefabStatus(false, BallSituation.Green, BallSituation.Green, 1.35f + globalSpareTime, 0.75f),
            new BallPrefabStatus(true, BallSituation.Green, BallSituation.Yellow, 1.35f + globalSpareTime, 1.5f),
            new BallPrefabStatus(false, BallSituation.Green, BallSituation.Green, 1.35f + globalSpareTime, 1.5f),
            new BallPrefabStatus(true, BallSituation.Yellow, BallSituation.Green, 1.85f + globalSpareTime, 1.5f),
            new BallPrefabStatus(false, BallSituation.Yellow, BallSituation.Yellow, 1.85f + globalSpareTime, 0.75f),
            new BallPrefabStatus(true, BallSituation.Green, BallSituation.Yellow, 1.35f + globalSpareTime, 1.5f),
            new BallPrefabStatus(true, BallSituation.Yellow, BallSituation.Green, 1.85f + globalSpareTime, 1.5f),
            new BallPrefabStatus(false, BallSituation.Green, BallSituation.Green, 1.85f + globalSpareTime, 1.5f)
        };

        /*stage2 = new BallPrefabStatus[] {
            new BallPrefabStatus(true, BallSituation.Yellow, BallSituation.Yellow, 1.85f),
            new BallPrefabStatus(true, BallSituation.Green, BallSituation.Green, 1.85f),
            new BallPrefabStatus(true, BallSituation.Green, BallSituation.Green, 1.35f),
            new BallPrefabStatus(false, BallSituation.Yellow, BallSituation.Green, 1.85f),
            new BallPrefabStatus(false, BallSituation.Green, BallSituation.Yellow, 1.35f),
            new BallPrefabStatus(true, BallSituation.Green, BallSituation.Green, 1.35f),
            new BallPrefabStatus(true, BallSituation.Yellow, BallSituation.Yellow, 1.85f),
            new BallPrefabStatus(false, BallSituation.Yellow, BallSituation.Green, 1.35f),
            new BallPrefabStatus(false, BallSituation.Yellow, BallSituation.Green, 1.35f),
            new BallPrefabStatus(false, BallSituation.Green, BallSituation.Yellow, 1.85f),
            new BallPrefabStatus(true, BallSituation.Green, BallSituation.Green, 1.85f),
            new BallPrefabStatus(true, BallSituation.Yellow, BallSituation.Yellow, 1.35f),
            new BallPrefabStatus(false, BallSituation.Green, BallSituation.Yellow, 1.85f),
            new BallPrefabStatus(true, BallSituation.Yellow, BallSituation.Yellow, 1.85f),
            new BallPrefabStatus(false, BallSituation.Yellow, BallSituation.Green, 1.35f),
            new BallPrefabStatus(true, BallSituation.Green, BallSituation.Green, 1.35f),
            new BallPrefabStatus(false, BallSituation.Green, BallSituation.Yellow, 1.85f),
            new BallPrefabStatus(true, BallSituation.Yellow, BallSituation.Yellow, 1.35f),
            new BallPrefabStatus(false, BallSituation.Yellow, BallSituation.Green, 1.85f),
            new BallPrefabStatus(true, BallSituation.Green, BallSituation.Green, 1.35f),
            new BallPrefabStatus(true, BallSituation.Yellow, BallSituation.Yellow, 1.85f),
            new BallPrefabStatus(true, BallSituation.Green, BallSituation.Green, 1.85f),
            new BallPrefabStatus(false, BallSituation.Yellow, BallSituation.Green, 1.35f),
            new BallPrefabStatus(true, BallSituation.Yellow, BallSituation.Yellow, 1.85f),
            new BallPrefabStatus(false, BallSituation.Yellow, BallSituation.Green, 1.85f),
            new BallPrefabStatus(false, BallSituation.Green, BallSituation.Yellow, 1.35f),
            new BallPrefabStatus(false, BallSituation.Green, BallSituation.Yellow, 1.35f),
            new BallPrefabStatus(false, BallSituation.Yellow, BallSituation.Green, 1.85f),
            new BallPrefabStatus(true, BallSituation.Green, BallSituation.Green, 1.35f),
            new BallPrefabStatus(true, BallSituation.Green, BallSituation.Green, 1.85f),
            new BallPrefabStatus(false, BallSituation.Yellow, BallSituation.Green, 1.35f),
            new BallPrefabStatus(true, BallSituation.Yellow, BallSituation.Yellow, 1.35f),
            new BallPrefabStatus(false, BallSituation.Green, BallSituation.Yellow, 1.85f),
            new BallPrefabStatus(true, BallSituation.Yellow, BallSituation.Yellow, 1.35f),
            new BallPrefabStatus(false, BallSituation.Yellow, BallSituation.Green, 1.35f),
            new BallPrefabStatus(true, BallSituation.Green, BallSituation.Green, 1.35f),
            new BallPrefabStatus(false, BallSituation.Yellow, BallSituation.Green, 1.85f),
            new BallPrefabStatus(true, BallSituation.Yellow, BallSituation.Yellow, 1.85f),
            new BallPrefabStatus(false, BallSituation.Green, BallSituation.Yellow, 1.35f),
            new BallPrefabStatus(false, BallSituation.Yellow, BallSituation.Green, 1.85f),
            new BallPrefabStatus(true, BallSituation.Yellow, BallSituation.Yellow, 1.85f),
            new BallPrefabStatus(true, BallSituation.Green, BallSituation.Green, 1.35f),
            new BallPrefabStatus(false, BallSituation.Yellow, BallSituation.Green, 1.35f),
            new BallPrefabStatus(true, BallSituation.Yellow, BallSituation.Yellow, 1.35f),
            new BallPrefabStatus(false, BallSituation.Green, BallSituation.Yellow, 1.85f),
            new BallPrefabStatus(true, BallSituation.Green, BallSituation.Green, 1.35f),
            new BallPrefabStatus(true, BallSituation.Yellow, BallSituation.Yellow, 1.85f),
            new BallPrefabStatus(false, BallSituation.Yellow, BallSituation.Green, 1.85f),
            new BallPrefabStatus(false, BallSituation.Yellow, BallSituation.Green, 1.35f),
            new BallPrefabStatus(false, BallSituation.Yellow, BallSituation.Green, 1.35f),
            new BallPrefabStatus(false, BallSituation.Green, BallSituation.Yellow, 1.85f),
            new BallPrefabStatus(true, BallSituation.Yellow, BallSituation.Yellow, 1.35f),
            new BallPrefabStatus(true, BallSituation.Green, BallSituation.Green, 1.85f),
            new BallPrefabStatus(true, BallSituation.Yellow, BallSituation.Yellow, 1.85f),
            new BallPrefabStatus(false, BallSituation.Yellow, BallSituation.Green, 1.85f),
            new BallPrefabStatus(true, BallSituation.Green, BallSituation.Green, 1.85f),
            new BallPrefabStatus(false, BallSituation.Green, BallSituation.Yellow, 1.35f),
            new BallPrefabStatus(true, BallSituation.Yellow, BallSituation.Yellow, 1.35f),
            new BallPrefabStatus(false, BallSituation.Green, BallSituation.Yellow, 1.85f),
            new BallPrefabStatus(true, BallSituation.Green, BallSituation.Green, 1.35f),
            new BallPrefabStatus(true, BallSituation.Green, BallSituation.Green, 1.85f),
            new BallPrefabStatus(false, BallSituation.Green, BallSituation.Yellow, 1.35f),
            new BallPrefabStatus(false, BallSituation.Yellow, BallSituation.Green, 1.35f),
            new BallPrefabStatus(true, BallSituation.Yellow, BallSituation.Yellow, 1.85f),
            new BallPrefabStatus(false, BallSituation.Green, BallSituation.Yellow, 1.85f),
            new BallPrefabStatus(true, BallSituation.Yellow, BallSituation.Yellow, 1.35f),
            new BallPrefabStatus(true, BallSituation.Green, BallSituation.Green, 1.85f),
            new BallPrefabStatus(false, BallSituation.Green, BallSituation.Yellow, 1.35f),
            new BallPrefabStatus(false, BallSituation.Green, BallSituation.Yellow, 1.85f),
            new BallPrefabStatus(true, BallSituation.Green, BallSituation.Green, 1.35f),
            new BallPrefabStatus(false, BallSituation.Green, BallSituation.Yellow, 1.35f),
            new BallPrefabStatus(true, BallSituation.Yellow, BallSituation.Yellow, 1.85f),
            new BallPrefabStatus(false, BallSituation.Green, BallSituation.Yellow, 1.85f),
            new BallPrefabStatus(true, BallSituation.Yellow, BallSituation.Yellow, 1.35f),
            new BallPrefabStatus(false, BallSituation.Green, BallSituation.Yellow, 1.85f),
            new BallPrefabStatus(true, BallSituation.Yellow, BallSituation.Yellow, 1.85f),
            new BallPrefabStatus(false, BallSituation.Yellow, BallSituation.Green, 1.35f),
            new BallPrefabStatus(false, BallSituation.Yellow, BallSituation.Green, 1.35f),
            new BallPrefabStatus(false, BallSituation.Green, BallSituation.Yellow, 1.85f),
            new BallPrefabStatus(true, BallSituation.Yellow, BallSituation.Yellow, 1.85f),
            new BallPrefabStatus(true, BallSituation.Green, BallSituation.Green, 1.85f),
            new BallPrefabStatus(true, BallSituation.Green, BallSituation.Green, 1.35f),
            new BallPrefabStatus(false, BallSituation.Yellow, BallSituation.Green, 1.85f),
            new BallPrefabStatus(false, BallSituation.Green, BallSituation.Yellow, 1.35f),
            new BallPrefabStatus(true, BallSituation.Green, BallSituation.Green, 1.35f),
            new BallPrefabStatus(true, BallSituation.Yellow, BallSituation.Yellow, 1.85f),
            new BallPrefabStatus(false, BallSituation.Yellow, BallSituation.Green, 1.35f),
            new BallPrefabStatus(false, BallSituation.Yellow, BallSituation.Green, 1.35f),
            new BallPrefabStatus(false, BallSituation.Green, BallSituation.Yellow, 1.85f),
            new BallPrefabStatus(true, BallSituation.Green, BallSituation.Green, 1.85f),
            new BallPrefabStatus(true, BallSituation.Yellow, BallSituation.Yellow, 1.35f),
            new BallPrefabStatus(false, BallSituation.Green, BallSituation.Yellow, 1.85f),
            new BallPrefabStatus(true, BallSituation.Yellow, BallSituation.Yellow, 1.85f),
            new BallPrefabStatus(false, BallSituation.Yellow, BallSituation.Green, 1.35f),
            new BallPrefabStatus(true, BallSituation.Green, BallSituation.Green, 1.35f),
            new BallPrefabStatus(false, BallSituation.Green, BallSituation.Yellow, 1.85f),
            new BallPrefabStatus(true, BallSituation.Yellow, BallSituation.Yellow, 1.35f),
            new BallPrefabStatus(false, BallSituation.Yellow, BallSituation.Green, 1.85f),
            new BallPrefabStatus(true, BallSituation.Green, BallSituation.Green, 1.35f),
            new BallPrefabStatus(true, BallSituation.Yellow, BallSituation.Yellow, 1.85f),
            new BallPrefabStatus(true, BallSituation.Green, BallSituation.Green, 1.85f),
            new BallPrefabStatus(false, BallSituation.Green, BallSituation.Yellow, 1.35f),
            new BallPrefabStatus(true, BallSituation.Yellow, BallSituation.Yellow, 1.85f),
            new BallPrefabStatus(false, BallSituation.Yellow, BallSituation.Green, 1.85f),
            new BallPrefabStatus(false, BallSituation.Green, BallSituation.Yellow, 1.35f),
            new BallPrefabStatus(false, BallSituation.Green, BallSituation.Yellow, 1.35f),
            new BallPrefabStatus(false, BallSituation.Yellow, BallSituation.Green, 1.85f),
            new BallPrefabStatus(true, BallSituation.Green, BallSituation.Green, 1.35f),
            new BallPrefabStatus(true, BallSituation.Green, BallSituation.Green, 1.85f),
            new BallPrefabStatus(false, BallSituation.Yellow, BallSituation.Green, 1.35f),
            new BallPrefabStatus(true, BallSituation.Yellow, BallSituation.Yellow, 1.35f),
            new BallPrefabStatus(false, BallSituation.Green, BallSituation.Yellow, 1.85f),
            new BallPrefabStatus(true, BallSituation.Green, BallSituation.Green, 1.35f),
            new BallPrefabStatus(false, BallSituation.Green, BallSituation.Yellow, 1.35f),
            new BallPrefabStatus(true, BallSituation.Green, BallSituation.Green, 1.35f),
            new BallPrefabStatus(false, BallSituation.Yellow, BallSituation.Green, 1.85f),
            new BallPrefabStatus(true, BallSituation.Yellow, BallSituation.Yellow, 1.85f),
            new BallPrefabStatus(false, BallSituation.Green, BallSituation.Yellow, 1.35f),
            new BallPrefabStatus(false, BallSituation.Yellow, BallSituation.Green, 1.85f),
            new BallPrefabStatus(true, BallSituation.Green, BallSituation.Green, 1.85f)
        };*/

    }

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(narration());
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.LeftControl) && !formCompleted) {
            if (nameInputField.GetComponent<InputField>().text != "" && ageInputField.GetComponent<InputField>().text != "" && gradeInputField.GetComponent<InputField>().text != "") {
                formCompleted = true;
                startForm.SetActive(false);
                StartCoroutine(narration());
            }
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            if (allowToPressSpace) {
                if (step == 0) {
                    allowToPressSpace = false;
                    StartCoroutine(generatePractice(practice1));
                } else if (step == 1) {
                    allowToPressSpace = false;
                    StartCoroutine(generatePractice(practice2));
                } else if (step == 2) {
                    allowToPressSpace = false;
                    StartCoroutine(generatePractice(practice3));
                } else if (step == 4) {
                    allowToPressSpace = false;
                    StartCoroutine(generatePractice(practice4));
                } else if (step == 5) {
                    allowToPressSpace = false;
                    StartCoroutine(generatePractice(practice5));
                } else if (step == 6) {
                    allowToPressSpace = false;
                    StartCoroutine(generatePractice(practice6));
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.S)) {
            if (allowToPressSpace){
                if (step == 3){
                    allowToPressSpace = false;
                    StartCoroutine(generateQuiz(stage1));
                } else if (step == 7) {
                    allowToPressSpace = false;
                    StartCoroutine(generateQuiz(stage2));
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (isPaused) {
                Application.Quit();
            } else {
                isPaused = true;
                Time.timeScale = 0.0f;
                counter.text = "PAUSED (ESC = exit / ENTER = resume / R = restart)";

                if (GameObject.Find("One shot audio")) {
                    GameObject.Find("One shot audio").GetComponent<AudioSource>().Pause();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Return)) {
            if (isPaused) {
                isPaused = false;
                Time.timeScale = 1.0f;
                counter.text = "";

                if (GameObject.Find("One shot audio"))
                {
                    GameObject.Find("One shot audio").GetComponent<AudioSource>().Play();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            if (isPaused)
            {
                isPaused = false;
                Time.timeScale = 1.0f;
                counter.text = "";

                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    IEnumerator narration() {

        if (step == 0) {
            yield return new WaitForSeconds(1.0f);

            AudioSource.PlayClipAtPoint(faIntro1, Vector3.zero);
            yield return new WaitForSeconds(faIntro1.length);

            yield return new WaitForSeconds(1.0f);

            AudioSource.PlayClipAtPoint(faSpace, Vector3.zero);
            yield return new WaitForSeconds(faSpace.length);

            allowToPressSpace = true;

        } else if (step == 1) {

            AudioSource.PlayClipAtPoint(faIntro2, Vector3.zero);
            yield return new WaitForSeconds(faIntro2.length);

            yield return new WaitForSeconds(1.0f);

            AudioSource.PlayClipAtPoint(faSpace, Vector3.zero);
            yield return new WaitForSeconds(faSpace.length);

            allowToPressSpace = true;

        } else if (step == 2) {

            AudioSource.PlayClipAtPoint(faIntro3, Vector3.zero);
            yield return new WaitForSeconds(faIntro3.length);

            yield return new WaitForSeconds(1.0f);

            AudioSource.PlayClipAtPoint(faSpace, Vector3.zero);
            yield return new WaitForSeconds(faSpace.length);

            allowToPressSpace = true;

        } else if (step == 3) {

            AudioSource.PlayClipAtPoint(faIntro4, Vector3.zero);
            yield return new WaitForSeconds(faIntro4.length);

            yield return new WaitForSeconds(1.0f);

            AudioSource.PlayClipAtPoint(faFastaccurate, Vector3.zero);
            yield return new WaitForSeconds(faFastaccurate.length);

            yield return new WaitForSeconds(1.0f);

            AudioSource.PlayClipAtPoint(faS, Vector3.zero);
            yield return new WaitForSeconds(faS.length);

            allowToPressSpace = true;

        } else if (step == 5) {

            AudioSource.PlayClipAtPoint(faIntro7, Vector3.zero);
            yield return new WaitForSeconds(faIntro7.length);

            yield return new WaitForSeconds(1.0f);

            AudioSource.PlayClipAtPoint(faSpace, Vector3.zero);
            yield return new WaitForSeconds(faSpace.length);

            allowToPressSpace = true;
        } else if (step == 6) {

            AudioSource.PlayClipAtPoint(faIntro8, Vector3.zero);
            yield return new WaitForSeconds(faIntro8.length);

            yield return new WaitForSeconds(1.0f);

            AudioSource.PlayClipAtPoint(faFastaccurate, Vector3.zero);
            yield return new WaitForSeconds(faFastaccurate.length);

            yield return new WaitForSeconds(1.0f);

            AudioSource.PlayClipAtPoint(faSpace, Vector3.zero);
            yield return new WaitForSeconds(faSpace.length);

            allowToPressSpace = true;
        } else if (step == 7) {

            AudioSource.PlayClipAtPoint(faIntro9, Vector3.zero);
            yield return new WaitForSeconds(faIntro9.length);

            yield return new WaitForSeconds(1.0f);

            AudioSource.PlayClipAtPoint(faIntro10, Vector3.zero);
            yield return new WaitForSeconds(faIntro10.length);

            yield return new WaitForSeconds(1.0f);

            AudioSource.PlayClipAtPoint(faFastaccurate, Vector3.zero);
            yield return new WaitForSeconds(faFastaccurate.length);

            yield return new WaitForSeconds(1.0f);

            AudioSource.PlayClipAtPoint(faS, Vector3.zero);
            yield return new WaitForSeconds(faS.length);

            allowToPressSpace = true;
        }

    }

    IEnumerator generatePractice(BallPrefabStatus[] levelStatuses)
    {

        yield return new WaitForSeconds(1.0f);

        foreach (BallPrefabStatus levelStatus in levelStatuses)
        {
            instantiateBallPrefab(levelStatus, true);

            yield return new WaitForSeconds(levelStatus.lifeSpanStatus);

            if (madeAMistake)
            {
                madeAMistake = false;
                yield return new WaitForSeconds(3.0f);
            }

            yield return new WaitForSeconds(1.5f);
        }

        step++;
        StartCoroutine(narration());
    }

    IEnumerator generateQuiz(BallPrefabStatus[] levelStatuses) {

        yield return new WaitForSeconds(1.0f);

        foreach (BallPrefabStatus levelStatus in levelStatuses) {
            if ((step == 3 && !stage1tooManyErrors) || (step == 7 && !stage2tooManyErrors)) {
                //counter.text = "question: " + ++stageCounter;
                instantiateBallPrefab(levelStatus, false);

                //yield return new WaitForSeconds(levelStatus.lifeSpanStatus); //todo v1
                yield return new WaitForSeconds(1.7f); //todo v2
                
                //yield return new WaitForSeconds(1.5f); //todo v1
                yield return new WaitForSeconds(levelStatus.delayStatus); //todo v2
            } else {
                break;
            }
        }

        //yield break;
        if (step == 3) {
        //    counter.text = "";
        //    AudioSource.PlayClipAtPoint(faIntro5, Vector3.zero);
        //    yield return new WaitForSeconds(faIntro5.length);

        //    yield return new WaitForSeconds(1.0f);

        //    AudioSource.PlayClipAtPoint(faIntro6, Vector3.zero);
        //    yield return new WaitForSeconds(faIntro6.length);

        //    yield return new WaitForSeconds(1.0f);

        //    AudioSource.PlayClipAtPoint(faSpace, Vector3.zero);
        //    yield return new WaitForSeconds(faSpace.length);

        //    step++;
        //    allowToPressSpace = true;
        //} else {
            counter.text = "";

            resultTable.SetActive(true);

            string finalResultReport;

            if (!stage1tooManyErrors) {
                finalResultReport = calculateParameters(answers1);
            } else {
                finalResultReport = "N/A\nN/A\nN/A\nN/A\nN/A\nN/A\nN/A\nN/A\nN/A\nN/A\nN/A";
            }

            //leftScore.text = finalResultReport;

            string questionsLog = "";
            int tempI = 0;
            foreach (BallAnswers answers in answers1) {
                questionsLog += tempI + "\t" + answers.answerCode + "\t" + answers.answerDelay + "\n";
                tempI++;
            }


            System.IO.Directory.CreateDirectory("C:\\psychology-test");
            System.IO.File.WriteAllText("C:\\psychology-test\\" +
                nameInputField.GetComponent<InputField>().text.Replace("\\", "-") + "-" +
                ageInputField.GetComponent<InputField>().text.Replace("\\", "-") + "-" + gradeInputField.GetComponent<InputField>().text.Replace("\\", "-") + ".txt",
                finalResultReport + "\n------------------------------\n" + "ID\tAnswer\tDelay\n------------------------------\n" + questionsLog);

            //if (!stage2tooManyErrors) {
            //    rightScore.text = calculateParameters(answers2);
            //} else {
            //    rightScore.text = "N/A\nN/A\nN/A\nN/A\nN/A\nN/A\nN/A\nN/A\nN/A\nN/A\nN/A";
            //}

            AudioSource.PlayClipAtPoint(faEnd, Vector3.zero);
            yield return new WaitForSeconds(faEnd.length);
        }
    }
    
    private void instantiateBallPrefab(BallPrefabStatus status, bool practice) {
        if (practice) {
            GameObject ballInstance = Instantiate(ballPrefabPractice);
            ballInstance.GetComponent<BallPrefabPractice>().isOral = status.oralStatus;
            ballInstance.GetComponent<BallPrefabPractice>().question = status.questionStatus;
            ballInstance.GetComponent<BallPrefabPractice>().answer = status.answerStatus;
            ballInstance.GetComponent<BallPrefabPractice>().lifeSpan = status.lifeSpanStatus;
        } else {
            GameObject ballInstance = Instantiate(ballPrefab);
            ballInstance.GetComponent<BallPrefab>().isOral = status.oralStatus;
            ballInstance.GetComponent<BallPrefab>().question = status.questionStatus;
            ballInstance.GetComponent<BallPrefab>().answer = status.answerStatus;
            //ballInstance.GetComponent<BallPrefab>().lifeSpan = status.lifeSpanStatus; //todo ver1
            ballInstance.GetComponent<BallPrefab>().lifeSpan = 1.7f; //todo ver2
        }
    }

    private string calculateParameters(List<BallAnswers> finalAnswers) {
        BallAnswers[] tempAnswerArray = finalAnswers.ToArray();

        //float controlTekaneh = ((
        //    (tempAnswerArray[9].answerCode == 1 ? 1 : 0) +
        //    (tempAnswerArray[27].answerCode == 1 ? 1 : 0) +
        //    (tempAnswerArray[50].answerCode == 1 ? 1 : 0) +
        //    (tempAnswerArray[78].answerCode == 1 ? 1 : 0) +
        //    (tempAnswerArray[88].answerCode == 1 ? 1 : 0) +
        //    (tempAnswerArray[106].answerCode == 1 ? 1 : 0)
        //) / 6.0f) * 100.0f;
        
        float controlTekaneh = 100.0f - (((((
            ((tempAnswerArray[3].answerCode == 2 && tempAnswerArray[3].answerDelay < 0.5f) ? 1 : 0) +
            ((tempAnswerArray[4].answerCode == 2 && tempAnswerArray[4].answerDelay < 0.5f) ? 1 : 0) +
            ((tempAnswerArray[7].answerCode == 2 && tempAnswerArray[7].answerDelay < 0.5f) ? 1 : 0) +
            ((tempAnswerArray[8].answerCode == 2 && tempAnswerArray[8].answerDelay < 0.5f) ? 1 : 0) +
            ((tempAnswerArray[9].answerCode == 2 && tempAnswerArray[9].answerDelay < 0.5f) ? 1 : 0) +
            ((tempAnswerArray[12].answerCode == 2 && tempAnswerArray[12].answerDelay < 0.5f) ? 1 : 0) +
            ((tempAnswerArray[14].answerCode == 2 && tempAnswerArray[14].answerDelay < 0.5f) ? 1 : 0) +
            ((tempAnswerArray[16].answerCode == 2 && tempAnswerArray[16].answerDelay < 0.5f) ? 1 : 0) +
            ((tempAnswerArray[18].answerCode == 2 && tempAnswerArray[18].answerDelay < 0.5f) ? 1 : 0) +
            ((tempAnswerArray[22].answerCode == 2 && tempAnswerArray[22].answerDelay < 0.5f) ? 1 : 0) +
            ((tempAnswerArray[24].answerCode == 2 && tempAnswerArray[24].answerDelay < 0.5f) ? 1 : 0) +
            ((tempAnswerArray[25].answerCode == 2 && tempAnswerArray[25].answerDelay < 0.5f) ? 1 : 0) +
            ((tempAnswerArray[26].answerCode == 2 && tempAnswerArray[26].answerDelay < 0.5f) ? 1 : 0) +
            ((tempAnswerArray[27].answerCode == 2 && tempAnswerArray[27].answerDelay < 0.5f) ? 1 : 0) +
            ((tempAnswerArray[30].answerCode == 2 && tempAnswerArray[30].answerDelay < 0.5f) ? 1 : 0) +
            ((tempAnswerArray[32].answerCode == 2 && tempAnswerArray[32].answerDelay < 0.5f) ? 1 : 0) +
            ((tempAnswerArray[34].answerCode == 2 && tempAnswerArray[34].answerDelay < 0.5f) ? 1 : 0) +
            ((tempAnswerArray[36].answerCode == 2 && tempAnswerArray[36].answerDelay < 0.5f) ? 1 : 0) +
            ((tempAnswerArray[38].answerCode == 2 && tempAnswerArray[38].answerDelay < 0.5f) ? 1 : 0) +
            ((tempAnswerArray[39].answerCode == 2 && tempAnswerArray[39].answerDelay < 0.5f) ? 1 : 0) +
            ((tempAnswerArray[42].answerCode == 2 && tempAnswerArray[42].answerDelay < 0.5f) ? 1 : 0) +
            ((tempAnswerArray[44].answerCode == 2 && tempAnswerArray[44].answerDelay < 0.5f) ? 1 : 0) +
            ((tempAnswerArray[47].answerCode == 2 && tempAnswerArray[47].answerDelay < 0.5f) ? 1 : 0) +
            ((tempAnswerArray[48].answerCode == 2 && tempAnswerArray[48].answerDelay < 0.5f) ? 1 : 0) +
            ((tempAnswerArray[49].answerCode == 2 && tempAnswerArray[49].answerDelay < 0.5f) ? 1 : 0) +
            ((tempAnswerArray[50].answerCode == 2 && tempAnswerArray[50].answerDelay < 0.5f) ? 1 : 0) +
            ((tempAnswerArray[54].answerCode == 2 && tempAnswerArray[54].answerDelay < 0.5f) ? 1 : 0) +
            ((tempAnswerArray[56].answerCode == 2 && tempAnswerArray[56].answerDelay < 0.5f) ? 1 : 0) +
            ((tempAnswerArray[58].answerCode == 2 && tempAnswerArray[58].answerDelay < 0.5f) ? 1 : 0) +
            ((tempAnswerArray[61].answerCode == 2 && tempAnswerArray[61].answerDelay < 0.5f) ? 1 : 0) +
            ((tempAnswerArray[62].answerCode == 2 && tempAnswerArray[62].answerDelay < 0.5f) ? 1 : 0) +
            ((tempAnswerArray[64].answerCode == 2 && tempAnswerArray[64].answerDelay < 0.5f) ? 1 : 0) +
            ((tempAnswerArray[67].answerCode == 2 && tempAnswerArray[67].answerDelay < 0.5f) ? 1 : 0) +
            ((tempAnswerArray[68].answerCode == 2 && tempAnswerArray[68].answerDelay < 0.5f) ? 1 : 0) +
            ((tempAnswerArray[70].answerCode == 2 && tempAnswerArray[70].answerDelay < 0.5f) ? 1 : 0) +
            ((tempAnswerArray[72].answerCode == 2 && tempAnswerArray[72].answerDelay < 0.5f) ? 1 : 0) +
            ((tempAnswerArray[74].answerCode == 2 && tempAnswerArray[74].answerDelay < 0.5f) ? 1 : 0) +
            ((tempAnswerArray[76].answerCode == 2 && tempAnswerArray[76].answerDelay < 0.5f) ? 1 : 0) +
            ((tempAnswerArray[77].answerCode == 2 && tempAnswerArray[77].answerDelay < 0.5f) ? 1 : 0) +
            ((tempAnswerArray[78].answerCode == 2 && tempAnswerArray[78].answerDelay < 0.5f) ? 1 : 0) +
            ((tempAnswerArray[82].answerCode == 2 && tempAnswerArray[82].answerDelay < 0.5f) ? 1 : 0) +
            ((tempAnswerArray[83].answerCode == 2 && tempAnswerArray[83].answerDelay < 0.5f) ? 1 : 0) +
            ((tempAnswerArray[86].answerCode == 2 && tempAnswerArray[86].answerDelay < 0.5f) ? 1 : 0) +
            ((tempAnswerArray[87].answerCode == 2 && tempAnswerArray[87].answerDelay < 0.5f) ? 1 : 0) +
            ((tempAnswerArray[88].answerCode == 2 && tempAnswerArray[88].answerDelay < 0.5f) ? 1 : 0) +
            ((tempAnswerArray[91].answerCode == 2 && tempAnswerArray[91].answerDelay < 0.5f) ? 1 : 0) +
            ((tempAnswerArray[93].answerCode == 2 && tempAnswerArray[93].answerDelay < 0.5f) ? 1 : 0) +
            ((tempAnswerArray[95].answerCode == 2 && tempAnswerArray[95].answerDelay < 0.5f) ? 1 : 0) +
            ((tempAnswerArray[97].answerCode == 2 && tempAnswerArray[97].answerDelay < 0.5f) ? 1 : 0) +
            ((tempAnswerArray[101].answerCode == 2 && tempAnswerArray[101].answerDelay < 0.5f) ? 1 : 0) +
            ((tempAnswerArray[103].answerCode == 2 && tempAnswerArray[103].answerDelay < 0.5f) ? 1 : 0) +
            ((tempAnswerArray[104].answerCode == 2 && tempAnswerArray[104].answerDelay < 0.5f) ? 1 : 0) +
            ((tempAnswerArray[105].answerCode == 2 && tempAnswerArray[105].answerDelay < 0.5f) ? 1 : 0) +
            ((tempAnswerArray[106].answerCode == 2 && tempAnswerArray[106].answerDelay < 0.5f) ? 1 : 0) +
            ((tempAnswerArray[109].answerCode == 2 && tempAnswerArray[109].answerDelay < 0.5f) ? 1 : 0) +
            ((tempAnswerArray[111].answerCode == 2 && tempAnswerArray[111].answerDelay < 0.5f) ? 1 : 0) +
            ((tempAnswerArray[113].answerCode == 2 && tempAnswerArray[113].answerDelay < 0.5f) ? 1 : 0) +
            ((tempAnswerArray[115].answerCode == 2 && tempAnswerArray[115].answerDelay < 0.5f) ? 1 : 0) +
            ((tempAnswerArray[117].answerCode == 2 && tempAnswerArray[117].answerDelay < 0.5f) ? 1 : 0) +
            ((tempAnswerArray[118].answerCode == 2 && tempAnswerArray[118].answerDelay < 0.5f) ? 1 : 0))
            / 60.0f)*100.0f) + (((
            (tempAnswerArray[0].answerCode == 2 ? 1 : 0) +
            (tempAnswerArray[1].answerCode == 2 ? 1 : 0) +
            (tempAnswerArray[2].answerCode == 2 ? 1 : 0) +
            (tempAnswerArray[5].answerCode == 2 ? 1 : 0) +
            (tempAnswerArray[6].answerCode == 2 ? 1 : 0) +
            (tempAnswerArray[10].answerCode == 2 ? 1 : 0) +
            (tempAnswerArray[11].answerCode == 2 ? 1 : 0) +
            (tempAnswerArray[13].answerCode == 2 ? 1 : 0) +
            (tempAnswerArray[15].answerCode == 2 ? 1 : 0) +
            (tempAnswerArray[17].answerCode == 2 ? 1 : 0) +
            (tempAnswerArray[19].answerCode == 2 ? 1 : 0) +
            (tempAnswerArray[20].answerCode == 2 ? 1 : 0) +
            (tempAnswerArray[21].answerCode == 2 ? 1 : 0) +
            (tempAnswerArray[23].answerCode == 2 ? 1 : 0) +
            (tempAnswerArray[28].answerCode == 2 ? 1 : 0) +
            (tempAnswerArray[29].answerCode == 2 ? 1 : 0) +
            (tempAnswerArray[31].answerCode == 2 ? 1 : 0) +
            (tempAnswerArray[33].answerCode == 2 ? 1 : 0) +
            (tempAnswerArray[35].answerCode == 2 ? 1 : 0) +
            (tempAnswerArray[37].answerCode == 2 ? 1 : 0) +
            (tempAnswerArray[40].answerCode == 2 ? 1 : 0) +
            (tempAnswerArray[41].answerCode == 2 ? 1 : 0) +
            (tempAnswerArray[43].answerCode == 2 ? 1 : 0) +
            (tempAnswerArray[45].answerCode == 2 ? 1 : 0) +
            (tempAnswerArray[46].answerCode == 2 ? 1 : 0) +
            (tempAnswerArray[51].answerCode == 2 ? 1 : 0) +
            (tempAnswerArray[52].answerCode == 2 ? 1 : 0) +
            (tempAnswerArray[53].answerCode == 2 ? 1 : 0) +
            (tempAnswerArray[55].answerCode == 2 ? 1 : 0) +
            (tempAnswerArray[57].answerCode == 2 ? 1 : 0) +
            (tempAnswerArray[59].answerCode == 2 ? 1 : 0) +
            (tempAnswerArray[60].answerCode == 2 ? 1 : 0) +
            (tempAnswerArray[63].answerCode == 2 ? 1 : 0) +
            (tempAnswerArray[65].answerCode == 2 ? 1 : 0) +
            (tempAnswerArray[66].answerCode == 2 ? 1 : 0) +
            (tempAnswerArray[69].answerCode == 2 ? 1 : 0) +
            (tempAnswerArray[71].answerCode == 2 ? 1 : 0) +
            (tempAnswerArray[73].answerCode == 2 ? 1 : 0) +
            (tempAnswerArray[75].answerCode == 2 ? 1 : 0) +
            (tempAnswerArray[79].answerCode == 2 ? 1 : 0) +
            (tempAnswerArray[80].answerCode == 2 ? 1 : 0) +
            (tempAnswerArray[81].answerCode == 2 ? 1 : 0) +
            (tempAnswerArray[84].answerCode == 2 ? 1 : 0) +
            (tempAnswerArray[85].answerCode == 2 ? 1 : 0) +
            (tempAnswerArray[89].answerCode == 2 ? 1 : 0) +
            (tempAnswerArray[90].answerCode == 2 ? 1 : 0) +
            (tempAnswerArray[92].answerCode == 2 ? 1 : 0) +
            (tempAnswerArray[94].answerCode == 2 ? 1 : 0) +
            (tempAnswerArray[96].answerCode == 2 ? 1 : 0) +
            (tempAnswerArray[98].answerCode == 2 ? 1 : 0) +
            (tempAnswerArray[99].answerCode == 2 ? 1 : 0) +
            (tempAnswerArray[100].answerCode == 2 ? 1 : 0) +
            (tempAnswerArray[102].answerCode == 2 ? 1 : 0) +
            (tempAnswerArray[107].answerCode == 2 ? 1 : 0) +
            (tempAnswerArray[108].answerCode == 2 ? 1 : 0) +
            (tempAnswerArray[110].answerCode == 2 ? 1 : 0) +
            (tempAnswerArray[112].answerCode == 2 ? 1 : 0) +
            (tempAnswerArray[114].answerCode == 2 ? 1 : 0) +
            (tempAnswerArray[116].answerCode == 2 ? 1 : 0) +
            (tempAnswerArray[119].answerCode == 2 ? 1 : 0))
            / 60.0f)*100.0f)) / 2.0f);

        float amadegi = ((
            (tempAnswerArray[3].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[22].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[54].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[82].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[101].answerCode == 1 ? 1 : 0)
        ) / 5.0f) * 100.0f;

        float sorat = 0.0f;
        try {
            sorat = (
                (tempAnswerArray[3].answerCode == 1 ? tempAnswerArray[3].answerDelay : 0) +
                (tempAnswerArray[4].answerCode == 1 ? tempAnswerArray[4].answerDelay : 0) +
                (tempAnswerArray[7].answerCode == 1 ? tempAnswerArray[7].answerDelay : 0) +
                (tempAnswerArray[8].answerCode == 1 ? tempAnswerArray[8].answerDelay : 0) +
                (tempAnswerArray[9].answerCode == 1 ? tempAnswerArray[9].answerDelay : 0) +
                (tempAnswerArray[12].answerCode == 1 ? tempAnswerArray[12].answerDelay : 0) +
                (tempAnswerArray[14].answerCode == 1 ? tempAnswerArray[14].answerDelay : 0) +
                (tempAnswerArray[16].answerCode == 1 ? tempAnswerArray[16].answerDelay : 0) +
                (tempAnswerArray[18].answerCode == 1 ? tempAnswerArray[18].answerDelay : 0) +
                (tempAnswerArray[22].answerCode == 1 ? tempAnswerArray[22].answerDelay : 0) +
                (tempAnswerArray[24].answerCode == 1 ? tempAnswerArray[24].answerDelay : 0) +
                (tempAnswerArray[25].answerCode == 1 ? tempAnswerArray[25].answerDelay : 0) +
                (tempAnswerArray[26].answerCode == 1 ? tempAnswerArray[26].answerDelay : 0) +
                (tempAnswerArray[27].answerCode == 1 ? tempAnswerArray[27].answerDelay : 0) +
                (tempAnswerArray[30].answerCode == 1 ? tempAnswerArray[30].answerDelay : 0) +
                (tempAnswerArray[32].answerCode == 1 ? tempAnswerArray[32].answerDelay : 0) +
                (tempAnswerArray[34].answerCode == 1 ? tempAnswerArray[34].answerDelay : 0) +
                (tempAnswerArray[36].answerCode == 1 ? tempAnswerArray[36].answerDelay : 0) +
                (tempAnswerArray[38].answerCode == 1 ? tempAnswerArray[38].answerDelay : 0) +
                (tempAnswerArray[39].answerCode == 1 ? tempAnswerArray[39].answerDelay : 0) +
                (tempAnswerArray[42].answerCode == 1 ? tempAnswerArray[42].answerDelay : 0) +
                (tempAnswerArray[44].answerCode == 1 ? tempAnswerArray[44].answerDelay : 0) +
                (tempAnswerArray[47].answerCode == 1 ? tempAnswerArray[47].answerDelay : 0) +
                (tempAnswerArray[48].answerCode == 1 ? tempAnswerArray[48].answerDelay : 0) +
                (tempAnswerArray[49].answerCode == 1 ? tempAnswerArray[49].answerDelay : 0) +
                (tempAnswerArray[50].answerCode == 1 ? tempAnswerArray[50].answerDelay : 0) +
                (tempAnswerArray[54].answerCode == 1 ? tempAnswerArray[54].answerDelay : 0) +
                (tempAnswerArray[56].answerCode == 1 ? tempAnswerArray[56].answerDelay : 0) +
                (tempAnswerArray[58].answerCode == 1 ? tempAnswerArray[58].answerDelay : 0) +
                (tempAnswerArray[61].answerCode == 1 ? tempAnswerArray[61].answerDelay : 0) +
                (tempAnswerArray[62].answerCode == 1 ? tempAnswerArray[62].answerDelay : 0) +
                (tempAnswerArray[64].answerCode == 1 ? tempAnswerArray[64].answerDelay : 0) +
                (tempAnswerArray[67].answerCode == 1 ? tempAnswerArray[67].answerDelay : 0) +
                (tempAnswerArray[68].answerCode == 1 ? tempAnswerArray[68].answerDelay : 0) +
                (tempAnswerArray[70].answerCode == 1 ? tempAnswerArray[70].answerDelay : 0) +
                (tempAnswerArray[72].answerCode == 1 ? tempAnswerArray[72].answerDelay : 0) +
                (tempAnswerArray[74].answerCode == 1 ? tempAnswerArray[74].answerDelay : 0) +
                (tempAnswerArray[76].answerCode == 1 ? tempAnswerArray[76].answerDelay : 0) +
                (tempAnswerArray[77].answerCode == 1 ? tempAnswerArray[77].answerDelay : 0) +
                (tempAnswerArray[78].answerCode == 1 ? tempAnswerArray[78].answerDelay : 0) +
                (tempAnswerArray[82].answerCode == 1 ? tempAnswerArray[82].answerDelay : 0) +
                (tempAnswerArray[83].answerCode == 1 ? tempAnswerArray[83].answerDelay : 0) +
                (tempAnswerArray[86].answerCode == 1 ? tempAnswerArray[86].answerDelay : 0) +
                (tempAnswerArray[87].answerCode == 1 ? tempAnswerArray[87].answerDelay : 0) +
                (tempAnswerArray[88].answerCode == 1 ? tempAnswerArray[88].answerDelay : 0) +
                (tempAnswerArray[91].answerCode == 1 ? tempAnswerArray[91].answerDelay : 0) +
                (tempAnswerArray[93].answerCode == 1 ? tempAnswerArray[93].answerDelay : 0) +
                (tempAnswerArray[95].answerCode == 1 ? tempAnswerArray[95].answerDelay : 0) +
                (tempAnswerArray[97].answerCode == 1 ? tempAnswerArray[97].answerDelay : 0) +
                (tempAnswerArray[101].answerCode == 1 ? tempAnswerArray[101].answerDelay : 0) +
                (tempAnswerArray[103].answerCode == 1 ? tempAnswerArray[103].answerDelay : 0) +
                (tempAnswerArray[104].answerCode == 1 ? tempAnswerArray[104].answerDelay : 0) +
                (tempAnswerArray[105].answerCode == 1 ? tempAnswerArray[105].answerDelay : 0) +
                (tempAnswerArray[106].answerCode == 1 ? tempAnswerArray[106].answerDelay : 0) +
                (tempAnswerArray[109].answerCode == 1 ? tempAnswerArray[109].answerDelay : 0) +
                (tempAnswerArray[111].answerCode == 1 ? tempAnswerArray[111].answerDelay : 0) +
                (tempAnswerArray[113].answerCode == 1 ? tempAnswerArray[113].answerDelay : 0) +
                (tempAnswerArray[115].answerCode == 1 ? tempAnswerArray[115].answerDelay : 0) +
                (tempAnswerArray[117].answerCode == 1 ? tempAnswerArray[117].answerDelay : 0) +
                (tempAnswerArray[118].answerCode == 1 ? tempAnswerArray[118].answerDelay : 0) + 0.0f
            ) / (
                (tempAnswerArray[3].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[4].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[7].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[8].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[9].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[12].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[14].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[16].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[18].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[22].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[24].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[25].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[26].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[27].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[30].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[32].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[34].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[36].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[38].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[39].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[42].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[44].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[47].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[48].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[49].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[50].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[54].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[56].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[58].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[61].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[62].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[64].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[67].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[68].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[70].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[72].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[74].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[76].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[77].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[78].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[82].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[83].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[86].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[87].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[88].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[91].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[93].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[95].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[97].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[101].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[103].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[104].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[105].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[106].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[109].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[111].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[113].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[115].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[117].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[118].answerCode == 1 ? 1 : 0) + 0.0f
            ) * 1000;
        } catch (DivideByZeroException e) {
            sorat = 0.0f;
        }

        float bazdaari = 0.0f;
        try {
            bazdaari = ((
            (tempAnswerArray[3].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[4].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[7].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[8].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[9].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[12].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[14].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[16].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[18].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[22].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[24].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[25].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[26].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[27].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[30].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[32].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[34].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[36].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[38].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[39].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[42].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[44].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[47].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[48].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[49].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[50].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[54].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[56].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[58].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[61].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[62].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[64].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[67].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[68].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[70].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[72].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[74].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[76].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[77].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[78].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[82].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[83].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[86].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[87].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[88].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[91].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[93].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[95].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[97].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[101].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[103].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[104].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[105].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[106].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[109].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[111].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[113].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[115].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[117].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[118].answerCode == 1 ? 1 : 0) + 0.0f
        ) / (
            (tempAnswerArray[0].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[1].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[2].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[5].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[6].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[10].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[11].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[13].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[15].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[17].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[19].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[20].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[21].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[23].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[28].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[29].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[31].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[33].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[35].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[37].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[40].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[41].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[43].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[45].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[46].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[51].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[52].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[53].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[55].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[57].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[59].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[60].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[63].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[65].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[66].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[69].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[71].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[73].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[75].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[79].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[80].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[81].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[84].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[85].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[89].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[90].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[92].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[94].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[96].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[98].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[99].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[100].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[102].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[107].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[108].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[110].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[112].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[114].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[116].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[119].answerCode == 1 ? 1 : 0) + 0.0f
        )) * 100.0f;
        } catch (DivideByZeroException e) {
            bazdaari = 0.0f;
        }

        float hooshyari = 100.0f - (((
            (tempAnswerArray[3].answerCode == 0 ? 1 : 0) +
            (tempAnswerArray[4].answerCode == 0 ? 1 : 0) +
            (tempAnswerArray[7].answerCode == 0 ? 1 : 0) +
            (tempAnswerArray[8].answerCode == 0 ? 1 : 0) +
            (tempAnswerArray[9].answerCode == 0 ? 1 : 0) +
            (tempAnswerArray[12].answerCode == 0 ? 1 : 0) +
            (tempAnswerArray[14].answerCode == 0 ? 1 : 0) +
            (tempAnswerArray[16].answerCode == 0 ? 1 : 0) +
            (tempAnswerArray[18].answerCode == 0 ? 1 : 0) +
            (tempAnswerArray[22].answerCode == 0 ? 1 : 0) +
            (tempAnswerArray[24].answerCode == 0 ? 1 : 0) +
            (tempAnswerArray[25].answerCode == 0 ? 1 : 0) +
            (tempAnswerArray[26].answerCode == 0 ? 1 : 0) +
            (tempAnswerArray[27].answerCode == 0 ? 1 : 0) +
            (tempAnswerArray[30].answerCode == 0 ? 1 : 0) +
            (tempAnswerArray[32].answerCode == 0 ? 1 : 0) +
            (tempAnswerArray[34].answerCode == 0 ? 1 : 0) +
            (tempAnswerArray[36].answerCode == 0 ? 1 : 0) +
            (tempAnswerArray[38].answerCode == 0 ? 1 : 0) +
            (tempAnswerArray[39].answerCode == 0 ? 1 : 0) +
            (tempAnswerArray[42].answerCode == 0 ? 1 : 0) +
            (tempAnswerArray[44].answerCode == 0 ? 1 : 0) +
            (tempAnswerArray[47].answerCode == 0 ? 1 : 0) +
            (tempAnswerArray[48].answerCode == 0 ? 1 : 0) +
            (tempAnswerArray[49].answerCode == 0 ? 1 : 0) +
            (tempAnswerArray[50].answerCode == 0 ? 1 : 0) +
            (tempAnswerArray[54].answerCode == 0 ? 1 : 0) +
            (tempAnswerArray[56].answerCode == 0 ? 1 : 0) +
            (tempAnswerArray[58].answerCode == 0 ? 1 : 0) +
            (tempAnswerArray[61].answerCode == 0 ? 1 : 0) +
            (tempAnswerArray[62].answerCode == 0 ? 1 : 0) +
            (tempAnswerArray[64].answerCode == 0 ? 1 : 0) +
            (tempAnswerArray[67].answerCode == 0 ? 1 : 0) +
            (tempAnswerArray[68].answerCode == 0 ? 1 : 0) +
            (tempAnswerArray[70].answerCode == 0 ? 1 : 0) +
            (tempAnswerArray[72].answerCode == 0 ? 1 : 0) +
            (tempAnswerArray[74].answerCode == 0 ? 1 : 0) +
            (tempAnswerArray[76].answerCode == 0 ? 1 : 0) +
            (tempAnswerArray[77].answerCode == 0 ? 1 : 0) +
            (tempAnswerArray[78].answerCode == 0 ? 1 : 0) +
            (tempAnswerArray[82].answerCode == 0 ? 1 : 0) +
            (tempAnswerArray[83].answerCode == 0 ? 1 : 0) +
            (tempAnswerArray[86].answerCode == 0 ? 1 : 0) +
            (tempAnswerArray[87].answerCode == 0 ? 1 : 0) +
            (tempAnswerArray[88].answerCode == 0 ? 1 : 0) +
            (tempAnswerArray[91].answerCode == 0 ? 1 : 0) +
            (tempAnswerArray[93].answerCode == 0 ? 1 : 0) +
            (tempAnswerArray[95].answerCode == 0 ? 1 : 0) +
            (tempAnswerArray[97].answerCode == 0 ? 1 : 0) +
            (tempAnswerArray[101].answerCode == 0 ? 1 : 0) +
            (tempAnswerArray[103].answerCode == 0 ? 1 : 0) +
            (tempAnswerArray[104].answerCode == 0 ? 1 : 0) +
            (tempAnswerArray[105].answerCode == 0 ? 1 : 0) +
            (tempAnswerArray[106].answerCode == 0 ? 1 : 0) +
            (tempAnswerArray[109].answerCode == 0 ? 1 : 0) +
            (tempAnswerArray[111].answerCode == 0 ? 1 : 0) +
            (tempAnswerArray[113].answerCode == 0 ? 1 : 0) +
            (tempAnswerArray[115].answerCode == 0 ? 1 : 0) +
            (tempAnswerArray[117].answerCode == 0 ? 1 : 0) +
            (tempAnswerArray[118].answerCode == 0 ? 1 : 0)
        ) / 60.0f) * 100.0f);

        float sobat = 0.0f;
        try
        {
            sobat = ((
            (tempAnswerArray[91].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[93].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[95].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[97].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[101].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[103].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[104].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[105].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[106].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[109].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[111].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[113].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[115].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[117].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[118].answerCode == 1 ? 1 : 0) + 0.0f
        ) / (
            (tempAnswerArray[3].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[4].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[7].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[8].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[9].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[12].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[14].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[16].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[18].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[22].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[24].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[25].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[26].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[27].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[30].answerCode == 1 ? 1 : 0) + 0.0f
        )) * 100.0f;
        }
        catch (DivideByZeroException e) {
            sobat = 0.0f;
        }

        float esteghamat = 0.0f;
        try {
            esteghamat = ((
            (
                (tempAnswerArray[91].answerCode == 1 ? tempAnswerArray[91].answerDelay : 0) +
                (tempAnswerArray[93].answerCode == 1 ? tempAnswerArray[93].answerDelay : 0) +
                (tempAnswerArray[95].answerCode == 1 ? tempAnswerArray[95].answerDelay : 0) +
                (tempAnswerArray[97].answerCode == 1 ? tempAnswerArray[97].answerDelay : 0) +
                (tempAnswerArray[101].answerCode == 1 ? tempAnswerArray[101].answerDelay : 0) +
                (tempAnswerArray[103].answerCode == 1 ? tempAnswerArray[103].answerDelay : 0) +
                (tempAnswerArray[104].answerCode == 1 ? tempAnswerArray[104].answerDelay : 0) +
                (tempAnswerArray[105].answerCode == 1 ? tempAnswerArray[105].answerDelay : 0) +
                (tempAnswerArray[106].answerCode == 1 ? tempAnswerArray[106].answerDelay : 0) +
                (tempAnswerArray[109].answerCode == 1 ? tempAnswerArray[109].answerDelay : 0) +
                (tempAnswerArray[111].answerCode == 1 ? tempAnswerArray[111].answerDelay : 0) +
                (tempAnswerArray[113].answerCode == 1 ? tempAnswerArray[113].answerDelay : 0) +
                (tempAnswerArray[115].answerCode == 1 ? tempAnswerArray[115].answerDelay : 0) +
                (tempAnswerArray[117].answerCode == 1 ? tempAnswerArray[117].answerDelay : 0) +
                (tempAnswerArray[118].answerCode == 1 ? tempAnswerArray[118].answerDelay : 0) + 0.0f
            ) / (
                (tempAnswerArray[91].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[93].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[95].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[97].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[101].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[103].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[104].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[105].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[106].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[109].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[111].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[113].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[115].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[117].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[118].answerCode == 1 ? 1 : 0) + 0.0f
            )
        ) / (
            (
                (tempAnswerArray[3].answerCode == 1 ? tempAnswerArray[3].answerDelay : 0) +
                (tempAnswerArray[4].answerCode == 1 ? tempAnswerArray[4].answerDelay : 0) +
                (tempAnswerArray[7].answerCode == 1 ? tempAnswerArray[7].answerDelay : 0) +
                (tempAnswerArray[8].answerCode == 1 ? tempAnswerArray[8].answerDelay : 0) +
                (tempAnswerArray[9].answerCode == 1 ? tempAnswerArray[9].answerDelay : 0) +
                (tempAnswerArray[12].answerCode == 1 ? tempAnswerArray[12].answerDelay : 0) +
                (tempAnswerArray[14].answerCode == 1 ? tempAnswerArray[14].answerDelay : 0) +
                (tempAnswerArray[16].answerCode == 1 ? tempAnswerArray[16].answerDelay : 0) +
                (tempAnswerArray[18].answerCode == 1 ? tempAnswerArray[18].answerDelay : 0) +
                (tempAnswerArray[22].answerCode == 1 ? tempAnswerArray[22].answerDelay : 0) +
                (tempAnswerArray[24].answerCode == 1 ? tempAnswerArray[24].answerDelay : 0) +
                (tempAnswerArray[25].answerCode == 1 ? tempAnswerArray[25].answerDelay : 0) +
                (tempAnswerArray[26].answerCode == 1 ? tempAnswerArray[26].answerDelay : 0) +
                (tempAnswerArray[27].answerCode == 1 ? tempAnswerArray[27].answerDelay : 0) +
                (tempAnswerArray[30].answerCode == 1 ? tempAnswerArray[30].answerDelay : 0) + 0.0f
            ) / (
                (tempAnswerArray[3].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[4].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[7].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[8].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[9].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[12].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[14].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[16].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[18].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[22].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[24].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[25].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[26].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[27].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[30].answerCode == 1 ? 1 : 0) + 0.0f
            )
        )) * 100.0f;
        } catch (DivideByZeroException e) {
            esteghamat = 0.0f;
        }

        float chaboki = 0.0f;
        try
        {
            float chabokiPercentage1 = ((
                (tempAnswerArray[4].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[7].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[8].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[14].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[22].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[25].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[26].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[30].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[34].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[38].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[42].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[48].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[49].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[56].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[61].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[62].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[67].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[70].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[76].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[77].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[83].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[86].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[87].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[93].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[101].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[104].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[105].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[109].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[113].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[117].answerCode == 1 ? 1 : 0) + 0.0f
            ) / 30.0f) * 100.0f;

            float chabokiPercentage2 = 
                ((
                    ((tempAnswerArray[3].answerCode == 1 && tempAnswerArray[4].answerCode == 1) ? 1 : 0) + 
                    ((tempAnswerArray[7].answerCode == 1 && tempAnswerArray[8].answerCode == 1) ? 1 : 0) + 
                    ((tempAnswerArray[25].answerCode == 1 && tempAnswerArray[26].answerCode == 1) ? 1 : 0) + 
                    ((tempAnswerArray[48].answerCode == 1 && tempAnswerArray[49].answerCode == 1) ? 1 : 0) + 
                    ((tempAnswerArray[61].answerCode == 1 && tempAnswerArray[62].answerCode == 1) ? 1 : 0) + 
                    ((tempAnswerArray[76].answerCode == 1 && tempAnswerArray[77].answerCode == 1) ? 1 : 0) + 
                    ((tempAnswerArray[86].answerCode == 1 && tempAnswerArray[87].answerCode == 1) ? 1 : 0) + 
                    ((tempAnswerArray[104].answerCode == 1 && tempAnswerArray[105].answerCode == 1) ? 1 : 0) + 0.0f
                 ) / 8.0f) * 100.0f;
            
            chaboki = (chabokiPercentage1 + chabokiPercentage2) / 2.0f;
        } catch (DivideByZeroException e) {
            chaboki = 0.0f;
        }

        float nahoftegi = ((
            (tempAnswerArray[3].answerCode == 1 ? tempAnswerArray[3].answerDelay : 0) +
            (tempAnswerArray[4].answerCode == 1 ? tempAnswerArray[4].answerDelay : 0) +
            (tempAnswerArray[7].answerCode == 1 ? tempAnswerArray[7].answerDelay : 0) +
            (tempAnswerArray[8].answerCode == 1 ? tempAnswerArray[8].answerDelay : 0) +
            (tempAnswerArray[9].answerCode == 1 ? tempAnswerArray[9].answerDelay : 0) +
            (tempAnswerArray[12].answerCode == 1 ? tempAnswerArray[12].answerDelay : 0) +
            (tempAnswerArray[14].answerCode == 1 ? tempAnswerArray[14].answerDelay : 0) +
            (tempAnswerArray[16].answerCode == 1 ? tempAnswerArray[16].answerDelay : 0) +
            (tempAnswerArray[18].answerCode == 1 ? tempAnswerArray[18].answerDelay : 0) +
            (tempAnswerArray[22].answerCode == 1 ? tempAnswerArray[22].answerDelay : 0) +
            (tempAnswerArray[24].answerCode == 1 ? tempAnswerArray[24].answerDelay : 0) +
            (tempAnswerArray[25].answerCode == 1 ? tempAnswerArray[25].answerDelay : 0) +
            (tempAnswerArray[26].answerCode == 1 ? tempAnswerArray[26].answerDelay : 0) +
            (tempAnswerArray[27].answerCode == 1 ? tempAnswerArray[27].answerDelay : 0) +
            (tempAnswerArray[30].answerCode == 1 ? tempAnswerArray[30].answerDelay : 0) +
            (tempAnswerArray[32].answerCode == 1 ? tempAnswerArray[32].answerDelay : 0) +
            (tempAnswerArray[34].answerCode == 1 ? tempAnswerArray[34].answerDelay : 0) +
            (tempAnswerArray[36].answerCode == 1 ? tempAnswerArray[36].answerDelay : 0) +
            (tempAnswerArray[38].answerCode == 1 ? tempAnswerArray[38].answerDelay : 0) +
            (tempAnswerArray[39].answerCode == 1 ? tempAnswerArray[39].answerDelay : 0) +
            (tempAnswerArray[42].answerCode == 1 ? tempAnswerArray[42].answerDelay : 0) +
            (tempAnswerArray[44].answerCode == 1 ? tempAnswerArray[44].answerDelay : 0) +
            (tempAnswerArray[47].answerCode == 1 ? tempAnswerArray[47].answerDelay : 0) +
            (tempAnswerArray[48].answerCode == 1 ? tempAnswerArray[48].answerDelay : 0) +
            (tempAnswerArray[49].answerCode == 1 ? tempAnswerArray[49].answerDelay : 0) +
            (tempAnswerArray[50].answerCode == 1 ? tempAnswerArray[50].answerDelay : 0) +
            (tempAnswerArray[54].answerCode == 1 ? tempAnswerArray[54].answerDelay : 0) +
            (tempAnswerArray[56].answerCode == 1 ? tempAnswerArray[56].answerDelay : 0) +
            (tempAnswerArray[58].answerCode == 1 ? tempAnswerArray[58].answerDelay : 0) +
            (tempAnswerArray[61].answerCode == 1 ? tempAnswerArray[61].answerDelay : 0) +
            (tempAnswerArray[62].answerCode == 1 ? tempAnswerArray[62].answerDelay : 0) +
            (tempAnswerArray[64].answerCode == 1 ? tempAnswerArray[64].answerDelay : 0) +
            (tempAnswerArray[67].answerCode == 1 ? tempAnswerArray[67].answerDelay : 0) +
            (tempAnswerArray[68].answerCode == 1 ? tempAnswerArray[68].answerDelay : 0) +
            (tempAnswerArray[70].answerCode == 1 ? tempAnswerArray[70].answerDelay : 0) +
            (tempAnswerArray[72].answerCode == 1 ? tempAnswerArray[72].answerDelay : 0) +
            (tempAnswerArray[74].answerCode == 1 ? tempAnswerArray[74].answerDelay : 0) +
            (tempAnswerArray[76].answerCode == 1 ? tempAnswerArray[76].answerDelay : 0) +
            (tempAnswerArray[77].answerCode == 1 ? tempAnswerArray[77].answerDelay : 0) +
            (tempAnswerArray[78].answerCode == 1 ? tempAnswerArray[78].answerDelay : 0) +
            (tempAnswerArray[82].answerCode == 1 ? tempAnswerArray[82].answerDelay : 0) +
            (tempAnswerArray[83].answerCode == 1 ? tempAnswerArray[83].answerDelay : 0) +
            (tempAnswerArray[86].answerCode == 1 ? tempAnswerArray[86].answerDelay : 0) +
            (tempAnswerArray[87].answerCode == 1 ? tempAnswerArray[87].answerDelay : 0) +
            (tempAnswerArray[88].answerCode == 1 ? tempAnswerArray[88].answerDelay : 0) +
            (tempAnswerArray[91].answerCode == 1 ? tempAnswerArray[91].answerDelay : 0) +
            (tempAnswerArray[93].answerCode == 1 ? tempAnswerArray[93].answerDelay : 0) +
            (tempAnswerArray[95].answerCode == 1 ? tempAnswerArray[95].answerDelay : 0) +
            (tempAnswerArray[97].answerCode == 1 ? tempAnswerArray[97].answerDelay : 0) +
            (tempAnswerArray[101].answerCode == 1 ? tempAnswerArray[101].answerDelay : 0) +
            (tempAnswerArray[103].answerCode == 1 ? tempAnswerArray[103].answerDelay : 0) +
            (tempAnswerArray[104].answerCode == 1 ? tempAnswerArray[104].answerDelay : 0) +
            (tempAnswerArray[105].answerCode == 1 ? tempAnswerArray[105].answerDelay : 0) +
            (tempAnswerArray[106].answerCode == 1 ? tempAnswerArray[106].answerDelay : 0) +
            (tempAnswerArray[109].answerCode == 1 ? tempAnswerArray[109].answerDelay : 0) +
            (tempAnswerArray[111].answerCode == 1 ? tempAnswerArray[111].answerDelay : 0) +
            (tempAnswerArray[113].answerCode == 1 ? tempAnswerArray[113].answerDelay : 0) +
            (tempAnswerArray[115].answerCode == 1 ? tempAnswerArray[115].answerDelay : 0) +
            (tempAnswerArray[117].answerCode == 1 ? tempAnswerArray[117].answerDelay : 0) +
            (tempAnswerArray[118].answerCode == 1 ? tempAnswerArray[118].answerDelay : 0) + 0.0f
        ) / (
            (tempAnswerArray[3].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[4].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[7].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[8].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[9].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[12].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[14].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[16].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[18].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[22].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[24].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[25].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[26].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[27].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[30].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[32].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[34].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[36].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[38].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[39].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[42].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[44].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[47].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[48].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[49].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[50].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[54].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[56].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[58].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[61].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[62].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[64].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[67].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[68].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[70].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[72].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[74].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[76].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[77].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[78].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[82].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[83].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[86].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[87].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[88].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[91].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[93].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[95].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[97].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[101].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[103].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[104].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[105].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[106].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[109].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[111].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[113].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[115].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[117].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[118].answerCode == 1 ? 1 : 0) + 0.0f
        )) - ((
            (tempAnswerArray[0].answerCode == 1 ? tempAnswerArray[0].answerDelay : 0) +
            (tempAnswerArray[1].answerCode == 1 ? tempAnswerArray[1].answerDelay : 0) +
            (tempAnswerArray[2].answerCode == 1 ? tempAnswerArray[2].answerDelay : 0) +
            (tempAnswerArray[5].answerCode == 1 ? tempAnswerArray[5].answerDelay : 0) +
            (tempAnswerArray[6].answerCode == 1 ? tempAnswerArray[6].answerDelay : 0) +
            (tempAnswerArray[10].answerCode == 1 ? tempAnswerArray[10].answerDelay : 0) +
            (tempAnswerArray[11].answerCode == 1 ? tempAnswerArray[11].answerDelay : 0) +
            (tempAnswerArray[13].answerCode == 1 ? tempAnswerArray[13].answerDelay : 0) +
            (tempAnswerArray[15].answerCode == 1 ? tempAnswerArray[15].answerDelay : 0) +
            (tempAnswerArray[17].answerCode == 1 ? tempAnswerArray[17].answerDelay : 0) +
            (tempAnswerArray[19].answerCode == 1 ? tempAnswerArray[19].answerDelay : 0) +
            (tempAnswerArray[20].answerCode == 1 ? tempAnswerArray[20].answerDelay : 0) +
            (tempAnswerArray[21].answerCode == 1 ? tempAnswerArray[21].answerDelay : 0) +
            (tempAnswerArray[23].answerCode == 1 ? tempAnswerArray[23].answerDelay : 0) +
            (tempAnswerArray[28].answerCode == 1 ? tempAnswerArray[28].answerDelay : 0) +
            (tempAnswerArray[29].answerCode == 1 ? tempAnswerArray[29].answerDelay : 0) +
            (tempAnswerArray[31].answerCode == 1 ? tempAnswerArray[31].answerDelay : 0) +
            (tempAnswerArray[33].answerCode == 1 ? tempAnswerArray[33].answerDelay : 0) +
            (tempAnswerArray[35].answerCode == 1 ? tempAnswerArray[35].answerDelay : 0) +
            (tempAnswerArray[37].answerCode == 1 ? tempAnswerArray[37].answerDelay : 0) +
            (tempAnswerArray[40].answerCode == 1 ? tempAnswerArray[40].answerDelay : 0) +
            (tempAnswerArray[41].answerCode == 1 ? tempAnswerArray[41].answerDelay : 0) +
            (tempAnswerArray[43].answerCode == 1 ? tempAnswerArray[43].answerDelay : 0) +
            (tempAnswerArray[45].answerCode == 1 ? tempAnswerArray[45].answerDelay : 0) +
            (tempAnswerArray[46].answerCode == 1 ? tempAnswerArray[46].answerDelay : 0) +
            (tempAnswerArray[51].answerCode == 1 ? tempAnswerArray[51].answerDelay : 0) +
            (tempAnswerArray[52].answerCode == 1 ? tempAnswerArray[52].answerDelay : 0) +
            (tempAnswerArray[53].answerCode == 1 ? tempAnswerArray[53].answerDelay : 0) +
            (tempAnswerArray[55].answerCode == 1 ? tempAnswerArray[55].answerDelay : 0) +
            (tempAnswerArray[57].answerCode == 1 ? tempAnswerArray[57].answerDelay : 0) +
            (tempAnswerArray[59].answerCode == 1 ? tempAnswerArray[59].answerDelay : 0) +
            (tempAnswerArray[60].answerCode == 1 ? tempAnswerArray[60].answerDelay : 0) +
            (tempAnswerArray[63].answerCode == 1 ? tempAnswerArray[63].answerDelay : 0) +
            (tempAnswerArray[65].answerCode == 1 ? tempAnswerArray[65].answerDelay : 0) +
            (tempAnswerArray[66].answerCode == 1 ? tempAnswerArray[66].answerDelay : 0) +
            (tempAnswerArray[69].answerCode == 1 ? tempAnswerArray[69].answerDelay : 0) +
            (tempAnswerArray[71].answerCode == 1 ? tempAnswerArray[71].answerDelay : 0) +
            (tempAnswerArray[73].answerCode == 1 ? tempAnswerArray[73].answerDelay : 0) +
            (tempAnswerArray[75].answerCode == 1 ? tempAnswerArray[75].answerDelay : 0) +
            (tempAnswerArray[79].answerCode == 1 ? tempAnswerArray[79].answerDelay : 0) +
            (tempAnswerArray[80].answerCode == 1 ? tempAnswerArray[80].answerDelay : 0) +
            (tempAnswerArray[81].answerCode == 1 ? tempAnswerArray[81].answerDelay : 0) +
            (tempAnswerArray[84].answerCode == 1 ? tempAnswerArray[84].answerDelay : 0) +
            (tempAnswerArray[85].answerCode == 1 ? tempAnswerArray[85].answerDelay : 0) +
            (tempAnswerArray[89].answerCode == 1 ? tempAnswerArray[89].answerDelay : 0) +
            (tempAnswerArray[90].answerCode == 1 ? tempAnswerArray[90].answerDelay : 0) +
            (tempAnswerArray[92].answerCode == 1 ? tempAnswerArray[92].answerDelay : 0) +
            (tempAnswerArray[94].answerCode == 1 ? tempAnswerArray[94].answerDelay : 0) +
            (tempAnswerArray[96].answerCode == 1 ? tempAnswerArray[96].answerDelay : 0) +
            (tempAnswerArray[98].answerCode == 1 ? tempAnswerArray[98].answerDelay : 0) +
            (tempAnswerArray[99].answerCode == 1 ? tempAnswerArray[99].answerDelay : 0) +
            (tempAnswerArray[100].answerCode == 1 ? tempAnswerArray[100].answerDelay : 0) +
            (tempAnswerArray[102].answerCode == 1 ? tempAnswerArray[102].answerDelay : 0) +
            (tempAnswerArray[107].answerCode == 1 ? tempAnswerArray[107].answerDelay : 0) +
            (tempAnswerArray[108].answerCode == 1 ? tempAnswerArray[108].answerDelay : 0) +
            (tempAnswerArray[110].answerCode == 1 ? tempAnswerArray[110].answerDelay : 0) +
            (tempAnswerArray[112].answerCode == 1 ? tempAnswerArray[112].answerDelay : 0) +
            (tempAnswerArray[114].answerCode == 1 ? tempAnswerArray[114].answerDelay : 0) +
            (tempAnswerArray[116].answerCode == 1 ? tempAnswerArray[116].answerDelay : 0) +
            (tempAnswerArray[119].answerCode == 1 ? tempAnswerArray[119].answerDelay : 0) + 0.0f
        ) / (
            (tempAnswerArray[0].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[1].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[2].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[5].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[6].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[10].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[11].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[13].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[15].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[17].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[19].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[20].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[21].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[23].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[28].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[29].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[31].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[33].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[35].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[37].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[40].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[41].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[43].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[45].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[46].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[51].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[52].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[53].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[55].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[57].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[59].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[60].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[63].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[65].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[66].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[69].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[71].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[73].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[75].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[79].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[80].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[81].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[84].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[85].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[89].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[90].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[92].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[94].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[96].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[98].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[99].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[100].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[102].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[107].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[108].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[110].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[112].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[114].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[116].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[119].answerCode == 1 ? 1 : 0) + 0.0f
        )) * 1000;

        float enetaaf = 0.0f;
        try {
            enetaaf = ((
            ((
                (tempAnswerArray[7].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[14].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[16].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[18].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[24].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[34].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[47].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[54].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[56].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[61].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[67].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[70].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[76].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[86].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[93].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[95].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[97].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[101].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[103].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[113].answerCode == 1 ? 1 : 0) + 0.0f
            ) / 20.0f) * 100.0f
        ) + (
            ((
                ((tempAnswerArray[14].answerCode == 1 && tempAnswerArray[16].answerCode == 1 && tempAnswerArray[18].answerCode == 1) ? 1 : 0) +
                ((tempAnswerArray[32].answerCode == 1 && tempAnswerArray[34].answerCode == 1 && tempAnswerArray[36].answerCode == 1) ? 1 : 0) +
                ((tempAnswerArray[54].answerCode == 1 && tempAnswerArray[56].answerCode == 1 && tempAnswerArray[58].answerCode == 1) ? 1 : 0) +
                ((tempAnswerArray[70].answerCode == 1 && tempAnswerArray[72].answerCode == 1 && tempAnswerArray[74].answerCode == 1) ? 1 : 0) +
                ((tempAnswerArray[93].answerCode == 1 && tempAnswerArray[95].answerCode == 1 && tempAnswerArray[97].answerCode == 1) ? 1 : 0) +
                ((tempAnswerArray[111].answerCode == 1 && tempAnswerArray[113].answerCode == 1 && tempAnswerArray[115].answerCode == 1) ? 1 : 0) + 0.0f
            ) / 6.0f) * 100.0f
        ) + (
            ((
                (tempAnswerArray[3].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[4].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[22].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[54].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[82].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[101].answerCode == 1 ? 1 : 0) + 0.0f
            ) / 6.0f) * 100.0f
        ) + (
            ((
                (tempAnswerArray[9].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[27].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[50].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[78].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[88].answerCode == 1 ? 1 : 0) +
                (tempAnswerArray[106].answerCode == 1 ? 1 : 0) + 0.0f
            ) / 6.0f) * 100.0f
        )) / 4;
        } catch (DivideByZeroException e) {
            enetaaf = 0.0f;
        }

        float estemraar = ((((
            (tempAnswerArray[3].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[9].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[12].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[16].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[18].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[24].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[27].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[32].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[36].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[39].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[44].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[47].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[50].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[54].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[58].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[64].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[68].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[72].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[74].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[78].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[82].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[88].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[91].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[95].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[97].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[103].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[106].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[111].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[115].answerCode == 1 ? 1 : 0) +
            (tempAnswerArray[118].answerCode == 1 ? 1 : 0) + 0.0f
        ) / 30.0f) * 100.0f) + esteghamat) / 2.0f;

        string finalResult =
            "controlTekaneh\t" + String.Format("{0:0.00}", controlTekaneh) + "\n" +
            "amadegi\t\t" + String.Format("{0:0.00}", amadegi) + "\n" +
            "sorat\t\t\t" + Mathf.Round(sorat) + "\n" +
            "bazdaari\t\t" + String.Format("{0:0.00}", bazdaari) + "\n" +
            "hooshyari\t\t" + String.Format("{0:0.00}", hooshyari) + "\n" +
            "sobat\t\t\t" + String.Format("{0:0.00}", sobat) + "\n" +
            "esteghamat\t\t" + String.Format("{0:0.00}", esteghamat) + "\n" +
            "chaboki\t\t" + String.Format("{0:0.00}", chaboki) + "\n" +
            "nahoftegi\t\t" + Mathf.Round(nahoftegi) + "\n" +
            "enetaaf\t\t" + String.Format("{0:0.00}", enetaaf) + "\n" +
            "estemraar\t\t" + String.Format("{0:0.00}", estemraar) + "\n";
        
        string finalResultDisplay =
            String.Format("{0:0.00}", controlTekaneh) + "\n" +
            String.Format("{0:0.00}", amadegi) + "\n" +
            Mathf.Round(sorat) + "\n" +
            String.Format("{0:0.00}", bazdaari) + "\n" +
            String.Format("{0:0.00}", hooshyari) + "\n" +
            String.Format("{0:0.00}", sobat) + "\n" +
            String.Format("{0:0.00}", esteghamat) + "\n" +
            String.Format("{0:0.00}", chaboki) + "\n" +
            Mathf.Round(nahoftegi) + "\n" +
            String.Format("{0:0.00}", enetaaf) + "\n" +
            String.Format("{0:0.00}", estemraar) + "\n";
        
        leftScore.text = finalResultDisplay;

        return finalResult;
    }
}
