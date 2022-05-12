using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    // Variables
    [Header("Inventory Description Panel")]
    [SerializeField] private GameObject inventoryDescriptionPanel;
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDescription;

    public static UIInventory instanceUI;
    
    [SerializeField] private InventorySlot slotPrefab;
    [SerializeField] private Transform inventoryContent;
    
    private List<InventorySlot> availableSlots = new List<InventorySlot>();

    public InventorySlot SelectedSlot { get; private set; }

    private void Awake()
    {
        instanceUI = this;
        inventoryDescriptionPanel.gameObject.SetActive(false);
    }

    void Start()
    {
        InitializeInventory();
    }

    private void Update()
    {
        UpdateSelectedSlot();
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

    private void UpdateSelectedSlot()
    {
        GameObject goSeleccionado = EventSystem.current.currentSelectedGameObject;

        if (goSeleccionado == null)
        {
            return;
        }

        InventorySlot slot = goSeleccionado.GetComponent<InventorySlot>();

        if (slot != null)
        {
            SelectedSlot = slot;
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
            itemIcon.sprite = Inventory.instance.ItemsInventory[index].icon;
            itemName.text = Inventory.instance.ItemsInventory[index].name;
            itemDescription.text = Inventory.instance.ItemsInventory[index].description;
            inventoryDescriptionPanel.SetActive(true);
        }

        else
        {
            inventoryDescriptionPanel.SetActive(false);
        }
    }
    
    private void SlotInteractionResponse(InteractionTypes type, int index)
    {
        if (type == InteractionTypes.Click)
        {
            UpdateDescriptionInventory(index);
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
