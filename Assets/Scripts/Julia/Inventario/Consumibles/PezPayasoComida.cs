using UnityEngine;

[CreateAssetMenu(menuName = "Consumables/PezPayaso")]

public class PezPayasoComida : ItemInventory
{
    [Header("Pez Payaso info")] 
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
