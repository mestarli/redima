using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropBehaviour : MonoBehaviour
{
    //Variables
    private ItemInventory seedToGrow;

    [Header("Etapas de vida")] 
    public GameObject seed;
    public GameObject seedling;
    private GameObject harvestable;
    public enum CropState
    {
        Seed,
        Seedling,
        Harvestable
    }

    public CropState cropState;
    
    // Inicializamos para el gameobject de crop (brote)
    // Metodo que se llama cuando el player planta una semilla
    public void Plant(ItemInventory seedToGrow)
    {
        // Guardado de la informacion de la semilla
        this.seedToGrow = seedToGrow;
        
        //
        seedling = Instantiate(seedToGrow.gameModel, transform);
    }

    public void Grow()
    {
        
    }
}
