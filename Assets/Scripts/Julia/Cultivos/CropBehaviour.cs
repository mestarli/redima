using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropBehaviour : MonoBehaviour
{
    //Variables
    [SerializeField] private Seed seedToGrow;

    public int growth;
    public int maxGrowth;
    
    [Header("Etapas de vida")] 
    public GameObject seed;
    public GameObject seedling;
    [SerializeField] private GameObject harvestable;
    public enum CropState
    {
        Seed,
        Seedling,
        Harvestable
    }

    public CropState cropState;
    
    // Inicializamos para el gameobject de crop (brote)
    // Metodo que se llama cuando el player planta una semilla
    public void Plant(Seed seedToGrow)
    {
        // Guardado de la informacion de la semilla
        this.seedToGrow = seedToGrow;
        
        // Instanciar la seedling y los gameobjects cosechables
        seedling = Instantiate(seedToGrow.seedling, transform);

        // Accedemos al script del crop
        ItemInventory cropToYield = seedToGrow.cropToYield;
        
        // Instanciamos el gameobject cosechable
        harvestable = Instantiate(cropToYield.gameModel, transform);
        
        // Conversion de los dias de crecimiento a horas
        int hoursToGrow = GameTimestamp.DaysToHours(seedToGrow.daysToGrow);
        
        // Conversion de los dias de crecimiento a horas
        maxGrowth = GameTimestamp.HoursToMinutes(hoursToGrow);
        
        // Establecemos como predefinido el estado del crop a seed
        SwitchState(CropState.Seed);
    }

    public void Grow()
    {
        // Aumentamos el crecimiento
        growth++;

        // La semilla brotará en una seedling cuando el crecimiento este al 50%
        if (growth >= maxGrowth / 2 && cropState == CropState.Seed)
        {
            SwitchState(CropState.Seedling);
        }
        
        // Crecimiento de seedling a cosechable
        if (growth >= maxGrowth && cropState == CropState.Seedling)
        {
            SwitchState(CropState.Harvestable);
        }
    }

    // Metodo para cambiar el estado del crop
    private void SwitchState(CropState stateToSwitch)
    {
        // Reseteamos todos los objetos y ponemos todos los gameobjects inactivos
        seed.SetActive(false);
        seedling.SetActive(false);
        harvestable.SetActive(false);

        switch (stateToSwitch)
        {
            case CropState.Seed:
                // Activamos la seed
                seed.SetActive(true);
                break;
            
            case CropState.Seedling:
                // Activamos la seedling
                seedling.SetActive(true);
                break;
            
            case CropState.Harvestable:
                // Activamos el cosechable
                harvestable.SetActive(true);
                break;
        }
        
        // Establecemos el estado del crop actual en el estado al que estamos cambiando
        cropState = stateToSwitch;
    }
}
