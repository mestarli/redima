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

    private Button slotButton;
    public int Index { get; set; }

    private void Awake()
    {
        slotButton = GetComponent<Button>();
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

    public void UnequipSlot()
    {
        HandSlotInteractionEvent?.Invoke(HandInteractionTypes.Unequip, Index);
    }
}
