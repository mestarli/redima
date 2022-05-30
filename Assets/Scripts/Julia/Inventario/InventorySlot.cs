using System;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private Text quantityText;

    private Button slotButton;
    public int Index { get; set; }

    private void Awake()
    {
        slotButton = GetComponent<Button>();
    }

    public void UpdateSlotUI(ItemInventory item, int quantity)
    {
        itemIcon.sprite = item.icon;
        quantityText.text = quantity.ToString();
    }

    public void ActivateSlotUI(bool state)
    {
        itemIcon.gameObject.SetActive(state);
        backgroundQuantity.gameObject.SetActive(state);
    }

    public void SelectSlot()
    {
        slotButton.Select();
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

    public void EquipItemSlot()
    {
        if (Inventory.instance.ItemsInventory[Index] != null)
        {
            SlotInteractionEvent?.Invoke(InteractionTypes.Equip, Index);
        }
    }
}
