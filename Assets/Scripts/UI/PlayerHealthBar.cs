using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] Image _healthBarFill;
    [SerializeField] TMP_Text _hpText;
    public void SetHealth(int currHp,int maxHp)
    {
        _healthBarFill.fillAmount = (float) currHp / maxHp;
        _hpText.text = $"HP {currHp} / {maxHp}";
    }
}