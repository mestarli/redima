using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.XR;

public class UI_Manager : MonoBehaviour, ITimeTracker
{
    // Variables
    public static UI_Manager instanceUI;

    [Header("PANELS")]
    [SerializeField] private GameObject inventoryPanel;

    [Header("Status Panel")]
    private List<HandSlot> availableSlot = new List<HandSlot>();
    [SerializeField] private HandSlot slotPrefab;
    [SerializeField] private Transform handContent;
    [SerializeField] private int slotsNum;
    public HandSlot SelectedSlot { get; private set; }

    [Header("Time Info Panel")]
    public Text timeText;
    public Text dateText;
    
    private void Awake()
    {
        inventoryPanel.SetActive(false);
    }

    private void Start()
    {
        InitializeEquippedSlot();
        
        // Añadir el UI_Manager a la lista de objetos del TimeManager notificará cuando el time se actualice
        TimeManager.instance.RegisterTracker(this);
    }

    private void InitializeEquippedSlot()
    {
        for (int i = 0; i < slotsNum; i++)
        {
            HandSlot newSlot = Instantiate(slotPrefab, handContent);
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

    public void EquipItem()
    {
        if (SelectedSlot != null)
        {
            SelectedSlot.UnequipItemSlot();
            SelectedSlot.SelectSlot();
        }
    }

    // Callback para manejar la UI por tiempo
    public void ClockUpdate(GameTimestamp timestamp)
    {
        // Recuperamos las horas y los minutos
        int hours = timestamp.hour;
        int minutes = timestamp.minute;

        timeText.text = hours.ToString("00") + ":" + minutes.ToString("00");
        
        // Recuperamos los dias, la estacion y el dia de la semana
        int day = timestamp.day;
        string season = timestamp.season.ToString();
        string dayOfTheWeek = timestamp.GetDayOfTheWeek().ToString();

        dateText.text = season + " " + day + " " + "(" + dayOfTheWeek + ")";
    }

    #region Panels

    public void OpenCloseInventoryPanel()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
    }

    #endregion
}
