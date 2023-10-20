using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] Image _healthBarFill;
    [SerializeField] TMP_Text _hpText;
    PlayerStatus _playerStatus;

    void Start() {
        _playerStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
    }

    public void SetHealth(int currHp,int maxHp)
    {
        _healthBarFill.fillAmount = (float) currHp / maxHp;
        _hpText.text = $"HP {currHp} / {maxHp}";
    }

    public void UpdateHealth() {
        SetHealth((int)_playerStatus.currHP,(int)_playerStatus.GetStat(TypeStats.MAX_HP));
    }
}