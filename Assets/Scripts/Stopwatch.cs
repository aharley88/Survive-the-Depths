using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

//stopwatch code from http://answers.unity.com/answers/1638458/view.html

public class Stopwatch : MonoBehaviour
{
    private float elapsedRunningTime = 0f;
    private float runningStartTime = 0f;
    private float pauseStartTime = 0f;
    private float elapsedPausedTime = 0f;
    private float totalElapsedPausedTime = 0f;
    private bool running = false;
    private bool paused = false;
    Text timer; 

    private void Awake()
    {
        Begin();
        timer = GetComponent<Text>();
    }

    void Update()
    {
        if (running)
        {
            elapsedRunningTime = Time.time - runningStartTime - totalElapsedPausedTime;
        }
        else if (paused)
        {
            elapsedPausedTime = Time.time - pauseStartTime;
        }
        timer.text = ("Time Elasped: " + elapsedRunningTime);
    }

    public void Begin()
    {
        if (!running && !paused)
        {
            runningStartTime = Time.time;
            running = true;
        }
    }

    public void Pause()
    {
        if (running && !paused)
        {
            running = false;
            pauseStartTime = Time.time;
            paused = true;
        }
    }

    public void Unpause()
    {
        if (!running && paused)
        {
            totalElapsedPausedTime += elapsedPausedTime;
            running = true;
            paused = false;
        }
    }

    public void Reset()
    {
        elapsedRunningTime = 0f;
        runningStartTime = 0f;
        pauseStartTime = 0f;
        elapsedPausedTime = 0f;
        totalElapsedPausedTime = 0f;
        running = false;
        paused = false;
    }

    public int GetMinutes()
    {
        return (int)(elapsedRunningTime / 60f);
    }

    public int GetSeconds()
    {
        return (int)(elapsedRunningTime);
    }

    public float GetMilliseconds()
    {
        return (float)(elapsedRunningTime - System.Math.Truncate(elapsedRunningTime));
    }

    public float GetRawElapsedTime()
    {
        return elapsedRunningTime;
    }
}