
using UnityEngine;

public enum ItemTypes
{
    Ingredients,
    Potions,
    Mushrooms
}

public class ItemInventory : ScriptableObject
{
    [Header("Parameters")] 
    public string ID;
    public string name;
    public Sprite icon;
    [TextArea] public string description;

    [Header("Information")] 
    public ItemTypes type;
    public bool canBeConsumed;
    public bool isCumulative;
    public int maxCumulative;

    [HideInInspector] public int quantity;
}
