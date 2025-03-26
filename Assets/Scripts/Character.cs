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

    public bool isWeaponEquip { get; private set; } = false;
    public int weaponIndex { get; private set; } = -1;
    public bool isHelmetEquip { get; private set; } = false;
    public int helmetIndex { get; private set; } = -1;



    public Character( PlayerClass userClass,string playerName, float maxHp, int strength, int agility, int intelligence, float damage, float defence)
    {
        UserClass = userClass;
        PlayerName = playerName;
        MaxHp = maxHp;
        CurrentHP = maxHp;
        Strength = strength;
        Agility = agility;
        Intelligence = intelligence;
        Damage = damage;
        Defence = defence;
        CriticalChance = 10;
        CriticalDamage = 150;
    }

    public void AddItemStatus(Item item)
    {
        this.MaxHp += item.itemEquipData.MaxHP;
        this.Strength += item.itemEquipData.Strength;
        this.Agility += item.itemEquipData.Agillity;
        this.Intelligence += item.itemEquipData.Intelligence;
        this.Damage += item.itemEquipData.damage;
        this.Defence += item.itemEquipData.Defence;
        UIManager.Instance.status.SetCharacterInfo(this);
    }

    public void SubItemStatus(Item item)
    {
        this.MaxHp -= item.itemEquipData.MaxHP;
        this.Strength -= item.itemEquipData.Strength;
        this.Agility -= item.itemEquipData.Agillity;
        this.Intelligence -= item.itemEquipData.Intelligence;
        this.Damage -= item.itemEquipData.damage;
        this.Defence -= item.itemEquipData.Defence;
        UIManager.Instance.status.SetCharacterInfo(this);
    }

    public void Equip(Slot slot)
    {
        if (slot == null)
        {
            Debug.LogError("Equip() Slot이 null입니다");
            return;
        }

        switch (slot.item.itemEquipData.type)
        {
            case ItemEquipType.Weapon:
                if (isWeaponEquip)
                {
                    UnEquip(UIManager.Instance.inventory.slots[weaponIndex]);
                }
                isWeaponEquip = true;
                weaponIndex = slot.index;
                AddItemStatus(slot.item);
                break;

            case ItemEquipType.Head:
                if (isHelmetEquip)
                {
                    UnEquip(UIManager.Instance.inventory.slots[helmetIndex]);
                }
                isHelmetEquip = true;
                weaponIndex = slot.index;
                break;
        }
    }

    public void UnEquip(Slot slot)
    {
        switch (slot.item.itemEquipData.type)
        {
            case ItemEquipType.Weapon:
                weaponIndex = -1;
                isWeaponEquip = false;
                SubItemStatus(slot.item);
                break;

            case ItemEquipType.Head:
                helmetIndex = -1;
                isHelmetEquip = false;
                SubItemStatus(slot.item);
                break;
             
        }

        slot.equipped = false;
        slot.equipImage.gameObject.SetActive(false);
    }

}
