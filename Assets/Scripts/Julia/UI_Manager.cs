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
    private List<HandSlot> availableSlot = new List<HandSlot>();
    [SerializeField] private HandSlot slotPrefab;
    [SerializeField] private Transform inventoryContent;
    [SerializeField] private int slotsNum;

    private void Awake()
    {
        inventoryPanel.SetActive(false);
    }

    private void Start()
    {
        InitializeEquippedSlot();
    }

    private void InitializeEquippedSlot()
    {
        for (int i = 0; i < slotsNum; i++)
        {
            HandSlot newSlot = Instantiate(slotPrefab, inventoryContent);
            newSlot.Index = i;
            availableSlot.Add(newSlot);
        }
    }
    
    public void DrawItemInEquippedSlot(ItemInventory itemToAdd, int quantity, int itemIndex)
    {
        HandSlot slot = availableSlot[itemIndex]; 
      
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

    #region Panels

    public void OpenCloseInventoryPanel()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
    }

    #endregion
}
