using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum GameState
{
    Playing,
    Paused,
    GameOver,
    Victory
}

public class GameStateManager : UnitySingleton<GameStateManager>
{
    public GameState GameState { get; private set; } = GameState.Playing;

    public Dictionary<GameState, UnityEvent> GameStateEvents { get; private set; } = new Dictionary<GameState, UnityEvent>();

    protected override void Awake()
    {
        base.Awake();

        // Init all toggle events
        foreach (GameState type in Enum.GetValues(typeof(GameState)))
        {
            GameStateEvents.Add(type, new UnityEvent());
        }
    }

    public void SetGameState(GameState state)
    {
        GameStateEvents[state].Invoke();
        GameState = state;
    }
}
