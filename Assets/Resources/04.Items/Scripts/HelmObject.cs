using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New Default Object", menuName = "Inventory System/Items/Helm")]

public class HelmObject : ItemObject
{
    public void Awake()
    {
        type = ItemType.Helmet;
    }

}
