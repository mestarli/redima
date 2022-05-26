using UnityEngine;

[CreateAssetMenu(menuName = "Consumables/PezRemo")]

public class PezRemoComida : ItemInventory
{
    [Header("Pez Remo info")] 
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
