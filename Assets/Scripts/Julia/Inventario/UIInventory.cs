using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    // Variables
    [Header("Inventory Description Panel")]
    [SerializeField] private GameObject InventoryDescriptionPanel;
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDescription;

    public static UIInventory instanceUI;
    
    [SerializeField] private InventorySlot slotPrefab;
    [SerializeField] private Transform inventoryContent;

    private List<InventorySlot> availableSlots = new List<InventorySlot>();

    private void Awake()
    {
        instanceUI = this;
    }

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

    public void DrawItemInInventory(ItemInventory itemToAdd, int quantity, int itemIndex)
    {
        InventorySlot slot = availableSlots[itemIndex];

        if (itemToAdd != null)
        {
            slot.ActivateSlotUI(true);
            slot.UpdateSlotUI(itemToAdd, quantity);
        }

        else
        {
            slot.ActivateSlotUI(false);
        }
    }

    private void UpdateDescriptionInventory(int index)
    {
        if (Inventory.instance.ItemsInventory[index] != null)
        {
            
        }
    }
    
    private void SlotInteractionResponse(InteractionTypes type, int index)
    {
        if (type == InteractionTypes.Click)
        {
            
        }
    }
    
    public void OnEnable()
    {
        InventorySlot.SlotInteractionEvent += SlotInteractionResponse;
    }

    public void OnDisable()
    {
        InventorySlot.SlotInteractionEvent -= SlotInteractionResponse;

    }
}
