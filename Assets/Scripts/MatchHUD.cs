using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MatchHUD : MonoBehaviour
{
    [SerializeField] TMP_Text timerComponent;
    [SerializeField] Image ExpBar;
    [SerializeField] TMP_Text LevelComponent;
    [SerializeField] TMP_Text ammoComponent;
    
    public static MatchHUD instance;

    void Awake()
    {
        if (instance != null && instance != this) Destroy(this);
        else instance = this;
    }

    public void setTimer(string text)
    {
        timerComponent.text = text;
    }


    public void setAmmoReloading()
    {
        ammoComponent.text = "Reloading";
    }

    public void setCurrentAmmo(float maxAmmo, float currentAmmo)
    {
        ammoComponent.text = $"{currentAmmo} / {maxAmmo}";
    }


    public void setLevelText(int level)
    {
        LevelComponent.text = level.ToString();
    }

    public void setExpBarAmount(float currentExp, float maxExp)
    {
        ExpBar.fillAmount = currentExp / maxExp;
    }
}