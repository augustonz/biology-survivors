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
    MAX_AMMO,
    PENETRATION,
    BULLET_SIZE,
    BULLET_SPEED,
    BULLET_RANGE,
    RELOAD_SPEED,
    ARMOR,
    SPEED,
    BONUS_DNA,
    PASSIVE_DNA,
    PICK_UP_RANGE,
    MISSILE_UNLOCKED,
    MISSILE_COOLDOWN,

}

[System.Serializable]
public enum TypeModifier
{
    ADD,
    MULT,
}
