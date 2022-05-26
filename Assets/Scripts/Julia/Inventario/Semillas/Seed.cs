using UnityEngine;

[CreateAssetMenu(menuName = "Seeds")]

public class Seed : ItemInventory
{
    // Variables
    public int daysToGrow;

    public ItemInventory cropToYield;

    public GameObject seedling;
}
