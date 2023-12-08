using System;
using UnityEngine;

[Serializable]
public enum EquipmentType
{
    Weapon,
    Armor,
    Accessory,
    Ring,
    Consumable
}
[Serializable]
public enum Rarity
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary
}

[Serializable]
public class Loot
{
    public Loot(Rarity rarity, EquipmentType type, GameObject @object)
    {
        _rarity = rarity;
        _type = type;
        _object = @object;
    }

    public GameObject _object;
    public Rarity _rarity;
    public EquipmentType _type;
    
}