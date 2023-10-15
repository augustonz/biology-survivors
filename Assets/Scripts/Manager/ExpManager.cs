using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ExpManager : MonoBehaviour
{
    [SerializeField] Transform _expsParent;
    [SerializeField] ExpPoint _exp;

    public static ExpManager instance;

    void Awake() {
        if (instance != null && instance != this) Destroy(this);
        else instance = this;
    }

    public void SpawnExp(Vector3 position, int expValue) {
        Instantiate(_exp,position,Quaternion.identity,_expsParent).value = expValue;
    }
}

