using UnityEngine;

[CreateAssetMenu(menuName = "Consumables/SalmonRojo")]

public class SalmonRojoComida : ItemInventory
{  
    [Header("Salmon Rojo info")] 
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
