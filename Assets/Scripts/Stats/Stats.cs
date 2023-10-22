using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField] private float reloadSpeed;
    [SerializeField] private int maxAmmo;
    [SerializeField] private int penetration;
    [SerializeField] private int numberOfShots;
    [SerializeField] private float bulletSize;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletRange;
    [SerializeField] private float bulletSpread;
    [SerializeField] private float bulletExplosionChance;

    [SerializeField] private float bulletKnockBack;

    [Header("Missile Settings")]
    [SerializeField] private float missileCount;
    [SerializeField] private float missileCooldown;

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
        for (int i = 0; i < upgrade.statInfo.Count(); i++)
        {
            if (_baseStats.TryGetValue(upgrade.GetInfo(i).stat, out float value) && _statMultiplier.TryGetValue(upgrade.GetInfo(i).stat, out float mult))
            {
                switch (upgrade.GetInfo(i).mod)
                {
                    case TypeModifier.ADD:
                        _baseStats[upgrade.GetInfo(i).stat] += upgrade.GetInfo(i).value;
                        break;
                    case TypeModifier.MULT:
                        _statMultiplier[upgrade.GetInfo(i).stat] += upgrade.GetInfo(i).value;
                        break;
                }
            }
            else
            {
                Debug.LogError($"No stat value found for {upgrade.GetInfo(i).stat} on {this.name}");
                return;
            }
        }
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
        _baseStats.Add(TypeStats.MAX_AMMO, maxAmmo);
        _baseStats.Add(TypeStats.BULLET_SIZE, bulletSize);
        _baseStats.Add(TypeStats.PENETRATION, penetration);
        _baseStats.Add(TypeStats.PASSIVE_DNA, passiveExp);
        _baseStats.Add(TypeStats.SPEED, speed);
        _baseStats.Add(TypeStats.BONUS_DNA, expBonus);
        _baseStats.Add(TypeStats.PICK_UP_RANGE, pickUpRange);
        _baseStats.Add(TypeStats.RELOAD_SPEED, reloadSpeed);
        _baseStats.Add(TypeStats.BULLET_SPEED, bulletSpeed);
        _baseStats.Add(TypeStats.EXPLOSION_CHANCE, bulletExplosionChance);
        _baseStats.Add(TypeStats.BULLET_SPREAD, bulletSpread);
        _baseStats.Add(TypeStats.BULLET_RANGE, bulletRange);
        _baseStats.Add(TypeStats.MISSILE_COOLDOWN, missileCooldown);
        _baseStats.Add(TypeStats.MISSILE_COUNT, missileCooldown);
        _baseStats.Add(TypeStats.BULLET_KNOCKBACK, bulletKnockBack);


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
        _statMultiplier.Add(TypeStats.BULLET_SIZE, 1);
        _statMultiplier.Add(TypeStats.BULLET_SPEED, 1);
        _statMultiplier.Add(TypeStats.BULLET_SPREAD, 1);
        _statMultiplier.Add(TypeStats.BULLET_RANGE, 1);
        _statMultiplier.Add(TypeStats.MISSILE_COOLDOWN, 1);
        _statMultiplier.Add(TypeStats.MISSILE_COUNT, 1);
        _statMultiplier.Add(TypeStats.MAX_AMMO, 1);
        _statMultiplier.Add(TypeStats.BULLET_KNOCKBACK, 1);
    }
}
