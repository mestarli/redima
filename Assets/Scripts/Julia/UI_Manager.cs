using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Manager : MonoBehaviour
{
    // Variables
    
    [Header("PANELS")]
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject timeInfoPanel;

    [Header("Status Panel")]
    [SerializeField] private Image equippedItemSlot;
    private void Awake()
    {
        inventoryPanel.SetActive(false);
    }

    #region Panels

    public void OpenCloseInventoryPanel()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
    }

    #endregion
}
