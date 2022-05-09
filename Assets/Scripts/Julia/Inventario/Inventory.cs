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

    void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        
        itemsInventory = new ItemInventory[slotsNum];
    }
}
