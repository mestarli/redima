using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Land : MonoBehaviour
{
    // Variables
    public enum LandStatus
    {
        Soil, Farmland, Watered
    }

    public LandStatus landStatus;
    
    public Material soilMat, farmlandMat, wateredMat;

    private new Renderer renderer;

    public GameObject select;
    
    void Start()
    {
        // Accedemos al componente Renderer del objeto
        renderer.GetComponent<Renderer>();
        
        // Ponemos el estado de la tierra a soil por defecto
        SwitchLandStatus(LandStatus.Soil);
        
        // Deselccionar la tierra por defecto
        Select(false);
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
                break;
        }
        
        // Accedemos al renderer para aplicar los cambios del switch
        renderer.material = materialToSwitch;
    }

    // Metodo para saber que trozo de tierra para plantar estamos seleccionando
    public void Select(bool toggle)
    {
        select.SetActive(toggle);
    }

    // Cuando el player pulse la tecla de interaccion y esté un trozo de tierra seleccionado
    public void Interact()
    {
        // Interactuar
        SwitchLandStatus(LandStatus.Farmland);
    }
}
