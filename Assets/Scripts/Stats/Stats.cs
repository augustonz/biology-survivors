using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "Stats", menuName = "ScriptableObjects/Stats")]
public class Stats : ScriptableObject
{
    [Header("Health Settings")]
    [SerializeField] private int maxHP;
    [SerializeField] private float regenHP;

    [Header("Damage Settings")]
    [SerializeField] private float power;
    [SerializeField] private float fireRate;
    [SerializeField] private int penetration;
    [SerializeField] private int numberOfShots;
    [SerializeField] private float reloadSpeed;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletRange;

    [Header("Defense Settings")]
    [SerializeField] private float armor;
    [SerializeField] private float speed;

    [Header("Level Settings")]
    [SerializeField] private float expBonus;
    [SerializeField] private float passiveExp;

    [Header("Other Settings")]
    [SerializeField] private float pickUpRange;

    public Dictionary<TypeStats, float> _baseStats = new();
    public Dictionary<TypeStats, float> _statMultiplier = new();

    public void Awake()
    {
        InitDicts();
    }

    public float GetStat(TypeStats stat)
    {
        if (_statMultiplier.TryGetValue(stat, out float mult))
        {
            if (_baseStats.TryGetValue(stat, out float value))
            {
                return value * mult;
            }
        }
        Debug.LogError($"No stat value found for {stat} on {name}");
        return 0;

    }

    public void AddStat(Upgrade upgrade)
    {
        if (_baseStats.TryGetValue(upgrade.stat, out float value) && _statMultiplier.TryGetValue(upgrade.stat, out float mult))
        {
            switch (upgrade.mod)
            {
                case TypeModifier.ADD:
                    _baseStats[upgrade.stat] += upgrade.value;
                    break;
                case TypeModifier.MULT:
                    _statMultiplier[upgrade.stat] += upgrade.value;
                    break;
            }
            return;
        }
        Debug.LogError($"No stat value found for {upgrade.stat} on {this.name}");
    }

    private void InitDicts()
    {
        if (_baseStats.Count != 0) return;
        _baseStats.Add(TypeStats.MAX_HP, maxHP);
        _baseStats.Add(TypeStats.REGEN_HP, regenHP);
        _baseStats.Add(TypeStats.POWER, power);
        _baseStats.Add(TypeStats.FIRE_RATE, fireRate);
        _baseStats.Add(TypeStats.ARMOR, armor);
        _baseStats.Add(TypeStats.NUMBER_OF_SHOTS, numberOfShots);
        _baseStats.Add(TypeStats.PENETRATION, penetration);
        _baseStats.Add(TypeStats.PASSIVE_DNA, passiveExp);
        _baseStats.Add(TypeStats.SPEED, speed);
        _baseStats.Add(TypeStats.BONUS_DNA, expBonus);
        _baseStats.Add(TypeStats.PICK_UP_RANGE, pickUpRange);
        _baseStats.Add(TypeStats.RELOAD_SPEED, reloadSpeed);
        _baseStats.Add(TypeStats.BULLET_SPEED, bulletSpeed);
        _baseStats.Add(TypeStats.BULLET_RANGE, bulletRange);

        _statMultiplier.Add(TypeStats.MAX_HP, 1);
        _statMultiplier.Add(TypeStats.REGEN_HP, 1);
        _statMultiplier.Add(TypeStats.POWER, 1);
        _statMultiplier.Add(TypeStats.FIRE_RATE, 1);
        _statMultiplier.Add(TypeStats.ARMOR, 1);
        _statMultiplier.Add(TypeStats.NUMBER_OF_SHOTS, 1);
        _statMultiplier.Add(TypeStats.PENETRATION, 1);
        _statMultiplier.Add(TypeStats.PASSIVE_DNA, 1);
        _statMultiplier.Add(TypeStats.SPEED, 1);
        _statMultiplier.Add(TypeStats.BONUS_DNA, 1);
        _statMultiplier.Add(TypeStats.PICK_UP_RANGE, 1);
        _statMultiplier.Add(TypeStats.RELOAD_SPEED, 1);
        _statMultiplier.Add(TypeStats.BULLET_SPEED, 1);
        _statMultiplier.Add(TypeStats.BULLET_RANGE, 1);
    }
}
