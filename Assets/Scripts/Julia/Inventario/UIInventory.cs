using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    // Variables
    [Header("Inventory Description Panel")]
    //[SerializeField] private GameObject inventoryDescriptionPanel;
    [SerializeField] private Image itemIcon;
    [SerializeField] private Text itemName;
    [SerializeField] private Text itemDescription;

    public static UIInventory instanceInventoryUI;
    
    [SerializeField] private InventorySlot slotPrefab;
    [SerializeField] private Transform inventoryContent;
    
    private List<InventorySlot> availableSlots = new List<InventorySlot>();

    public List<InventorySlot> AvailableSlots => availableSlots;

    public InventorySlot SelectedSlot { get; private set; }

    private void Awake()
    {
        instanceInventoryUI = this;
        //inventoryDescriptionPanel.gameObject.SetActive(false);
    }

    void Start()
    {
        // Llamada del metodo para crear los slots del inventario
        InitializeInventory();
    }

    private void Update()
    {
        // Llamada del metodo que actualiza la info del slot seleccionado
        UpdateSelectedSlot();
    }

    // Metodo que sirve para crear los slots del inventario
    private void InitializeInventory()
    {
        for (int i = 0; i < Inventory.instance.SlotsNum; i++)
        {
            InventorySlot newSlot = Instantiate(slotPrefab, inventoryContent);
            newSlot.Index = i;
            availableSlots.Add(newSlot);
        }
    }

    // Metodo para actualizar la informacion del slot que hemos seleccionado
    private void UpdateSelectedSlot()
    {
        GameObject goSeleccionado = EventSystem.current.currentSelectedGameObject;

        if (goSeleccionado == null)
        {
            return;
        }

        InventorySlot slot = goSeleccionado.GetComponent<InventorySlot>();

        if (slot != null)
        {
            SelectedSlot = slot;
        }
    }

    // Metodo para mostrar el objeto que se ha recogido en la UI del inventario
    public void DrawItemInInventory(ItemInventory itemToAdd, int quantity, int itemIndex)
    {
        InventorySlot slot = availableSlots[itemIndex];
        
        if (itemToAdd != null)
        {
            slot.ActivateSlotUI(true);
            slot.UpdateSlotUI(itemToAdd, quantity);
        }

        else
        {
            slot.ActivateSlotUI(false);
        }
    }
    
    // Metodo que al hacer click sobre un slot nos muestra informacion de este
    private void UpdateDescriptionInventory(int index)
    {
        if (Inventory.instance.ItemsInventory[index] != null)
        {
            itemIcon.sprite = Inventory.instance.ItemsInventory[index].icon;
            itemName.text = Inventory.instance.ItemsInventory[index].name;
            itemDescription.text = Inventory.instance.ItemsInventory[index].description;
            //inventoryDescriptionPanel.SetActive(true);
        }

        else
        {
            //inventoryDescriptionPanel.SetActive(false);
        }
    }

    // Metodo para usar un item
    public void UseItem()
    {
        if (SelectedSlot != null)
        {
            SelectedSlot.UseItemSlot();
            SelectedSlot.SelectSlot();
        }
    }

    // Metodo para equipar un item
    public void EquipItem()
    {
        if (SelectedSlot != null)
        {
            SelectedSlot.EquipItemSlot();
            SelectedSlot.SelectSlot();
            Inventory.instance.isEquipped = true;
        }
    }
    
    #region Events

    // Diferentes respuestas al interactuar con los slots
    private void SlotInteractionResponse(InteractionTypes type, int index)
    {
        if (type == InteractionTypes.Click)
        {
            UpdateDescriptionInventory(index);
        }
    }
    
    public void OnEnable()
    {
        InventorySlot.SlotInteractionEvent += SlotInteractionResponse;
    }

    public void OnDisable()
    {
        InventorySlot.SlotInteractionEvent -= SlotInteractionResponse;

    }

    #endregion
}
