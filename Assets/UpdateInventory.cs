using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateInventory : MonoBehaviour
{
    public InventoryObject storage;
    public InventoryObject inventory;

    public void ItemUpdate()
    {
        // 여기부터 해야함.20240417

        foreach (InventorySlot item in inventory.Container)
        {
            storage.AddItem(item.item, item.amount);
        }
    }

}
