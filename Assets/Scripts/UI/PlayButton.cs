using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : MonoBehaviour
{
    public void GoToPlayScreen()
    {
        GameStateManager.Instance.SetGameState(GameState.Playing);
    }
}
