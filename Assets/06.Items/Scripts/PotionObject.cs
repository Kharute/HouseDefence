using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Default Object", menuName = "Inventory System/Items/Potion")]

public class PotionObject : ItemObject
{
    public float restoreHPvalue;
    private void Awake()
    {
        type = ItemType.Potions;
    }
}
