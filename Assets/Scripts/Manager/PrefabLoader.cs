using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabLoader : MonoBehaviour
{
    public static PrefabLoader instance;
    [SerializeField] GameObject _bullet;
    [SerializeField] GameObject _friendlyMissile;
    [SerializeField] ExplosionEfetivation _friendlyMissileExplosion;

    [SerializeField] GameObject _grenade;
    [SerializeField] ExplosionEfetivation _grenadeExplosion;


    [SerializeField] DamageValue _damageValue;
    [SerializeField] GameObject _bulletHit;

    [SerializeField] GameObject _upgradeOption;

    void Awake()
    {
        instance = this;
    }

    public GameObject getBullet()
    {
        return _bullet;
    }

    public GameObject GetFriendlyMissile()
    {
        return _friendlyMissile;
    }

    public GameObject getBulletHit()
    {
        return _bulletHit;
    }

    public DamageValue getDamageValue()
    {
        return _damageValue;
    }

    public GameObject getUpgradeOption()
    {
        return _upgradeOption;
    }

    public GameObject getGrenade()
    {
        return _grenade;
    }
    public ExplosionEfetivation getGrenadeExplosion()
    {
        return _grenadeExplosion;
    }
    public ExplosionEfetivation getFriendlyMissileExplosion()
    {
        return _friendlyMissileExplosion;
    }
}
