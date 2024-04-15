using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Default Object", menuName = "Inventory System/Items/Use")]
public class UseObject : ItemObject
{

    private void Awake()
    {
        type = ItemType.Use;
    }
}
