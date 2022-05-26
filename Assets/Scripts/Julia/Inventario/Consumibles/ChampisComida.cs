using UnityEngine;

[CreateAssetMenu(menuName = "Consumables/Champis")]

public class ChampisComida : ItemInventory
{  
    [Header("Champis info")] 
    public float hungryRestoration;

    public override bool UseItem()
    {
        if (Inventory.instance.Player.puedeAlimentarse)
        {
            Inventory.instance.Player.alimentarse(hungryRestoration);
            return true;
        }

        return false;
    }
}
