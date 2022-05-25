using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;

public enum HandInteractionTypes
{
    Click,
    Unequip
}

public class HandSlot : MonoBehaviour
{  // Variables
    public static Action<HandInteractionTypes, int> HandSlotInteractionEvent;
    
    [SerializeField] private Image itemIcon;
    [SerializeField] private GameObject backgroundQuantity;
    [SerializeField] private TextMeshProUGUI quantityTMP;
    [SerializeField] private ItemInventory itemInventory_hand;

    private Button slotButton;
    public int Index { get; set; }

    public ItemInventory ItemInventoryHand
    {
        get => itemInventory_hand;
        set => itemInventory_hand = value;
    }

    private void Awake()
    {
        slotButton = GetComponent<Button>();
        Inventory.instance.HandSlot = this;
    }

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

    public void SelectSlot()
    {
        slotButton.Select();
    }
    
    public void ClickSlot()
    {
        HandSlotInteractionEvent?.Invoke(HandInteractionTypes.Click, Index);
    }

    public void Update_Info_item_inventory()
    {
        ActivateSlotUI(true);
        itemIcon.sprite = itemInventory_hand.icon;
        quantityTMP.text = itemInventory_hand.quantity.ToString();
    }

    public void Deactivate_Info_iteminventory()
    {
        ActivateSlotUI(false);
        Update_Info_item_inventory();
    }
    
    public void UnequipItemSlot()
    {
        if (Inventory.instance.ItemsInventory[Index] != null)
        {
            HandSlotInteractionEvent?.Invoke(HandInteractionTypes.Unequip, Index);
        }
        
        UI_Manager.instanceUI.EquipItem();
    }
}
