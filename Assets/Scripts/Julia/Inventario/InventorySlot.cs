using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum InteractionTypes
{
    Click,
    Use,
    Equip,
    Delete
}

public class InventorySlot : MonoBehaviour
{
    // Variables
    public static Action<InteractionTypes, int> SlotInteractionEvent;
    
    [SerializeField] private Image itemIcon;
    [SerializeField] private GameObject backgroundQuantity;
    [SerializeField] private TextMeshProUGUI quantityTMP;
    public int Index { get; set; }

    public void UpdateSlotUI(ItemInventory item, int quantity)
    {
        itemIcon.sprite = item.icon;
        quantityTMP.text = quantity.ToString();
    }

    public void ActivateSlotUI(bool state)
    {
        itemIcon.gameObject.SetActive(state);
        backgroundQuantity.gameObject.SetActive(state);
    }

    public void ClickSlot()
    {
        SlotInteractionEvent?.Invoke(InteractionTypes.Click, Index);
    }

    public void UseItemSlot()
    {
        if (Inventory.instance.ItemsInventory[Index] != null)
        {
            SlotInteractionEvent?.Invoke(InteractionTypes.Use, Index);
        }
    }
}
