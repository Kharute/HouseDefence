using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New Default Object", menuName = "Inventory System/Items/Equipment")]

public class EquipmentObject : ItemObject
{
    public float atkBonus;
    public float defBonus;
    public float spdBonus;
    public void Awake()
    {
        type = ItemType.Equip;
    }

}
