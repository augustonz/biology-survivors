using UnityEngine;

public class UIManager : MonoBehaviour
{
    
    [SerializeField] MatchTimer matchTimer;
    [SerializeField] MatchHUD matchHUD;
    public static UIManager instance;
    void Awake() {
        if (instance != null && instance != this) Destroy(this); 
        else instance = this;
    }

    public void setMatchHUDTimer(string text) {
        matchHUD.setTimer(text);
    }

    public void StartUI() {
        matchTimer.unpauseTimer();
    }

    public void setAmmoText(float maxAmmo,float currentAmmo,bool reloading) {
        if (reloading) {
            matchHUD.setAmmoReloading();
            return;
        }
        matchHUD.setCurrentAmmo(maxAmmo,currentAmmo);
    }

    public void SetPlayerLevelText(int lvl) {
        matchHUD.setLevelText(lvl);
    }

    public void SetPlayerExpBarLength(float currentExp, float maxExp) {
        matchHUD.setExpBarAmount(currentExp,maxExp);
    }
}