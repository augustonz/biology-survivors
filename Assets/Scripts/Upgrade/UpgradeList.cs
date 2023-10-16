using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "ScriptableObjects/UpgradeList")]
public class UpgradeList : ScriptableObject
{

    public List<Upgrade> upgrades = new();

}
