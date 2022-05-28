using UnityEngine;

[CreateAssetMenu(menuName = "Consumables/ChampisReduccion")]

public class ChampisReduccionComida : ItemInventory
{  
    [Header("Champis info")] 
    public float reduccion;
    
    public override bool UseItem()
    {
        if (ID == "ChampiReduccion")
        {
            Inventory.instance.Player.Tamaño(reduccion);
            return true;
        }

        return false;
    }
}
