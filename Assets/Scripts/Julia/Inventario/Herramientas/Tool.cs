using UnityEngine;

[CreateAssetMenu(menuName = "Tools")]

public class Tool : ItemInventory
{
    public enum ToolType
    {
        WateringCan,
        FishingRod,
        Shovel,
        Sickle
    }

    public ToolType toolType;
}