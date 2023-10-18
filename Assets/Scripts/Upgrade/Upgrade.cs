using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField] public StatInfo[] statInfo;

    [SerializeField] public Sprite icon;

    [SerializeField] public TypeTier tier;
    [SerializeField] public Upgrade unlockedBy;
    [SerializeField] public List<Upgrade> unlocks;

    [SerializeField] public SkillTree skillTree;

    [SerializeField][TextArea] public string description;

    public StatInfo GetInfo(int index)
    {
        return statInfo[index];
    }

}

[System.Serializable]
public class StatInfo
{
    [SerializeField] public TypeStats stat;
    [SerializeField] public TypeModifier mod;
    [SerializeField] public float value;
}
