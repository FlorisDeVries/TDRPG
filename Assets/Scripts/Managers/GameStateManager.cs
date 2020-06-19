using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum GameState
{
    Playing,
    Paused,
    GameOver
}

public class GameStateManager : UnitySingleton<GameStateManager>
{
    public GameState GameState { get; private set; } = GameState.Paused;

    [HideInInspector]
    public UnityEvent OnGameOver = new UnityEvent();

    [HideInInspector]
    public UnityEvent OnPause = new UnityEvent();

    [HideInInspector]
    public UnityEvent OnPlay = new UnityEvent();

    public void GameOver()
    {
        OnGameOver.Invoke();
        GameState = GameState.GameOver;
    }

    public void Pause()
    {
        OnPause.Invoke();
        GameState = GameState.Paused;
    }

    public void Play()
    {
        OnPlay.Invoke();
        GameState = GameState.Playing;
    }
}
