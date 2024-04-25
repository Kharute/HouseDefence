using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New Default Object", menuName = "Inventory System/Items/Weapon")]

public class WeaponObject : ItemObject
{
    public void Awake()
    {
        type = ItemType.Weapon;
    }

}
