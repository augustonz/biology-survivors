using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "ScriptableObjects/Stats")]
public class Stats : ScriptableObject
{
    [SerializeField] private int maxHP;
    [SerializeField] private int regenHP;
    [SerializeField] private int speed;



    public int MaxHp { get => maxHP; }
    public float RegenHP { get => regenHP; }
    public float Speed { get => speed; }
}
