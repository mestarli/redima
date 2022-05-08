using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Variables
    [SerializeField] private int slotsNum;
    public static Inventory instance;

    [Header("Items")] [SerializeField] private ItemInventory[] itemsInventory;
    
    public int SlotsNum => slotsNum;

    private void Start()
    {
        instance = this;
        itemsInventory = new ItemInventory[slotsNum];
    }
}
