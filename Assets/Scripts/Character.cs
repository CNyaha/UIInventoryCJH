using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerClass
{
    SPARTASTUDENT,

}

[System.Serializable]
public class Character 
{
    
    public string PlayerName { get; private set; }
    public int Level { get; private set; } = 1;
    public int MaxExp {  get; private set; }
    public int CurrentExp {  get; private set; }
    public float MaxHp {  get; private set; }
    public float CurrentHP { get; private set; }
    public int Strength { get; private set; }
    public int Agility { get; private set; }
    public int Intelligence { get; private set; }
    public float Damage { get; private set; }
    public float Defence { get; private set; }

    public float CriticalChance { get; private set; }
    public float CriticalDamage {  get; private set; }

    public int Gold { get; private set; } = 1000;

    public PlayerClass UserClass { get; private set; }



    public Character( PlayerClass userClass,string playerName, float maxHp, int strength, int agility, int interlligence, float damage, float defence)
    {
        UserClass = userClass;
        PlayerName = playerName;
        MaxHp = maxHp;
        CurrentHP = maxHp;
        Strength = strength;
        Agility = agility;
        Intelligence = interlligence;
        Damage = damage;
        Defence = defence;
        CriticalChance = 10;
        CriticalDamage = 150;
    }


}
