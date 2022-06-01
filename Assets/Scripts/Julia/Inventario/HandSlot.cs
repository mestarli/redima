using System;
using UnityEngine;
using UnityEngine.UI;

public enum HandInteractionTypes
{
    Click,
    Unequip
}

public class HandSlot : MonoBehaviour
{  
    // Variables
    public static HandSlot instanceHandSlot;
    
    [SerializeField] private Image itemIcon;
    [SerializeField] private GameObject backgroundQuantity;
    [SerializeField] private Text quantityText;
    [SerializeField] private ItemInventory itemInventory_hand;

    public int Index { get; set; }
    
    public ItemInventory ItemInventoryHand
    {
        get => itemInventory_hand;
        set => itemInventory_hand = value;
    }

    private void Awake()
    {
        Inventory.instance.HandSlot = this;
        instanceHandSlot = this;
    }

    // Metodo para activar y desactivar las imagenes que hay dentro del slot
    public void ActivateSlotUI(bool state)
    {
        itemIcon.gameObject.SetActive(state);
        backgroundQuantity.gameObject.SetActive(state);
    }

    // Método para actualizar la info de lo que tenemos equipado
    public void Update_Info_item_inventory()
    {
        ActivateSlotUI(true);
        itemIcon.sprite = itemInventory_hand.icon;
        quantityText.text = itemInventory_hand.quantity.ToString();
    }
    
    // Método para desequipar objetos
    public void UnequipItemSlot()
    {
        if (itemInventory_hand != null)
        {
            Inventory.instance.AddItem(itemInventory_hand, itemInventory_hand.quantity);
            itemInventory_hand = null; 
            ActivateSlotUI(false);
            Inventory.instance.isEquipped = false;
        }
    }
}
