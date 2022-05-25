using UnityEngine;

[CreateAssetMenu(menuName = "Tools")]

public class Tool : ItemInventory
{
    public enum ToolType
    {
        WateringCan,
        FishinRod,
        Shovel
    }

    public ToolType toolType;
}