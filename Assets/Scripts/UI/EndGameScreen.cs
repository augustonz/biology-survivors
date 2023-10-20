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
    [SerializeField] TMP_Text _maxScore;
    Animator _anim;
    bool _isVisible;
    PlayerStatus _playerStatus;
    int _currentMaxScore;
    [SerializeField] int _delay;
    void Start() {
        _anim = GetComponent<Animator>();
    }

    void Update() {
    }

    public async void ShowEndGameOnDelay() {
        int finalScore = CalculateScore();

        SaveFinalScore(finalScore);

        int enemiesKilled = WaveManager.instance.EnemiesKilledCount;
        _playerStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
        int level = _playerStatus.currLevel;

        UpdateScoreText(level,finalScore,enemiesKilled);

        await Task.Delay(_delay);
        
        AppearEndGameScreen();
    }

    void SaveFinalScore(int score) {
        int savedFinalScore = PlayerPrefs.GetInt("maxScore",0);
        _currentMaxScore =  savedFinalScore;
        if (score>savedFinalScore) {
            PlayerPrefs.SetInt("maxScore",score);
            _currentMaxScore = score;
        }
    }

    void UpdateScoreText(int level, int finalScore, int enemiesKilled) {
        _gameTime.text = $"Time survived: {MatchTimer.instance.getCurrentTimeString()}";
        _enemiesKilled.text = $"Enemies Killed: {enemiesKilled}";
        _level.text = $"Max Level: {level}";
        _finalScore.text = $"Total Score: {finalScore}";
        _maxScore.text = $"Max Score: {_currentMaxScore}";
    }

    int CalculateScore() {
        float time = MatchTimer.instance.GetCurrentTIme;
        int enemiesKilled = WaveManager.instance.EnemiesKilledCount;
        _playerStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
        int level = _playerStatus.currLevel;
        int finalScore = (int) (level * 1000 + enemiesKilled * 100 + time * 100);
        return finalScore;
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
