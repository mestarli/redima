using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Variables
    [Header("Items")] 
    [SerializeField] private ItemInventory[] itemsInventory;
    [SerializeField] private Player player;
    [SerializeField] private int slotsNum;
    [SerializeField] private HandSlot handSlot;

    public Player Player => player;
    public static Inventory instance; 
    public ItemInventory[] ItemsInventory => itemsInventory;

    public bool isEquipped;

    public HandSlot HandSlot
    {
        get => handSlot;
        set => handSlot = value;
    }
    public int SlotsNum => slotsNum;

    void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        
        itemsInventory = new ItemInventory[slotsNum];
    }
    
    /// <summary>
    /// Este metodo verifica que en caso de tener ya un item similar en el inventario, se suma la cantidad existente en
    /// el inventario mas la que se esta añadiendo. Pudiendo crear paquetes del mismo objeto en un solo slot hasta
    /// cierto limite. Limite definido en cada scriptable object y que si es un objeto normal y corriente que se puede
    /// consumir sera de 60 y si es una herramienta sera de 1.
    /// Las herramientas solo se pueden tener 1, porque no podemos tener mas de una herramienta del mismo tipo.
    ///
    /// La informacion se la pasa el script del ItemToAdd.
    /// </summary>
    /// <param name="itemToAdd">el scriptable object que le pasamos cuando se recoge un objeto, es una copia del
    /// scriptable object original para no modificarlo.</param>
    /// <param name="quantity">la cantidad que hemos recogido del scriptable object</param>
    
    public void AddItem(ItemInventory itemToAdd, int quantity)
    {
        if (itemToAdd == null)
        {
            return;
        }

        List<int> index = VerifyStock(itemToAdd.ID);

        if (itemToAdd.isCumulative && itemToAdd.canBeConsumed)
        {
            if (index.Count > 0)
            {
                for (int i = 0; i < index.Count; i++)
                {
                    if (itemsInventory[index[i]].quantity < itemToAdd.maxCumulative)
                    {
                        itemsInventory[index[i]].quantity += quantity;
                        
                        if (itemsInventory[index[i]].quantity > itemToAdd.maxCumulative)
                        {
                            int difference = itemsInventory[index[i]].quantity - itemToAdd.maxCumulative;
                            itemsInventory[index[i]].quantity = itemToAdd.maxCumulative;
                            AddItem(itemToAdd, difference);
                        }
                        
                        UIInventory.instanceInventoryUI.DrawItemInInventory(itemToAdd, itemsInventory[index[i]].quantity, index[i]);
                        return;
                    }
                }
            }
        }

        if (!itemToAdd.isCumulative && itemToAdd.canBeEquipped)
        {
            if (index.Count > 0)
            {
                for (int i = 0; i < index.Count; i++)
                {
                    if (itemsInventory[index[i]].quantity < itemToAdd.maxCumulative)
                    {
                        itemsInventory[index[i]].quantity += quantity;

                        if (itemsInventory[index[i]].quantity > itemToAdd.maxCumulative)
                        {
                            int equipDifference = itemsInventory[index[i]].quantity - itemToAdd.maxCumulative;
                            itemsInventory[index[i]].quantity = itemToAdd.maxCumulative;
                            AddItem(itemToAdd, equipDifference);
                        }
                        
                        UIInventory.instanceInventoryUI.DrawItemInInventory(itemToAdd, itemsInventory[index[i]].quantity, index[i]);
                        return;
                    }
                }
            }
        }

        if (quantity <= 0)
        {
            return;
        }

        if (quantity > itemToAdd.maxCumulative)
        {
            AddItemInADisponibleSlot(itemToAdd, itemToAdd.maxCumulative);
            quantity -= itemToAdd.maxCumulative;
            AddItem(itemToAdd, quantity);
        }
        
        else
        {
            AddItemInADisponibleSlot(itemToAdd, quantity);
        }
    }

    /// <summary>
    /// Verificamos todos los slots existentes. Si en la lista de int hay un objeto, comprobamos que la ID del objeto
    /// que se añade es la misma ID que el objeto que ya tenemos en el inventario.
    /// Si coinciden, entonces se añade el item en ese mismo index. Si no, devuelve los index la lista de items
    /// </summary>
    /// <param name="itemID">string que se le pasa para poder comprobar que se tenga ese objeto ya en el inventario</param>
    /// <returns></returns>
    private List<int> VerifyStock(string itemID)
    {
        List<int> itemsIndex = new List<int>();

        for (int i = 0; i < itemsInventory.Length; i++)
        {
            if (itemsInventory[i] != null)
            {
                if (itemsInventory[i].ID == itemID)
                {
                    itemsIndex.Add(i);
                }
            }
        }

        return itemsIndex;
    }

    /// <summary>
    /// Metodo que añade los items en un slot disponible. Este metodo comprueba toda una lista de int donde se mira si
    /// hay algun item. Si no hay ningun item en toda la lista, se creara una copia del scriptable object y se le pasara
    /// la cantidad que se ha recogido de este y luego se mostrara el objeto recogido en el primer slot del
    /// inventario (el elemento 0 de la lista). Si ya hay items en nuestro inventario, despues de recorrer la lista de
    /// int, se añadira en el primer slot que encuentre disponible. Despues de esto, saldrá del metodo
    /// </summary>
    /// <param name="item">se recoge del ItemToAdd, es el scriptable object del qual se hara una copia</param>
    /// <param name="quantity">se recoge del ItemToAdd, la cantidad que le hemos puesto que hay de ese item</param>
    private void AddItemInADisponibleSlot(ItemInventory item, int quantity)
    {
        for (int i = 0; i < itemsInventory.Length; i++)
        {
            if (itemsInventory[i] == null)
            {
                itemsInventory[i] = item.copyItem();
                itemsInventory[i].quantity = quantity;
                UIInventory.instanceInventoryUI.DrawItemInInventory(item, quantity, i);
                return; 
            }
        }
    }

    /// <summary>
    /// Metodo para cuando se usa un item. Este metodo hace que se reste la cantidad en 1 cada vez que se usa un item.
    /// Si la cantidad de este item es inferior o igual a 0, la cantidad de ese objeto se volvera 0, nulo y se mostrara
    /// en la UI que de ese objeto no queda nada.
    /// Si esa condicion no se cumple, simplemente se actualizara la informacion de ese item en la UI.
    /// </summary>
    /// <param name="index">es la posicion del objeto que vamos a restar su cantidad al usarlo</param>
    private void EliminateUsedItem(int index)
    {
        itemsInventory[index].quantity--;

        if (itemsInventory[index].quantity <= 0)
        {
            itemsInventory[index].quantity = 0;
            itemsInventory[index] = null;
            UIInventory.instanceInventoryUI.DrawItemInInventory(null, 0, index);
        }

        else
        {
            UIInventory.instanceInventoryUI.DrawItemInInventory(itemsInventory[index], itemsInventory[index].quantity, index);
        }
    }

    /// <summary>
    /// Metodo para equipar una herramienta o semillas. Este metodo hace que si el valor del objeto es menor o igual a 0,
    /// el objeto de ese indice se volvera nulo, su cantidad sera 0 y se actualizara la info de la UI en ese indice.
    /// Si no se cumple, el objeto que se añada sera nulo y tendra cantidad 0 en ese indice.
    /// </summary>
    /// <param name="index">es la posicion del objeto que vamos a restar su cantidad al equiparlo</param>
    public void EquippedUsedItem(int index)
    {
        if (itemsInventory[index].quantity <= 0)
        {
            itemsInventory[index].quantity = 0;
            itemsInventory[index] = null;
            UIInventory.instanceInventoryUI.DrawItemInInventory(itemsInventory[index], itemsInventory[index].quantity, index);
        }

        else
        {
            UIInventory.instanceInventoryUI.DrawItemInInventory(null, 0, index);
            itemsInventory[index] = null;
        }
    }
    
    /// <summary>
    /// Metodo para usar un item. Si el objeto en ese indice es nulo y no se puede consumir, saldrá de la logica.
    /// Si ese objeto se puede usar, el metodo UseItem() del ItemInventory nos devolverá a true y si ese objeto se
    /// puede consumir, se llamará al metodo que resta los items en ese indice. 
    /// </summary>
    /// <param name="index">es la posicion del objeto que tenemos seleccionado</param>
    private void UseItem(int index)
    {
        if (itemsInventory[index] == null && !itemsInventory[index].canBeConsumed)
        {
            return;
        }

        if (itemsInventory[index].UseItem() && itemsInventory[index].canBeConsumed)
        {
            EliminateUsedItem(index);
        }
    }

    /// <summary>
    /// Metodo para equipar un item. Si el objeto es nulo y no se puede equipar, saldrá de la logica. Si ese objeto se
    /// puede equipar, el metodo EquipItem() del ItemInventory nos devuelve true y si ese objeto se puede equipar, le
    /// pasaremos la informacion de ese objeto en ese indice al handSlot que es donde se van a equipar nuestros objetos.
    /// Luego se actualizara la informacion del slot con la informacion que tenia el scriptable object del inventario.
    /// Por ultimo se llamará al metodo que elimina el objeto del inventario.
    /// </summary>
    /// <param name="index">es la posicion del objeto que tenemos seleccionado</param>
    private void EquipItem(int index)
    {
        if (itemsInventory[index] == null && !itemsInventory[index].canBeEquipped)
        {
            return;
        }

        if (itemsInventory[index].EquipItem() && itemsInventory[index].canBeEquipped)
        {
            handSlot.ItemInventoryHand = itemsInventory[index];
            handSlot.Update_Info_item_inventory();
            EquippedUsedItem(index);
        }
    }

    #region Events

    // Diferentes respuestas al darle click a un slot, al usarlo, al equiparlo y al eliminar cualquier objeto
    private void SlotInteractionResponse(InteractionTypes type, int index)
    {
        switch (type)
        {
            case InteractionTypes.Use:
                UseItem(index);
                break;
            
            case InteractionTypes.Equip:
                EquipItem(index);
                break;
            
            case InteractionTypes.Delete:
                break;
        }
    }
    
    private void OnEnable()
    {
        InventorySlot.SlotInteractionEvent += SlotInteractionResponse;
    }

    private void OnDisable()
    {
        InventorySlot.SlotInteractionEvent -= SlotInteractionResponse;
    }

    #endregion
}
