using UnityEngine;

[CreateAssetMenu(menuName = "Seeds/SeedPatata")]

public class SeedPatata : ItemInventory
{
    // Variables
    public int daysToGrow;

    public ItemInventory cropToYield;

    public GameObject seedling;
}
