using UnityEngine;

[CreateAssetMenu(menuName = "Items/Platano")]
public class ItemPlatano : ItemInventory
{
    [Header("Platano info")] 
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