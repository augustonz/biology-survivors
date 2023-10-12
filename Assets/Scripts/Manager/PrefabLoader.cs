using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabLoader : MonoBehaviour
{
    public static PrefabLoader instance;
    [SerializeField] GameObject _bullet;
    [SerializeField] DamageValue _damageValue;
    [SerializeField] GameObject _bulletHit;
    void Awake() {
        instance=this;
    }

    public GameObject getBullet() {
        return _bullet;
    }

    public GameObject getBulletHit() {
        return _bulletHit;
    }

    public DamageValue getDamageValue() {
        return _damageValue;
    }
}
