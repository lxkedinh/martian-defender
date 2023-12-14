using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }
    public GameState currentState;
    public Dictionary<GameState, RectTransform> screens;
    public Dictionary<GameState, UnityEvent> events;
    public RectTransform titleScreen;
    public RectTransform playingScreen;
    public RectTransform gameWinScreen;
    public RectTransform gameOverScreen;
    public UnityEvent onTitle;
    public UnityEvent onPlaying;
    public UnityEvent onWin;
    public UnityEvent onGameOver;

    void Awake()
    {
        if (Instance != this && Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentState = GameState.TitleScreen;
        screens = new()
        {
            { GameState.TitleScreen, titleScreen },
            { GameState.Playing, playingScreen },
            { GameState.Win, gameWinScreen },
            { GameState.Death, gameOverScreen }
        };
        events = new()
        {
            {GameState.TitleScreen, onTitle},
            {GameState.Playing, onPlaying},
            {GameState.Win, onWin},
            {GameState.Death, onGameOver}
        };
    }

    public void SetGameState(GameState state)
    {
        ResetGameState();
        currentState = state;
        events[currentState].Invoke();
    }

    private void ResetGameState()
    {
        foreach (var (_, screen) in screens)
        {
            screen.gameObject.SetActive(false);
        }
    }

    public void OnTitle()
    {
        screens[GameState.TitleScreen].gameObject.SetActive(true);
    }

    public void OnPlay()
    {
        screens[GameState.Playing].gameObject.SetActive(true);
        DayNightController.Instance.ChangeToDay();
        InventoryController.Instance.ResetInventory();
    }

    public void OnWin()
    {
        screens[GameState.Win].gameObject.SetActive(true);
        EnemyManager.Instance.DespawnEnemies();
        MapController.Instance.ResetMap();
    }

    public void OnGameOver()
    {
        screens[GameState.Death].gameObject.SetActive(true);
        EnemyManager.Instance.DespawnEnemies();
        MapController.Instance.ResetMap();
    }
}

public enum GameState
{
    TitleScreen,
    Playing,
    Death,
    Win
}
