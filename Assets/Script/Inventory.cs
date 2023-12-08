using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int maxSlots = 8;
    public Transform weapon, armor, accessory1, accessory2, ring, consumable;
    public List<Loot> slots;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool isFull()
    {
        if (slots.Count >= maxSlots) return true;
        return false;
    }

    public Transform CreateAsset(GameObject asset, string name)
    {
        Transform graphic = transform.GetChild(0);
        Transform clone = Instantiate(asset.transform, graphic);
        clone.name = name;

        return clone;
    }

    public void DestroyAsset(GameObject asset)
    {
        Destroy(asset.gameObject);
    }

    public void Equip(Loot loot)
    {
        EquipmentType type = loot._type;
        GameObject @object = loot._object;
        Transform createdAsset = CreateAsset(@object, @object.name);

        switch (type)
        {
            case EquipmentType.Weapon:
                if (weapon != null)
                {
                    DestroyAsset(weapon.gameObject);
                }

                weapon = createdAsset;
                break;
            case EquipmentType.Armor:
                if (armor != null)
                {
                    DestroyAsset(armor.gameObject);
                }

                armor = createdAsset;
                break;
            case EquipmentType.Accessory:
                if (accessory1 == null)
                {
                    accessory1 = createdAsset;
                    break;
                }
                if(accessory2 == null)
                {
                    accessory2 = createdAsset;
                    break;
                }

                break;
            case EquipmentType.Ring:
                if (ring != null)
                {
                    DestroyAsset(ring.gameObject);
                }

                ring = createdAsset;
                break;
            case EquipmentType.Consumable:
                if (consumable != null)
                {
                    DestroyAsset(consumable.gameObject);
                }

                consumable = createdAsset;
                break;
        }
    }
}
