using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "ScriptableObjects/UpgradeList")]
public class UpgradeList : ScriptableObject
{

    public List<Upgrade> upgrades = new();
    private List<Upgrade> copy = new();

    public void Init()
    {
        copy = upgrades.ToList();
    }

    public void ChooseUpgrade(string id)
    {
        Upgrade choosedUpgrade;
        for (int i = 0; i < copy.Count; i++)
        {
            if (copy[i].Id == id)
            {
                choosedUpgrade = copy[i];
                copy.RemoveAt(i);
                foreach (var unlockedUpgrade in choosedUpgrade.unlocks)
                {
                    AddToPool(unlockedUpgrade);
                }
            }
        }
    }

    private void AddToPool(Upgrade upgrade)
    {
        copy.Add(upgrade);
    }

    public Upgrade[] GetFromPool()
    {
        HashSet<int> choosedNumbers = new();
        int size = Mathf.Min(4, copy.Count);
        while (choosedNumbers.Count != size)
        {
            choosedNumbers.Add(Random.Range(0, copy.Count));
        }
        return copy.Where((u, index) => choosedNumbers.Contains(index)).ToArray();
    }

}
