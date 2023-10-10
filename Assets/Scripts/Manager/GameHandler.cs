using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OnChangeEvent : UnityEvent<GameState>
{

}

public class GameHandler : MonoBehaviour
{
    public static GameHandler instance;

    public OnChangeEvent OnChange;

    [SerializeField]
    GameState _gameState = GameState.NONE;
    public GameState _GameState { get => _gameState; set => ChangeState(value); }

    void Awake()
    {
        if (instance != null && instance != this) Destroy(this);
        else instance = this;
    }

    void Start()
    {

        ChangeState(GameState.MENU);
    }

    void Update()
    {
        switch (_gameState)
        {
            case GameState.MENU:
                MenuUpdate();
                break;

            case GameState.INGAME:
                InGameUpdate();
                break;

            case GameState.PAUSED:
                PausedUpdate();
                break;

        }
    }



    void ExitState(GameState oldGameState)
    {
        switch (oldGameState)
        {
            case GameState.MENU:
                UIManager.instance.EnlargeVision();
                break;

            case GameState.INGAME:
                break;

            case GameState.PAUSED:
                break;
        }
    }

    void EnterState(GameState newGameState)
    {
        switch (newGameState)
        {
            case GameState.MENU:
                CursorConfig.instance.ShowCursor();
                CursorConfig.instance.ChangeCursorDefault();
                break;

            case GameState.INGAME:
                CursorConfig.instance.ShowCursor();
                break;

            case GameState.PAUSED:
                CursorConfig.instance.ShowCursor();
                break;
        }
    }

    public void ChangeState(GameState ev)
    {
        _gameState = ev;
        OnChange?.Invoke(ev);
    }

    void MenuUpdate()
    {
        Time.timeScale = 0;
    }

    void InGameUpdate()
    {
        Time.timeScale = 1;
    }

    void PausedUpdate()
    {
        Time.timeScale = 0;
    }

    public void StartGame()
    {
        ChangeState(GameState.INGAME);
    }
}

[System.Serializable]
public enum GameState
{
    NONE, MENU, INGAME, PAUSED
}