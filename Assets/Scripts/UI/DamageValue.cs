using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using TMPro;

public class DamageValue : MonoBehaviour
{
    TMP_Text _text;
    Animator _anim;

    void Awake() {
        _text = GetComponentInChildren<TMP_Text>();
        _anim = GetComponent<Animator>();
        Destroy(gameObject,_anim.GetCurrentAnimatorStateInfo(0).length);
    }

    public void SetText(string text) {
        _text.text = text;
    }

    public static void Instantiate(Vector3 initialPos,int amount) {
        DamageValue value = Instantiate<DamageValue>(PrefabLoader.instance.getDamageValue(),initialPos,Quaternion.identity);
        value.SetText(amount.ToString());
    }
}
