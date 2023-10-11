using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] private Stats playerStats;

    [Header("Health Settings")]
    [SerializeField] private int maxHP;
    [SerializeField] private float currHP;
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
    [SerializeField] private int currLevel = 1;
    [SerializeField] private float currExp;
    [SerializeField] private float expCap;
    [SerializeField] private float expBonus;
    [SerializeField] private float passiveExp;




    public int MaxHP { get => maxHP; }
    public float CurrHP { get => currHP; }

    public int CurrLevel { get => currLevel; }
    public float CurrExp { get => currExp; }
    public float ExpCap { get => expCap; }

    private bool isDead;

    public UnityEvent OnChangeCurrExp, OnChangeCurrLevel;

    public UnityEvent OnChangeCurrHealth;

    public UnityEvent OnChangeMaxHealth;

    public UnityEvent OnChangeSpeed;

    public Dictionary<TypeStats, float> dict = new Dictionary<TypeStats, float>();

    void Awake()
    {
        maxHP = playerStats.MaxHp;
        regenHP = playerStats.RegenHP;
        speed = playerStats.Speed;

        currHP = maxHP;
        CalculateExpCap();
    }

    void Update()
    {
        Heal(regenHP * Time.deltaTime);
    }

    public void AddUpgrade(Upgrade upgrade)
    {
        var res;
        dict.TryGetValue(upgrade.stat, res);
        switch (upgrade.mod)
        {

            case TypeModifier.ADD:
                break;
            case TypeModifier.MULT:
                break;
        }
    }

    void CalculateExpCap()
    {
        expCap += (currLevel - 1 + 100 * Mathf.Pow(2, (currLevel - 1) / 7)) / 4;
        Debug.Log(expCap);
    }

    public void AddExp(float exp)
    {
        if (currExp + exp > expCap)
        {
            currExp = 0;
            currLevel++;
            CalculateExpCap();
            OnChangeCurrLevel?.Invoke();
            return;
        }
        currExp += exp;
    }

    public void GetHit(int damage)
    {
        if (currHP - damage <= 0)
        {
            isDead = true;
            Destroy(gameObject);
            return;
        }
        currHP -= damage;
    }

    public void Heal(float healing)
    {
        if (currHP + healing > maxHP)
        {
            currHP = maxHP;
            return;
        }
        currHP += healing;
    }

    void InitDict()
    {
        dict.Add(TypeStats.MAX_HP, maxHP);
        dict.Add(TypeStats.REGEN_HP, regenHP);

        dict.Add(TypeStats.POWER, power);
        dict.Add(TypeStats.FIRE_RATE, fireRate);
        dict.Add(TypeStats.NUMBER_OF_SHOTS, numberOfShots);
        dict.Add(TypeStats.PENETRATION, penetration);

        dict.Add(TypeStats.ARMOR, armor);
        dict.Add(TypeStats.SPEED, speed);

        dict.Add(TypeStats.BONUS_DNA, expBonus);
        dict.Add(TypeStats.PASSIVE_DNA, passiveExp);


    }

}
