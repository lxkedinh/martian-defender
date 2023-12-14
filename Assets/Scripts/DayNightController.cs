using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DayNightController : MonoBehaviour
{
    public static DayNightController Instance { get; private set; }
    public float dayDuration = 5 * 60;
    public float currentTime = 0;
    public TimeOfDay cyclePhase;
    public TMP_Text timeUI;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (currentTime >= dayDuration)
        {
            cyclePhase = TimeOfDay.Night;
            timeUI.text = "Night has fallen!";
        }

        currentTime += Time.fixedDeltaTime;
        timeUI.text = $"Night falls in {Math.Truncate(currentTime)}";

    }
}

public enum TimeOfDay
{
    Day,
    Night
}
