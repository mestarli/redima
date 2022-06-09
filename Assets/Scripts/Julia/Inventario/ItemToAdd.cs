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
    
    [SerializeField] private GameObject panelShow;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.R))
        {
            Inventory.instance.AddItem(_itemInventoryReference, quantityToAdd);
            try
            {
                panelShow = GameObject.FindGameObjectWithTag("ShowPanelDescription");
                panelShow.SetActive(false);
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine(e);
            }
        
            //Destroy(gameObject);
            if (gameObject.transform.parent)
            {
                gameObject.SetActive(false);
            }
            else
            {
                Destroy(gameObject);
            }
            
            if (_itemInventoryReference.type == ItemTypes.Ingredients)
            {
                Inventory.instance.Collected = true;
            }
        }
    }
}
