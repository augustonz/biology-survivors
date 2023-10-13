using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAudioManager : MonoBehaviour
{
    [SerializeField] AudioSource _levelUp;
    [SerializeField] AudioSource _deathJingle;
    [SerializeField] AudioSource _deathPop;
    [SerializeField] AudioSource _step;
    [SerializeField] AudioSource _hurt;
    [SerializeField] AudioSource _xpGather;

    [SerializeField] AudioClip[] _xpGatherSound;

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


    public void Hurt()
    {
        _hurt.Play();
    }

    public void GatheredXP()
    {
        _xpGather.Stop();
        _xpGather.clip = _xpGatherSound[Random.Range(0, _xpGatherSound.Length)];
        _xpGather.Play();
    }
}
