using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ItemToAdd : MonoBehaviour
{
    /// <summary>
    /// Script que esta en los objetos que se pueden recoger para que se muestren en la ui del inventario.
    /// Al entrar en contacto de tipo trigger con el tag Player, mandamos la informacion que le hemos puesto por
    /// el inspector de unity. En este caso le pasamos la referencia de un scriptable object y la cantidad que se
    /// tiene que añadir. Luego el objeto se destruye.
    /// </summary>
    
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

            if (_itemInventoryReference.type == ItemTypes.Ingredients)
            {
                Inventory.instance.Collected = true;
            }
        }
    }
}
