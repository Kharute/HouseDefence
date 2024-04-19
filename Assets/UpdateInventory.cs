using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateInventory : MonoBehaviour
{
    public InventoryObject storage;
    public InventoryObject inventory;

    public void ItemUpdate()
    {
        storage.Container = inventory.Container;
        inventory.Save();
    }

}
