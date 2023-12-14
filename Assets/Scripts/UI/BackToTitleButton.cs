using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToTitleButton : MonoBehaviour
{
    public void GoToTitleScreen()
    {
        GameStateManager.Instance.SetGameState(GameState.TitleScreen);
    }
}
