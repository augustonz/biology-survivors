using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "ScriptableObjects/Upgrade")]
public class Upgrade : ScriptableObject
{
    [SerializeField] public TypeStats stat;
    [SerializeField] public TypeModifier mod;
    [SerializeField] public float value;

}
