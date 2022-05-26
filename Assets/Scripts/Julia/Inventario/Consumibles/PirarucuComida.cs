using UnityEngine;

[CreateAssetMenu(menuName = "Consumables/Pirarucu")]

public class PirarucuComida : ItemInventory
{
    [Header("Pirarucu info")] 
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
