using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New Default Object", menuName = "Inventory System/Items/Boots")]

public class BootsObject : ItemObject
{
    public void Awake()
    {
        type = ItemType.Boots;
    }

}
