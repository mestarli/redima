using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    // Variables
    private PlayerMovement _playerMovement;
    private Land _selectedLand = null;
    void Start()
    {
        // Accedemos al script del player
        _playerMovement = transform.parent.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1))
        {
            OnInteractableHit(hit);
        }
        
        AdvanceTime();
    }
    
    // Metodo cuando el raycast hit choca con algo que es interactuable
    void OnInteractableHit(RaycastHit hit)
    {
        Collider other = hit.collider;

        // Miramos si el player esta interactuando con la tierra donde se puede plantar
        if (other.tag == "Farmland")
        {
            // Accedemos al script de land cuando el raycast choque
            Land land = other.GetComponent<Land>();
            SelectLand(land);
            return;
        }
        
        // Deseleccionar la tierra si el player no está encima de ninguna tierra interactuable
        if (_selectedLand != null)
        {
            _selectedLand.Select(false);
            _selectedLand = null;
        }
    }
    
    // Selecciona las casillas o trozos de tierra donde se puede plantar
    void SelectLand(Land land)
    {
        // Si hay algun trozo de tierra seleccionado que se desactive previamente
        if (_selectedLand != null)
        {
            _selectedLand.Select(false);
        }

        // Establecer la nueva tierra seleccionada en la tierra que estamos seleccionando ahora 
        _selectedLand = land;
        land.Select(true);
    }

    // Metodo que se activa cuando el player presiona la tecla 
    public void Interact()
    {
        // Revisar si estamos seleccionando un trozo de tierra o no
        if (_selectedLand != null)
        {
            _selectedLand.Interact();
            return;
        }
    }

    public void AdvanceTime()
    {
        if (Input.GetKey(KeyCode.T))
        {
            TimeManager.instance.Tick();
        }
    }
}
