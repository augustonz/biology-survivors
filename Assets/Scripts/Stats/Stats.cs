using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "Stats", menuName = "ScriptableObjects/Stats")]
public class Stats : ScriptableObject
{
    [SerializeField] private int maxHP;
    [SerializeField] private int regenHP;
    [SerializeField] private int speed;

    [SerializeField]
    public SerializedDictionary<TypeStats, float> stats;

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
}
