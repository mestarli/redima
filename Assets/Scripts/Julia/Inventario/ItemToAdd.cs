using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ItemToAdd : MonoBehaviour
{
    // Variables
    [Header("Configuration")]
    [SerializeField] private ItemInventory _itemInventoryReference;
    [SerializeField] private int quantityToAdd;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Inventory.instance.AddItem(_itemInventoryReference, quantityToAdd);
            Destroy(gameObject);
        }
    }
}
