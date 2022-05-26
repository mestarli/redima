using UnityEngine;

[CreateAssetMenu(menuName = "Consumables/Atun")]

public class AtunComida : ItemInventory
{
    [Header("Atun info")] 
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
