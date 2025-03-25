using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerClass
{
    내배캠수강생,

}

[System.Serializable]
public class Character 
{
    
    public string PlayerName { get; private set; }
    public int Level { get; private set; } = 1;
    public float MaxHp {  get; private set; }
    public float CurrentHP { get; private set; }
    public int Strength { get; private set; }
    public int Agility { get; private set; }
    public int Intelligence { get; private set; }
    public float CriticalChance { get; private set; }
    public float CriticalDamage {  get; private set; }



    public Character(string playerName, float maxHp, int strength, int agility, int interlligence)
    {
        PlayerName = playerName;
        MaxHp = maxHp;
        CurrentHP = maxHp;
        Strength = strength;
        Agility = agility;
        Intelligence = interlligence;
        CriticalChance = 10;
        CriticalDamage = 150;
    }


}
