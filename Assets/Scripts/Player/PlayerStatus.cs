using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class OnChangeStatEvent : UnityEvent<float> { }
public class PlayerStatus : MonoBehaviour
{
    [SerializeField] private Stats playerStats;

    [Header("Health Settings")]
    [SerializeField] public float currHP;

    [Header("Level Settings")]
    [SerializeField] public int currLevel = 1;
    [SerializeField] public float currExp;
    [SerializeField] public float expCap;

    [Header("Invincibility")]
    [SerializeField] bool  _isInvincible;
    [SerializeField] int  _invincibilityTimeMillis;
    public int  InvincibilityTimeMillis { get => _invincibilityTimeMillis; }

    public UnityEvent OnChangeCurrExp, OnChangeCurrLevel;
    
    public UnityEvent OnHeal;
    public UnityEvent OnGetDamaged;
    public UnityEvent OnDeath;


    public OnChangeStatEvent OnChangeMaxHealth = new();

    public OnChangeStatEvent OnChangeSpeed;


    private Dictionary<TypeStats, OnChangeStatEvent> events = new();

    void Awake()
    {
        playerStats.Awake();
        InitEventDict();
        CalculateExpCap();
    }

    void Start()
    {
        currHP = (int)playerStats.GetStat(TypeStats.MAX_HP);
        OnChangeMaxHealth.AddListener((maxHp) => Heal(maxHp));
    }

    void Update()
    {
        Heal(GetStat(TypeStats.REGEN_HP) * Time.deltaTime);
        AddExp(GetStat(TypeStats.PASSIVE_DNA) * Time.deltaTime);
    }

    void CalculateExpCap()
    {
        expCap += (currLevel - 1 + 100 * Mathf.Pow(2, (currLevel - 1) / 7)) / 4;
    }

    public void AddExp(float exp)
    {
        exp *= GetStat(TypeStats.BONUS_DNA);
        if (currExp + exp > expCap)
        {
            currExp = 0;
            currLevel++;
            CalculateExpCap();
            OnChangeCurrLevel?.Invoke();
            OnChangeCurrExp?.Invoke();
            return;
        }
        currExp += exp;
        OnChangeCurrExp?.Invoke();
    }

    public void GetHit(int damage)
    {
        if (_isInvincible) return;

        _isInvincible = true;

        StopInvincibility();
        
        if (currHP - damage <= 0)
        {
            currHP = 0;
            OnDeath?.Invoke();
            return;
        }
        currHP -= damage;
        OnGetDamaged?.Invoke();
    }

    async void StopInvincibility() {
        await Task.Delay(_invincibilityTimeMillis);
        _isInvincible=false;
    }

    public void Heal(float healing)
    {
        if (currHP + healing > (int)playerStats.GetStat(TypeStats.MAX_HP))
        {
            currHP = (int)playerStats.GetStat(TypeStats.MAX_HP);
            return;
        }
        currHP += healing;
        OnHeal?.Invoke();
    }

    public void AddUpgrade(Upgrade upgrade)
    {
        playerStats.AddStat(upgrade);
        for (int i = 0; i < upgrade.statInfo.Count(); i++)
        {
            if (events.TryGetValue(upgrade.GetInfo(i).stat, out OnChangeStatEvent ev))
            {
                ev?.Invoke(playerStats.GetStat(upgrade.GetInfo(i).stat));
            }
        }
    }

    public float GetStat(TypeStats stat)
    {
        return playerStats.GetStat(stat);
    }

    private void InitEventDict()
    {
        events.Add(TypeStats.SPEED, OnChangeSpeed);
        events.Add(TypeStats.MAX_HP, OnChangeMaxHealth);
    }
}
