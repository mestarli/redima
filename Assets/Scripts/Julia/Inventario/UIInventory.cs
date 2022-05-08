using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    // Variables
    [SerializeField] private InventorySlot slotPrefab;
    [SerializeField] private Transform inventoryContent;

    private List<InventorySlot> availableSlots = new List<InventorySlot>();

    void Start()
    {
        InitializeInventory();
    }

    private void InitializeInventory()
    {
        for (int i = 0; i < Inventory.instance.SlotsNum; i++)
        {
            InventorySlot newSlot = Instantiate(slotPrefab, inventoryContent);
            newSlot.Index = i;
            availableSlots.Add(newSlot);
        }
    }
}
