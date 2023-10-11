using UnityEngine;

public class UIManager : MonoBehaviour
{

    [SerializeField] MatchTimer matchTimer;
    [SerializeField] MatchHUD matchHUD;
    [SerializeField] GameObject inGameVision;
    [SerializeField] GameObject UIVision;
    public static UIManager instance;
    void Awake()
    {
        if (instance != null && instance != this) Destroy(this);
        else instance = this;
    }

    public void OnChangeState(GameState gameState)
    {
        if (gameState == GameState.INGAME)
        {
            EnlargeVision();
            StartUI();
        }
    }

    public void EnlargeVision()
    {
        inGameVision.GetComponent<Animator>().SetTrigger("enlarge");
        UIVision.GetComponent<Animator>().SetTrigger("enlarge");
    }

    public void setMatchHUDTimer(string text)
    {
        matchHUD.setTimer(text);
    }

    public void StartUI()
    {
        matchTimer.unpauseTimer();
    }

    public void setAmmoText(float maxAmmo, float currentAmmo, bool reloading)
    {
        if (reloading)
        {
            matchHUD.setAmmoReloading();
            return;
        }
        matchHUD.setCurrentAmmo(maxAmmo, currentAmmo);
    }

    public void SetPlayerLevelText(int lvl)
    {
        matchHUD.setLevelText(lvl);
    }

    public void SetPlayerExpBarLength(float currentExp, float maxExp)
    {
        matchHUD.setExpBarAmount(currentExp, maxExp);
    }
}