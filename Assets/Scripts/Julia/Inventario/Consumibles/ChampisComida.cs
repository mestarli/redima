using UnityEngine;

[CreateAssetMenu(menuName = "Consumables/Champis")]

public class ChampisComida : ItemInventory
{  
    [Header("Champis info")] 
    public float reduccion;

    public float normal;

    public override bool UseItem()
    {
        if (ID == "ChampiReduccion")
        {
            Inventory.instance.Player.Tamaño(reduccion);
            return true;
        }

        if (ID == "ChampiNormal")
        {
            Inventory.instance.Player.Tamaño(normal);
            return true;
        }

        return false;
    }
}
