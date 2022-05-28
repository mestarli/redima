using UnityEngine;

[CreateAssetMenu(menuName = "Tools")]

public class Tool : ItemInventory
{
    public enum ToolType
    {
        WateringCan,
        FishingRod,
    }

    public ToolType toolType;
    
    public ItemInventory tools;
}