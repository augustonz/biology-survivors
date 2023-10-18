using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillTree
{
    public Upgrade Head;
    public List<Upgrade> Right = new();
    public List<Upgrade> Left = new();

    public List<Upgrade> GetSkillTree()
    {
        List<Upgrade> temp = new() { Head };
        temp.AddRange(Right);
        temp.AddRange(Left);
        return temp;
    }
}