using GameClient.Constructor;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData: MonoBehaviour
{
    public GameObject[] items;

    public void UpdateInventory(Dictionary<int,Inventory> InventoryDict)
    {
        foreach(var inv in InventoryDict.Values)
        {
            items[inv.IDitem - 1].SetActive(true);
        }
    }


}
