using UnityEngine;

public class MatchTimer : MonoBehaviour
{
    private bool isPaused = true;
    private float currentTime = 0f;
    private string timeText;

    public void resetTimer() {
        isPaused=true;
        currentTime=0f;
    }

    public void pauseTimer() {
        isPaused=true;
    }

    public void unpauseTimer() {
        isPaused=false;
    }

    public int getMinutes() {
        return  Mathf.FloorToInt(currentTime / 60);
    }

    public int getSeconds() {
        return  Mathf.FloorToInt(currentTime % 60);
    }

    public float getCurrentTIme() {return currentTime;}

    public string getCurrentTimeString() {
        int timeInSeconds = getSeconds();
        int timeInMinutes = getMinutes();
        return string.Format("{0:00}:{1:00}",timeInMinutes,timeInSeconds);
    }

    void FixedUpdate()
    {
        if (!isPaused) currentTime+=Time.fixedDeltaTime;
        GameHandler.instance.setMatchHUDTimer(getCurrentTimeString());
    }
}
