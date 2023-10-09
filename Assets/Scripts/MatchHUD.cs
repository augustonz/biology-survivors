using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MatchHUD : MonoBehaviour
{
    private TMP_Text timerComponent;
    private Slider ExpBar;
    private TMP_Text LevelComponent;
    private TMP_Text ammoComponent;

    public static MatchHUD instance;


    void Awake() {
        if (instance != null && instance != this) Destroy(this); 
        else instance = this; 
        timerComponent = GameObject.Find("Timer").GetComponent<TMP_Text>();
        ammoComponent = GameObject.Find("Ammo").GetComponent<TMP_Text>();
        LevelComponent = GameObject.Find("Lvl Text").GetComponent<TMP_Text>();
        ExpBar = GameObject.Find("Exp bar").GetComponent<Slider>();
    } 

    public void setTimer(string text) {
        timerComponent.text=text;
    }

    public void setAmmoReloading() {
        ammoComponent.text="Ammo\nReloading...";
    }

    public void setCurrentAmmo(float maxAmmo, float currentAmmo) {
        ammoComponent.text=$"Ammo\n{currentAmmo.ToString()}/{maxAmmo.ToString()}";
    }

    public void setLevelText(int level) {
        LevelComponent.text=$"Level {level}";
    }

    public void setExpBarAmount(float currentExp, float maxExp) {
        ExpBar.value = currentExp/maxExp;
    }
}