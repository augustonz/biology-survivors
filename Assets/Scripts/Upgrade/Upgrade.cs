using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TypeTier
{
    FIRST,
    SECOND,
    THIRD,
}


[CreateAssetMenu(fileName = "Upgrade", menuName = "ScriptableObjects/Upgrade")]
public class Upgrade : BaseScriptableObject
{
    [SerializeField] public TypeStats stat;
    [SerializeField] public TypeModifier mod;
    [SerializeField] public float value;

    [SerializeField] public Sprite icon;

    [SerializeField] public TypeTier tier;
    [SerializeField] public bool isUnlocked;
    [SerializeField] public List<Upgrade> unlocks;


    [SerializeField][TextArea] public string description;
}
