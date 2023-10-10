using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] private Stats playerStats;

    [SerializeField] private int maxHP;
    [SerializeField] private float currHP;
    [SerializeField] private float regenHP;

    [SerializeField] private float speed;

    [SerializeField] private int currLevel = 1;

    [SerializeField] private int currExp = 0;
    [SerializeField] private int expCap;

    private bool isDead;

    public UnityEvent OnChangeCurrExp, OnChangeCurrLevel;

    public UnityEvent OnChangeCurrHealth;

    public UnityEvent OnChangeMaxHealth;

    public UnityEvent OnChangeSpeed;

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

    void CalculateExpCap()
    {
        expCap = expCap + (int)(currLevel - 1 + 300 * Mathf.Pow(2, (currLevel - 1) / 7)) / 4;
    }

    public void AddExp(int exp)
    {
        if (currExp + exp > expCap)
        {
            currExp = 0;
            currLevel++;
            int oldExpCap = expCap;
            CalculateExpCap();
            AddExp(currExp + exp - oldExpCap);
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

}
