using UnityEngine;

[CreateAssetMenu(menuName = "Consumables/ChampisNormal")]

public class ChampisNormalComida : ItemInventory
{  
    [Header("Champis info")] 
    public float normal;

    public override bool UseItem()
    {
        if (ID == "ChampiNormal")
        {
            Inventory.instance.Player.Tamaño(normal);
            return true;
        }

        return false;
    }
}
