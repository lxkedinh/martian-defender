using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DayNightController : MonoBehaviour
{
    public static DayNightController Instance { get; private set; }
    public float dayDuration;
    public float dayTimer;
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

    void Start()
    {
        ChangeToDay();
    }

    // Update is called once per frame
    void Update()
    {
        if (cyclePhase == TimeOfDay.Night || GameStateManager.Instance.currentState != GameState.Playing) return;

        if (dayTimer <= 0)
        {
            cyclePhase = TimeOfDay.Night;
            timeUI.text = "Night has fallen!";
            OnNightFall();
            return;
        }

        dayTimer -= Time.deltaTime;
        timeUI.text = "Night falls in" + "\n" +
        $"{FormatTime(dayTimer)}";

    }

    private string FormatTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void OnNightFall()
    {
        EnemyManager.Instance.SpawnEnemies(Random.Range(8, 14));
    }

    public void ChangeToDay()
    {
        dayDuration = dayTimer = 5 * 60;
        cyclePhase = TimeOfDay.Day;
    }
}

public enum TimeOfDay
{
    Day,
    Night
}
