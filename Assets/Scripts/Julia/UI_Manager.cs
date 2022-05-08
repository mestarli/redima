using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Manager : MonoBehaviour
{
    // Variables
    
    [Header("PANELS")]
    [SerializeField] private GameObject inventoryPanel;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Panels

    public void OpenCloseInventoryPanel()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
    }

    #endregion
}
