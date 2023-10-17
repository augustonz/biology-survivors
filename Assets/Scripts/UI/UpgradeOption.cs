using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeOption : MonoBehaviour
{
    [SerializeField] private Button upgradeButton;
    [SerializeField] private Image upgradeIcon;
    [SerializeField] private Image upgradeBorder;


    private Upgrade _upgrade;

    private int _id;
    private bool isSelected = false;


    public void SetUpgrade(Upgrade upgrade)
    {
        _upgrade = upgrade;
        if (_upgrade.icon != null)
        {
            upgradeIcon.sprite = _upgrade.icon;
        }
    }

    public void OnFocus()
    {
        upgradeBorder.color = Color.cyan;
        isSelected = true;
    }

    public void OnUnfocus()
    {
        upgradeBorder.color = Color.white;
        isSelected = false;
    }

    public void SetId(int id)
    {
        _id = id;
        upgradeButton.onClick.AddListener(delegate { UpgradeScreen.instance.SetSelected(_id); });
    }

    public string GetName()
    {
        return _upgrade.name;
    }

    public string GetDescription()
    {
        return _upgrade.description;
    }

    public Upgrade GetUpgrade()
    {
        return _upgrade;
    }
}
