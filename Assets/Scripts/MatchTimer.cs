using UnityEngine;

public class MatchTimer : MonoBehaviour
{
    bool _isPaused;
    float _currentTime;
    
    public float GetCurrentTIme { get => _currentTime;}

    public static MatchTimer instance;

    void Awake() {
        if (instance != null && instance != this) Destroy(this);
        else instance = this;
    }

    void Start() {
       ResetTimer();
    }

    void Update()
    {
        if (!_isPaused) _currentTime+=Time.deltaTime;
        UIManager.instance.setMatchHUDTimer(getCurrentTimeString());
    }
    public void ResetTimer() {
        _isPaused=true;
        _currentTime=0f;
    }

    public void pauseTimer() {
        _isPaused=true;
    }

    public void unpauseTimer() {
        _isPaused=false;
    }

    public int getMinutes() {
        return  Mathf.FloorToInt(_currentTime / 60);
    }

    public int getSeconds() {
        return  Mathf.FloorToInt(_currentTime % 60);
    }

    public string getCurrentTimeString() {
        int timeInSeconds = getSeconds();
        int timeInMinutes = getMinutes();
        return string.Format("{0:00}:{1:00}",timeInMinutes,timeInSeconds);
    }

}
