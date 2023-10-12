using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAudioManager : MonoBehaviour
{
    [SerializeField] AudioSource _levelUp;
    [SerializeField] AudioSource _deathJingle;
    [SerializeField] AudioSource _deathPop;
    [SerializeField] AudioSource _step;

    public void LevelUp()
    {
        _levelUp.Play();
    }

    public void DeathJingle()
    {
        _deathJingle.Play();
    }

    public void DeathPop()
    {
        _deathPop.Play();
    }

    public void Step()
    {
        _step.Play();
    }

}
