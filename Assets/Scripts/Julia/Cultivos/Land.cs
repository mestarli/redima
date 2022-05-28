using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Land : MonoBehaviour, ITimeTracker
{
    // https://www.youtube.com/watch?v=uij6JL_8LWo - 13:09
    
    // Variables
    public enum LandStatus
    {
        Soil, Farmland, Watered
    }

    public LandStatus landStatus;
    
    public Material soilMat, farmlandMat, wateredMat;
    private MeshRenderer renderer;

    public GameObject select;

    private GameTimestamp timeWatered;

    public Tool tool;
    
    void Start()
    {
        // Accedemos al componente Renderer del objeto
        gameObject.GetComponent<MeshRenderer>().material = soilMat;
        
        // Ponemos el estado de la tierra a soil por defecto
        SwitchLandStatus(LandStatus.Soil);
        
        // Deselccionar la tierra por defecto
        Select(false);
        
        // Añadir este script a la lista de listeners del TimeManager
        TimeManager.instance.RegisterTracker(this);
    }

    public void SwitchLandStatus(LandStatus statusToSwitch)
    {
        // Pondrá el status acorde
        landStatus = statusToSwitch;
        Material materialToSwitch = soilMat;
        
        // Depende del estado en que se encuentre la tierra, se va a mostrar un material u otro
        switch (statusToSwitch)
        {
            case LandStatus.Soil:
                // Mostrará el material Soil
                materialToSwitch = soilMat;
                break;
            
            case LandStatus.Farmland:
                // Mostrará el material Farmland
                materialToSwitch = farmlandMat;
                break;
            
            case LandStatus.Watered:
                // Mostrará el material Watered
                materialToSwitch = wateredMat;
                
                // 
                timeWatered = TimeManager.instance.GetGameTimestamp();
                break;
        }
        
        // Accedemos al renderer para aplicar los cambios del switch
        gameObject.GetComponent<MeshRenderer>().material = materialToSwitch;
    }

    // Metodo para saber que trozo de tierra para plantar estamos seleccionando
    public void Select(bool toggle)
    {
        select.SetActive(toggle);
    }

    // Cuando el player pulse la tecla de interaccion y esté un trozo de tierra seleccionado
    public void Interact()
    {
        // Miramos que el player tenga una herramienta equipada
        if (Inventory.instance.isEquipped)
        {
            if (tool.tools.type == ItemTypes.Tools)
            {
                if (tool.toolType == Tool.ToolType.WateringCan)
                {
                    // Interactuar
                    SwitchLandStatus(LandStatus.Watered);
                }
            }

            if (tool.tools.type == ItemTypes.Seeds)
            {
                // Interactuar
                SwitchLandStatus(LandStatus.Farmland);
            }
        }
    }

    public void ClockUpdate(GameTimestamp timestamp)
    {
        // Miramos si han pasado 24 horas desde que la tierra se ha regado
        if (landStatus == LandStatus.Watered)
        {
            // Horas desde que hemos regado la tierra
            int hoursElapsed = GameTimestamp.CompareTimestamps(timeWatered, timestamp);

            Debug.Log(hoursElapsed);

            if (hoursElapsed > 24)
            {
                SwitchLandStatus(LandStatus.Farmland);
            }
        }
    }
}
