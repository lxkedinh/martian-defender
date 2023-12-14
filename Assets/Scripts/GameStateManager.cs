using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }
    public GameState currentState;
    public Dictionary<GameState, RectTransform> screens;
    public RectTransform titleScreen;
    public RectTransform playingScreen;
    public RectTransform gameWinScreen;
    public RectTransform gameOverScreen;

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
        currentState = GameState.Playing;
        screens = new()
        {
            // { GameState.TitleScreen, titleScreen },
            { GameState.Playing, playingScreen },
            { GameState.Win, gameWinScreen },
            { GameState.Death, gameOverScreen }
        };
    }

    public void SetGameState(GameState state)
    {
        ResetGameState();
        currentState = state;
        screens[currentState].gameObject.SetActive(true);
    }

    private void ResetGameState()
    {
        foreach (var (_, screen) in screens)
        {
            screen.gameObject.SetActive(false);
        }
    }
}

public enum GameState
{
    TitleScreen,
    Playing,
    Death,
    Win
}
