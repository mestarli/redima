using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour, ITimeTracker
{
    // Variables
    public static UI_Manager instance;

    [Header("PANELS")]
    [SerializeField] private GameObject mapPanel;
    [SerializeField] private GameObject pecedexPanel;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject animadexPanel;
    [SerializeField] private GameObject plantadexPanel;
    [SerializeField] private GameObject recetadexPanel;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject enciclopediaPanels;

    [Header("Status Panel")]
    private List<HandSlot> availableSlot = new List<HandSlot>();
    [SerializeField] private HandSlot slotPrefab;
    [SerializeField] private Transform handContent;
    [SerializeField] private int slotsNum;

    [Header("Time Info Panel")]
    public Text timeText;
    public Text dateText;
    public Text dateNumText;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        else
        {
            Destroy(gameObject);
        }
        
        mapPanel.SetActive(false);
        pecedexPanel.SetActive(false);
        animadexPanel.SetActive(false);
        plantadexPanel.SetActive(false);
        recetadexPanel.SetActive(false);
        inventoryPanel.SetActive(false);
        enciclopediaPanels.SetActive(false);
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

        dateText.text = season + " " + dayOfTheWeek;
        dateNumText.text = "" + day;
    }

    public void Options()
    {
        mapPanel.SetActive(false);
        pecedexPanel.SetActive(false);
        optionsPanel.SetActive(true);
        animadexPanel.SetActive(false);
        plantadexPanel.SetActive(false);
        recetadexPanel.SetActive(false);
        inventoryPanel.SetActive(false);
        enciclopediaPanels.SetActive(true);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
    
    #region Panels

    public void OpenCloseInventoryPanel()
    {
        enciclopediaPanels.SetActive(!enciclopediaPanels.activeSelf);
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
    }
    
    public void OpenCloseMapPanel()
    {
        enciclopediaPanels.SetActive(!enciclopediaPanels.activeSelf);
        mapPanel.SetActive(!mapPanel.activeSelf);
    }
    
    public void PassToMapPanel()
    {
        mapPanel.SetActive(true);
        pecedexPanel.SetActive(false);
        optionsPanel.SetActive(false);
        animadexPanel.SetActive(false);
        plantadexPanel.SetActive(false);
        recetadexPanel.SetActive(false);
        inventoryPanel.SetActive(false);
    }
    
    public void PassToPecedexPanel()
    {
        mapPanel.SetActive(false);
        pecedexPanel.SetActive(true);
        optionsPanel.SetActive(false);
        animadexPanel.SetActive(false);
        plantadexPanel.SetActive(false);
        recetadexPanel.SetActive(false);
        inventoryPanel.SetActive(false);
    }
    
    public void PassToOptionsPanel()
    {
        mapPanel.SetActive(false);
        pecedexPanel.SetActive(false);
        optionsPanel.SetActive(true);
        animadexPanel.SetActive(false);
        plantadexPanel.SetActive(false);
        recetadexPanel.SetActive(false);
        inventoryPanel.SetActive(false);
    }

    public void PassToAnimadexPanel()
    {
        mapPanel.SetActive(false);
        pecedexPanel.SetActive(false);
        optionsPanel.SetActive(false);
        animadexPanel.SetActive(true);
        plantadexPanel.SetActive(false);
        recetadexPanel.SetActive(false);
        inventoryPanel.SetActive(false);
    }

    public void PassToPlantadexPanel()
    {
        mapPanel.SetActive(false);
        pecedexPanel.SetActive(false);
        optionsPanel.SetActive(false);
        animadexPanel.SetActive(false);
        plantadexPanel.SetActive(true);
        recetadexPanel.SetActive(false);
        inventoryPanel.SetActive(false);
    }

    public void PassToRecetadexPanel()
    {
        mapPanel.SetActive(false);
        pecedexPanel.SetActive(false);
        optionsPanel.SetActive(false);
        animadexPanel.SetActive(false);
        plantadexPanel.SetActive(false);
        recetadexPanel.SetActive(true);
        inventoryPanel.SetActive(false);
    }
    
    public void PassToInventoryPanel()
    {
        mapPanel.SetActive(false);
        pecedexPanel.SetActive(false);
        optionsPanel.SetActive(false);
        animadexPanel.SetActive(false);
        plantadexPanel.SetActive(false);
        recetadexPanel.SetActive(false);
        inventoryPanel.SetActive(true);
    } 

    #endregion
}
