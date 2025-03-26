using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType
{
    Consumable,
    Equipable,
}

public enum ItemEquipType
{
    Head,
    Chest,
    Leg,
    Boots,
    Gloves,
    Weapon
}

public enum ConsumableType
{
    Health,
}

[System.Serializable]
public class ItemEquipData
{
    public ItemEquipType type;
    public bool isEquip = false;
    [Header("Shop")]
    public int buyPrice;
    public int sellPrice;
    [Header("Item Stat")]
    public float damage;
    public float Defence;
    public float MaxHP;
    public int Strength;
    public int Agillity;
    public int Intelligence;
    public float CriticalChance;
    public float CiriticalDamage;
}

[System.Serializable]
public class ItemConsumable
{
    public ConsumableType type;
    public float value;
}

[CreateAssetMenu(fileName = "NewItem", menuName = "InventoryItem")]
public class Item : ScriptableObject
{
    [Header("Item Info")]
    public string itemName;
    public string itemDescription;
    public Sprite icon;
    public ItemType type;

    [Header("Setting")]
    public bool canStack;
    public int maxStackAmount;

    [Header("Consumable")]
    public ItemConsumable[] consumables;

    [Header("EquipType")]
    public ItemEquipData itemEquipData;

    public Item(string itemName, string itemDescription, Sprite icon, ItemType type, bool canStack, int maxStackAmount, 
        ItemConsumable[] consumables, ItemEquipData itemEquipData)
    {
        this.itemName = itemName;
        this.itemDescription = itemDescription;
        this.icon = icon;
        this.type = type;
        this.canStack = canStack;
        this.maxStackAmount = maxStackAmount;
        this.consumables = consumables;
        this.itemEquipData = itemEquipData;
    }
}
