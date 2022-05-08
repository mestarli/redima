using UnityEngine;

[CreateAssetMenu(menuName = "Items/HealthPotion")]
public class ItemHealthPotion : ItemInventory
{
    [Header("Potion info")] 
    public float HPRestoration;
}