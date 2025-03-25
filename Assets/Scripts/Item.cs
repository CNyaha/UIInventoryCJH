using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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

[System.Serializable]
public class ItemEquipData
{
    public ItemEquipType type;
    public bool isEquip = false;
}

public class Item
{
    [Header("Item Info")]
    public string itemName;
    public string itemDescription;
    public Sprite icon;
    public ItemType type;

    [Header("Setting")]
    public bool canStack;
    public int maxStackAmount;

    [Header("EquipType")]
    public ItemEquipData itemEquipData;

}
