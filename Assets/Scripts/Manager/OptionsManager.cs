using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

[System.Serializable]
public class ScreenResolution {
    public int Height;
    public int Width;
}

public class OptionsManager : MonoBehaviour
{

    [SerializeField] AudioMixer _audioMixer;
    [SerializeField] Slider _BGMSlider;
    [SerializeField] Image _BGMFill;
    [SerializeField] Slider _SFXSlider;
    [SerializeField] Image _SFXFill;
    [SerializeField] Toggle _isFullscreen;
    [SerializeField] TMP_Dropdown _resolutionDropdown;

    [SerializeField] List<ScreenResolution> _resolutions;

    // Start is called before the first frame update
    void Start()
    {
        GetInitialValues();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GetInitialValues() {
        _BGMSlider.value = PlayerPrefs.GetFloat("bgmVolume",0f);
        _audioMixer.SetFloat("bgmVolume",PlayerPrefs.GetFloat("bgmVolume",0f));

        _SFXSlider.value = PlayerPrefs.GetFloat("sfxVolume",0f);
        _audioMixer.SetFloat("sfxVolume",PlayerPrefs.GetFloat("sfxVolume",0f));

        _isFullscreen.isOn = PlayerPrefs.GetInt("isFullscreen",1) == 1 ;
        Screen.fullScreen = PlayerPrefs.GetInt("isFullscreen",1) == 1 ;

        _resolutionDropdown.value = PlayerPrefs.GetInt("resolution",0);

        _resolutions.ForEach((res)=>{
            _resolutionDropdown.AddOptions(new List<TMP_Dropdown.OptionData> {
                new TMP_Dropdown.OptionData {
                    text = $"{res.Width} x {res.Height}"
                }
            });
        });
    }

    public void UpdateFullscreen(bool newValue) {
        Screen.fullScreen = newValue;
        PlayerPrefs.SetInt("isFullscreen",newValue?1:0);
    }

    public void UpdateBGMVolume(float newVolume) {
        PlayerPrefs.SetFloat("bgmVolume",newVolume);
        _audioMixer.SetFloat("bgmVolume",newVolume);
        _BGMFill.fillAmount = 1 - (newVolume/-80);
    }

    public void UpdateSFXVolume(float newVolume) {
        PlayerPrefs.SetFloat("sfxVolume",newVolume);
        _audioMixer.SetFloat("sfxVolume",newVolume);
        _SFXFill.fillAmount = 1 - (newVolume/-80);
    }

    public void UpdateResolution(int newIndex) {
        PlayerPrefs.SetInt("resolution",newIndex);
        ScreenResolution resolution = _resolutions[newIndex];
        Screen.SetResolution(resolution.Width,resolution.Height,Screen.fullScreenMode);
    }
}
