using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class TimeManager : Singleton<TimeManager>
{
    private float startTime;
    public TimeSpan TimeSinceStartup { get; private set; }

    private void Awake()
    {
        EventSystem.OnLevelStart += OnLevelRestart;
    }
    private void OnDestroy()
    {
        EventSystem.OnLevelStart -= OnLevelRestart;
    }

    private void Update()
    {
        TimeSinceStartup = TimeSpan.FromSeconds(CurrentRunTimeInSeconds());
    }
    private float CurrentRunTimeInSeconds()
    {
        return Time.time - startTime;
    }
    private void OnLevelRestart()
    {
        startTime = Time.time;
    }
}
