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

    // Metodo para mostrar la info del scriptable object
    public void UpdateSlotUI(ItemInventory item, int quantity)
    {
        itemIcon.sprite = item.icon;
        quantityText.text = quantity.ToString();
    }

    // Metodo para activar o desactivar las imagenes en la UI
    public void ActivateSlotUI(bool state)
    {
        itemIcon.gameObject.SetActive(state);
        backgroundQuantity.gameObject.SetActive(state);
    }

    // Metodo si seleccionamos un slot
    public void SelectSlot()
    {
        slotButton.Select();
    }
    
    // Metodo para saber si se le da click a un slot y en que indice
    public void ClickSlot()
    {
        SlotInteractionEvent?.Invoke(InteractionTypes.Click, Index);
    }

    // Metodo para saber si en el slot que se ha seleccionado hay un item y para saber si se va a usar un item y
    // en que indice se encuentra de la lista de int
    public void UseItemSlot()
    {
        if (Inventory.instance.ItemsInventory[Index] != null)
        {
            SlotInteractionEvent?.Invoke(InteractionTypes.Use, Index);
        }
    }

    // Metodo para saber si en el slot que se ha seleccionado hay un item y para saber si se va a equipar un item y
    // en que indice se encuentra de la lista de int
    public void EquipItemSlot()
    {
        if (Inventory.instance.ItemsInventory[Index] != null)
        {
            SlotInteractionEvent?.Invoke(InteractionTypes.Equip, Index);
        }
    }
}
