using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;

public class EndGameScreen : MonoBehaviour
{
    [SerializeField] TMP_Text _gameTime;
    [SerializeField] TMP_Text _enemiesKilled;
    [SerializeField] TMP_Text _level;
    [SerializeField] TMP_Text _finalScore;
    Animator _anim;
    bool _isVisible;
    PlayerStatus _playerStatus;
    [SerializeField] int _delay;
    void Start() {
        _anim = GetComponent<Animator>();
    }

    void Update() {
    }

    public async void ShowEndGameOnDelay() {
        CalculateScore();
        await Task.Delay(_delay);
        AppearEndGameScreen();
    }

    void CalculateScore() {
        float time = MatchTimer.instance.GetCurrentTIme;
        int enemiesKilled = WaveManager.instance.EnemiesKilledCount;
        _playerStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
        int level = _playerStatus.currLevel;
        int finalScore = (int) (level * 1000 + enemiesKilled * 100 + time * 100);

        _gameTime.text = $"Time survived: {MatchTimer.instance.getCurrentTimeString()}";
        _enemiesKilled.text = $"Enemies Killed: {enemiesKilled}";
        _level.text = $"Max Level: {level}";
        _finalScore.text = $"Total Score: {finalScore}";
    }

    void ToggleVisibility() {
        if (_isVisible) DisappearEndGameScreen();
        else AppearEndGameScreen();
    }

    void AppearEndGameScreen() {
        _anim.SetTrigger("appear");
        _isVisible=true;
        GameHandler.instance.ChangeState(GameState.MENU);
    }

    public void DisappearEndGameScreen() {
        _anim.SetTrigger("disappear");
        _isVisible=false;
        GameHandler.instance.ChangeState(GameState.INGAME);
    }

}
