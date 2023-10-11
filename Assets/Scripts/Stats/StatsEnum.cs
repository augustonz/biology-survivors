using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum TypeStats
{
    MAX_HP,
    REGEN_HP,
    POWER,
    FIRE_RATE,
    NUMBER_OF_SHOTS,
    PENETRATION,
    ARMOR,
    SPEED,
    BONUS_DNA,
    PASSIVE_DNA,
}

[System.Serializable]
public enum TypeModifier
{
    ADD,
    MULT,
}
