using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMuzzle : MonoBehaviour
{
    [SerializeField] SpriteRenderer _muzzleSpriter;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] Sprite[] _muzzleSprites;

    float _muzzleDuration;


    private void Start()
    {
        _muzzleSpriter.sprite = null;
    }

    void Update()
    {
        if (_muzzleSpriter.sprite != null)
        {
            if (_muzzleDuration <= 0)
                _muzzleSpriter.sprite = null;
            else
                _muzzleDuration -= Time.deltaTime;
        }
    }

    public void CallMuzzle()
    {
        _audioSource.Play();
        _muzzleSpriter.sprite = _muzzleSprites[Random.Range(0, _muzzleSprites.Length)];
        _muzzleDuration = 0.1f;
    }
}
