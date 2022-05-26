using UnityEngine;

[CreateAssetMenu(menuName = "Consumables/Celacanto")]

public class CelacantoComida : ItemInventory
{
    [Header("Celacanto info")] 
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
