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
    [SerializeField] private GameObject mapPanel;
    [SerializeField] private GameObject pecedexPanel;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject animadexPanel;
    [SerializeField] private GameObject mainMenuPanel;
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
    // Variables
    public static UI_Manager instance;
    
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

        #region TryCatch

        try
        {
            mapPanel.SetActive(false);
        }
        catch (UnassignedReferenceException ex)
        {
            Debug.Log("Map Panel not assigned");
        }

        try
        {
            pecedexPanel.SetActive(false);
        }
        catch (UnassignedReferenceException ex)
        {
            Debug.Log("Pecedex Panel not assigned");
        }
      
        try 
        {
            optionsPanel.SetActive(false);
        }
        catch (UnassignedReferenceException ex) 
        {
            Debug.Log("Options Panel not assigned");
        }
        
        try 
        {
            animadexPanel.SetActive(false);
        }
        catch (UnassignedReferenceException ex) 
        {
            Debug.Log("Animadex Panel not assigned");
        }
        
        try 
        {
            mainMenuPanel.SetActive(true);
        }
        catch (UnassignedReferenceException ex) 
        {
            Debug.Log("Main Menu Panel not assigned");
        }
        
        try 
        {
            plantadexPanel.SetActive(false);
        }
        catch (UnassignedReferenceException ex) 
        {
            Debug.Log("Plantadex Panel not assigned");
        }
        
        try 
        {
            recetadexPanel.SetActive(false);
        }
        catch (UnassignedReferenceException ex) 
        {
            Debug.Log("Recetadex Panel not assigned");
        }
        
        try 
        {
            inventoryPanel.SetActive(false);
        }
        catch (UnassignedReferenceException ex) 
        {
            Debug.Log("Inventory Panel not assigned");
        }
        
        try 
        {
            enciclopediaPanels.SetActive(false);
        }
        catch (UnassignedReferenceException ex) 
        {
            Debug.Log("Enciclopedia Panel not assigned");
        }
        #endregion
    }

    private void Start()
    {
        // Llamada del método para crear el slot de equipacion
        InitializeEquippedSlot();
        
        // Añadir el UI_Manager a la lista de objetos del TimeManager notificará cuando el time se actualice
        try {
            TimeManager.instance.RegisterTracker(this);
        }       
        catch (NullReferenceException ex) {
            Debug.Log("TimeManager not assigned");
        }
       
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
        SceneManager.LoadScene("MainScene");
        
        mapPanel.SetActive(false);
        pecedexPanel.SetActive(false);
        optionsPanel.SetActive(false);
        animadexPanel.SetActive(false);
        mainMenuPanel.SetActive(false);
        plantadexPanel.SetActive(false);
        recetadexPanel.SetActive(false);
        inventoryPanel.SetActive(false);
        enciclopediaPanels.SetActive(false);
    }

    public void Options()
    {
        mapPanel.SetActive(false);
        pecedexPanel.SetActive(false);
        optionsPanel.SetActive(true);
        animadexPanel.SetActive(false);
        mainMenuPanel.SetActive(false);
        plantadexPanel.SetActive(false);
        recetadexPanel.SetActive(false);
        inventoryPanel.SetActive(false);
        enciclopediaPanels.SetActive(true);
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
