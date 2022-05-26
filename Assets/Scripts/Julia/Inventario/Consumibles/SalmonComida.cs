using UnityEngine;

[CreateAssetMenu(menuName = "Consumables/Salmon")]

public class SalmonComida : ItemInventory
{
    [Header("Salmon info")] 
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
