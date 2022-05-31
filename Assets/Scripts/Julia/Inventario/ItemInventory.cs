
using UnityEngine;

public enum ItemTypes
{
    Ingredients, 
    Seeds,
    Consumables,
    Tools
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
    public bool canBeEquipped;
    public bool isCumulative;
    public int maxCumulative;
    public GameObject gameModel;

    public int quantity;

    // Metodo que crea una copia del scriptable object para no modificar el original
    public ItemInventory copyItem()
    {
        ItemInventory newInstance = Instantiate(this);
        return newInstance;
    }

    public virtual bool UseItem()
    {
        return true;
    }

    public virtual bool EquipItem()
    {
        return true;
    }

    public virtual bool DeleteItem()
    {
        return true;
    }
}
