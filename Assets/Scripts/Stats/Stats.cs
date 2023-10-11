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
    [SerializeField] private int power;
    [SerializeField] private int fireRate;
    [SerializeField] private int penetration;
    [SerializeField] private int numberOfShots;

    [Header("Defense Settings")]
    [SerializeField] private float armor;
    [SerializeField] private float speed;

    [Header("Level Settings")]
    [SerializeField] private float expBonus;
    [SerializeField] private float passiveExp;

    public Dictionary<TypeStats, float> stats = new();


    public void Awake()
    {
        InitDict();
    }



    public float GetStat(TypeStats stat)
    {
        if (stats.TryGetValue(stat, out float value))
        {
            return value;
        }
        else
        {
            Debug.LogError($"No stat value found for {stat} on {this.name}");
            return 0;
        }
    }

    public float AddStat(Upgrade upgrade)
    {
        if (stats.TryGetValue(upgrade.stat, out float value))
        {
            switch (upgrade.mod)
            {
                case TypeModifier.ADD:
                    stats[upgrade.stat] += upgrade.value;
                    break;
                case TypeModifier.MULT:
                    stats[upgrade.stat] *= upgrade.value;
                    break;
            }
        }
        {
            Debug.LogError($"No stat value found for {upgrade.stat} on {this.name}");
            return 0;
        }
    }

    private void InitDict()
    {
        stats.Add(TypeStats.MAX_HP, maxHP);
        stats.Add(TypeStats.REGEN_HP, regenHP);
        stats.Add(TypeStats.POWER, power);
        stats.Add(TypeStats.FIRE_RATE, fireRate);
        stats.Add(TypeStats.ARMOR, armor);
        stats.Add(TypeStats.NUMBER_OF_SHOTS, numberOfShots);
        stats.Add(TypeStats.PENETRATION, penetration);
        stats.Add(TypeStats.PASSIVE_DNA, passiveExp);
        stats.Add(TypeStats.SPEED, speed);
        stats.Add(TypeStats.BONUS_DNA, expBonus);
    }
}
