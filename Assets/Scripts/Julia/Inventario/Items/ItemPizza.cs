using UnityEngine;

[CreateAssetMenu(menuName = "Items/Pizza")]
public class ItemPizza : ItemInventory
{
    [Header("Platano info")] 
    public float estamineRestoration;

    
    public override bool UseItem()
    {
        if (Inventory.instance.Player.puedeRecuperarEstamina)
        {
            Inventory.instance.Player.recuperarEstamina(estamineRestoration);
            return true;
        }

        return false;
    }
    
}