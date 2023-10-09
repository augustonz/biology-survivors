using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public static GameHandler instance;

    GameState _gameState;
    void Awake() {
        if (instance != null && instance != this) Destroy(this); 
        else instance = this;
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
        _gameState=GameState.INGAME;
        UIManager.instance.StartUI();
    }
}

public enum GameState {
    MENU,INGAME,PAUSED
}