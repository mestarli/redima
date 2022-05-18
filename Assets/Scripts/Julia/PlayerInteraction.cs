using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    // Variables
    private PlayerMovement _playerMovement;
    
    // Video 21:55
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
    }

    void OnInteractableHit(RaycastHit hit)
    {
        Collider other = hit.collider;

        // Miramos si el player esta interactuando con la tierra donde se puede plantar
        if (other.tag == "Farmland")
        {
            // Accedemos al script de land cuando el raycast choque
            Land land = other.GetComponent<Land>();

            land.Select(true);
        }
    }
}
