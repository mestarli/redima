using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour, ITimeTracker
{
    // Variables
    [Header("PANELS")]
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject optionsPanel;

    [Header("Status Panel")]
    private List<HandSlot> availableSlot = new List<HandSlot>();
    [SerializeField] private HandSlot slotPrefab;
    [SerializeField] private Transform handContent;
    [SerializeField] private int slotsNum;

    [Header("Time Info Panel")]
    public Text timeText;
    public Text dateText;
    
    private void Awake()
    {
        inventoryPanel.SetActive(false);
    }

    private void Start()
    {
        // Llamada del método para crear el slot de equipacion
        InitializeEquippedSlot();
        
        // Añadir el UI_Manager a la lista de objetos del TimeManager notificará cuando el time se actualice
        TimeManager.instance.RegisterTracker(this);
    }

    // Metodo para crear slots en funcion del numero que pongamos nosotros
    private void InitializeEquippedSlot()
    {
        for (int i = 0; i < slotsNum; i++)
        {
            HandSlot newSlot = Instantiate(slotPrefab, handContent);
            newSlot.Index = i;
            availableSlot.Add(newSlot);
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

    public void PlayGame()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Options()
    {
        
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
    
    #region Panels

    public void OpenCloseInventoryPanel()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
    }

    #endregion
}
