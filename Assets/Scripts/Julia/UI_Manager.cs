using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Manager : MonoBehaviour
{
    // Variables
    public static UI_Manager instanceUI;

    [Header("PANELS")]
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject timeInfoPanel;

    [Header("Status Panel")]
    [SerializeField] private Image equippedItemSlot;

    private void Awake()
    {
        inventoryPanel.SetActive(false);
    }
    
    public void UpdateSlotUI(ItemInventory item, int quantity)
    {
        equippedItemSlot.sprite = item.icon;
    }

    public void ActivateSlotUI(bool state)
    {
        equippedItemSlot.gameObject.SetActive(state);
    }
    
    public void DrawItemInEquippedSlot(ItemInventory itemToAdd, int quantity, int itemIndex)
    {

        if (itemToAdd != null)
        {
            ActivateSlotUI(true);
            UpdateSlotUI(itemToAdd, quantity);
        }

        else
        {
            ActivateSlotUI(false);
        }
    }

    #region Panels

    public void OpenCloseInventoryPanel()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
    }

    #endregion
}
