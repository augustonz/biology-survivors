using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabLoader : MonoBehaviour
{
    public static PrefabLoader instance;
    [SerializeField] GameObject _bullet;
    [SerializeField] DamageValue _damageValue;
    void Awake() {
        instance=this;
    }

    public GameObject getBullet() {
        return _bullet;
    }

    public DamageValue getDamageValue() {
        return _damageValue;
    }
}
