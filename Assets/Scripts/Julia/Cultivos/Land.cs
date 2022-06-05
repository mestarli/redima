using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Land : MonoBehaviour, ITimeTracker
{
    // https://www.youtube.com/watch?v=uij6JL_8LWo
    
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

    [Header("Crop Prefab")]
    public GameObject cropPrefab;
    CropBehaviour cropPlanted = null;

    [SerializeField] private Seed semillaAPlantar;
    
    public Seed[] availableSeeds;


    public void Start()
    {
        // Accedemos al componente Renderer del objeto
        gameObject.GetComponent<MeshRenderer>().material = soilMat;
        availableSeeds = Resources.LoadAll<Seed>("ScriptableObjects/Semillas");
        
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
                    
                    StartCoroutine(ChangeMatToFarmland());

                    if (landStatus == LandStatus.Farmland)
                    {
                        SwitchLandStatus(LandStatus.Farmland);
                    }
                }
            }
            
            if (HandSlot.instanceHandSlot.ItemInventoryHand.type == ItemTypes.Seeds && landStatus != LandStatus.Soil && 
                landStatus == LandStatus.Watered && cropPlanted == null && HandSlot.instanceHandSlot.ItemInventoryHand.quantity > 0)
            {
                HandSlot.instanceHandSlot.ItemInventoryHand.quantity -= 1;
                
                if (HandSlot.instanceHandSlot.ItemInventoryHand.quantity <= 0)
                {
                    HandSlot.instanceHandSlot.ActivateSlotUI(false);
                }

                // Instanciamos el objeto crop en los cultivos
                GameObject cropObject = Instantiate(cropPrefab, transform);
                cropObject.transform.localScale = new  Vector3(10, 1, 10);
                
                // Accedemos al script CropBehaviour que esta dentro del crop
                cropPlanted = cropObject.GetComponent<CropBehaviour>();
                
                //Recogemos la info de la semilla equipada y se la pasamos a uno de tipo semilla para poder plantarse
                foreach (var seed in availableSeeds)
                {
                    if (seed.ID == HandSlot.instanceHandSlot.ItemInventoryHand.ID)
                    {
                        semillaAPlantar = seed;
                    }
                }
                
                cropPlanted.Plant(semillaAPlantar);
            }
        }
    }

    public void ClockUpdate(GameTimestamp timestamp)
    {
        if (landStatus == LandStatus.Farmland)
        {
            // Hacemos crecer la planta mientras esta la tierra regada
            if (cropPlanted != null)
            {
                cropPlanted.Grow();
            }
        }
    }

    IEnumerator ChangeMatToFarmland()
    {
        yield return new WaitForSeconds(1); 
        SwitchLandStatus(LandStatus.Farmland);
    }
}
