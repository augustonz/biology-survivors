using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsAudioManager : MonoBehaviour
{
    public static ButtonsAudioManager instance;
    [SerializeField] AudioClip[] _audioClip;
    AudioSource _audioSource;

    private void Awake()
    {
        instance = this;
        _audioSource = GetComponent<AudioSource>();
    }

    public void CallAudio(int audio)
    {
        _audioSource.Stop();
        _audioSource.clip = _audioClip[audio];
        _audioSource.Play();
    }
}
