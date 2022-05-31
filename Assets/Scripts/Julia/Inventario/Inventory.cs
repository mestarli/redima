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

    // Verficacion en caso de tener ya un item similar en el inventario
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

            /*
            if(itemsInventory[i] != null && itemsInventory[i].type == ItemTypes.Seeds)
            {
                itemsInventory[i] = item;
                itemsInventory[i].quantity = quantity;
                UIInventory.instanceInventoryUI.DrawItemInInventory(item, quantity, i);
                return;
            }
            */
        }
    }

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

    public void EquippedUsedItem(int index)
    {
        itemsInventory[index].quantity--;
      
        if (itemsInventory[index].quantity <= 0)
        {
            itemsInventory[index].quantity = 0;
            itemsInventory[index] = null;
            UIInventory.instanceInventoryUI.DrawItemInInventory(itemsInventory[index], itemsInventory[index].quantity, index);
        }

        else
        {
            UI_Manager.instanceUI.DrawItemInEquippedSlot(itemsInventory[index], itemsInventory[index].quantity, index);
        }
    }
    
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
    
    private void UnequipItem(int index)
    {
        if (itemsInventory[index] == null && !itemsInventory[index].canBeEquipped)
        {
            return;
        }

        if (itemsInventory[index].EquipItem() && itemsInventory[index].canBeEquipped)
        {
            itemsInventory[index] = handSlot.ItemInventoryHand;
            EquippedUsedItem(index);
        }
    }

    #region Events

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

    private void HandSlotInteractionResponse(HandInteractionTypes type, int index)
    {
        switch (type)
        {
            case HandInteractionTypes.Unequip:
                UnequipItem(index);
                break;
        }
    }
    
    private void OnEnable()
    {
        InventorySlot.SlotInteractionEvent += SlotInteractionResponse;
        HandSlot.HandSlotInteractionEvent += HandSlotInteractionResponse;
    }

    private void OnDisable()
    {
        InventorySlot.SlotInteractionEvent -= SlotInteractionResponse;
        HandSlot.HandSlotInteractionEvent += HandSlotInteractionResponse;
    }

    #endregion
}
