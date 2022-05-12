using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Variables
    [Header("Items")] 
    [SerializeField] private ItemInventory[] itemsInventory;
    [SerializeField] private Player player;
    [SerializeField] private int slotsNum;

    public Player Player => player;
    public static Inventory instance; 
    public ItemInventory[] ItemsInventory => itemsInventory;
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

        if (itemToAdd.isCumulative)
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
                        
                        UIInventory.instanceUI.DrawItemInInventory(itemToAdd, itemsInventory[index[i]].quantity, index[i]);
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
                UIInventory.instanceUI.DrawItemInInventory(item, quantity, i);
                return; 
            }
        }
    }
}
