using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;


public class TimerController : MonoBehaviour
{
    public static TimerController instance;

    public Text timeCounter;
    public Text highScoreText;
    private AlienSling aS;

    private TimeSpan timePlaying;
    private TimeSpan highScoreSpan;
    private float highScoreTime;
    private bool timerGoing;
    private double elapsedTime;
    private int scenNumber;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        aS = FindObjectOfType<AlienSling>();
        scenNumber = SceneManager.GetActiveScene().buildIndex;
        highScoreTime = PlayerPrefs.GetFloat("High Score: " + scenNumber + aS.swipe);

        if (highScoreTime > 0)
        {
            highScoreSpan = TimeSpan.FromSeconds(highScoreTime);
        }

        string highScoreStr = highScoreSpan.ToString("mm':'ss'.'ff"); 
        highScoreText.text = highScoreStr;
        timerGoing = false;
    }

    public void Update()
    {
        if (timerGoing)
        {
            elapsedTime += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            string timePlayingStr = timePlaying.ToString("mm':'ss'.'ff");
            timeCounter.text = timePlayingStr;
        }
    }

    public void BeginTime() 
    {
        timerGoing = true;
        elapsedTime = 0f;


    }

    public void EndTimer()
    {
        timerGoing = false;

        float currentTime = Convert.ToSingle(timePlaying.TotalSeconds);

        if (currentTime < highScoreTime || highScoreTime == 0)
        {
            highScoreTime = currentTime;
            PlayerPrefs.SetFloat("High Score: " + scenNumber, highScoreTime);
        }

        Destroy(this);
    }


}
