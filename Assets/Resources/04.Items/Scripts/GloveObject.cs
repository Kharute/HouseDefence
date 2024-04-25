using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New Default Object", menuName = "Inventory System/Items/Glove")]

public class GloveObject : ItemObject
{
    public void Awake()
    {
        type = ItemType.Glove;
    }

}
