using System.Collections;
using System.Collections.Generic;
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
}

[System.Serializable]
public class ItemConsumable
{
    public ConsumableType type;
    public float value;
}

public class Item
{
    [Header("Item Info")]
    public string itemName;
    public string itemDescription;
    public Image icon;
    public ItemType type;

    [Header("Setting")]
    public bool canStack;
    public int maxStackAmount;

    [Header("Consumable")]
    public ItemConsumable[] consumables;

    [Header("EquipType")]
    public ItemEquipData itemEquipData;

}
