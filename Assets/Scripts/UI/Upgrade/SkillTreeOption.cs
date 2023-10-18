using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeOption : MonoBehaviour
{
    [SerializeField] private Button upgradeButton;
    [SerializeField] private Image upgradeIcon;
    [SerializeField] private Image upgradeBorder;


    private Upgrade _upgrade;

    private int _id;
    private bool isSelected = false;

    private Sprite defaultIcon;

    public void SetUpgrade(Upgrade upgrade)
    {
        _upgrade = upgrade;
        if (_upgrade.icon != null)
        {
            upgradeIcon.sprite = _upgrade.icon;
        }
    }

    public void Reset()
    {
        _upgrade = null;
        upgradeIcon.sprite = defaultIcon;
    }

    public void OnFocus()
    {
        upgradeBorder.color = Color.cyan;
        // ButtonsAudioManager.instance.CallAudio(0);
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
        defaultIcon = upgradeIcon.sprite;
        upgradeButton.onClick.AddListener(delegate { UpgradeScreen.instance.SetSkillSelected(_id); });
    }

    public string GetName()
    {
        if (_upgrade == null)
        {
            return "[WIP] Placeholder";
        }
        return _upgrade.name;
    }

    public string GetDescription()
    {
        if (_upgrade == null)
        {
            return "In Development";
        }
        return _upgrade.description;
    }

    public Upgrade GetUpgrade()
    {
        return _upgrade;
    }
}
