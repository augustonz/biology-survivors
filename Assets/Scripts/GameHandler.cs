using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public static GameHandler instance;

    GameState _gameState = GameState.NONE;
    Dictionary<string,GameState> stringToGameState = new Dictionary<string, GameState>();
    void Awake() {
        if (instance != null && instance != this) Destroy(this); 
        else instance = this;
    }

    void Start() {
        stringToGameState.Add("menu",GameState.MENU);
        stringToGameState.Add("ingame",GameState.INGAME);
        stringToGameState.Add("paused",GameState.PAUSED);

        ChangeState("menu");
    }

    void Update() {
        switch(_gameState) {
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

    public void ChangeState(string stateName) {
        GameState newGameState = stringToGameState[stateName];
        if (newGameState==_gameState) return;
        
        if (_gameState!=GameState.NONE) ExitState(_gameState);
        _gameState = newGameState;
        EnterState(newGameState);
    }

    void ExitState(GameState oldGameState) {
        switch(oldGameState) {
            case GameState.MENU:
                UIManager.instance.EnlargeVision();
                break;

            case GameState.INGAME:
                break;

            case GameState.PAUSED:
                break;
        }
    }

    void EnterState(GameState newGameState) {
        switch(newGameState) {
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

    void MenuUpdate() {
        Time.timeScale=0;
    }

    void InGameUpdate() {
        Time.timeScale=1;
    }

    void PausedUpdate() {
        Time.timeScale=0;
    }

    public void StartGame() {
        ChangeState("ingame");
    }
}

public enum GameState {
    NONE,MENU,INGAME,PAUSED
}