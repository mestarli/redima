using UnityEngine;

[CreateAssetMenu(menuName = "Consumables/PezLuna")]

public class PezLunaComida : ItemInventory
{
    [Header("Pez Luna info")] 
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
