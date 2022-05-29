using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float hambre;
    private float maxHambre = 100;
    public bool puedeAlimentarse;
    
    public float estamina;
    private float maxEstamina = 150;
    public bool puedeRecuperarEstamina;

    [SerializeField] private bool isInBoat;
    [SerializeField] private bool SetActiveBoat;
    [SerializeField] private GameObject Boat;

    private float diferencia;

    private float x;
    private float y;
    private float z;
    private void Awake()
    {
        //estamina = maxEstamina;
        //hambre = maxHambre;
    }

    private void Update()
    {
        PuedeAlimentarse();
        PuedeRecuperarEstamina();
        if (isInBoat && Input.GetKeyDown(KeyCode.G) && !SetActiveBoat)
        {
            Boat.GetComponent<ShipMovement>().enabled = true;
            Boat.gameObject.GetComponent<ShipFloatMovement>().enabled = true;
            gameObject.GetComponent<PlayerMovement>().enabled = false;
            gameObject.transform.parent = Boat.transform;
            Boat.transform.GetChild(0).GetComponent<CinemachineVirtualCamera>().Priority = 14;
            SetActiveBoat = true;
        }
    }

    private void PuedeAlimentarse()
    {
        if (hambre < maxHambre)
        {
            puedeAlimentarse = true;
        }
        
        else
        {
            puedeAlimentarse = false;
        }
    }

    private void PuedeRecuperarEstamina()
    {
        if (estamina < maxEstamina)
        {
            puedeRecuperarEstamina = true;
        }
        
        else
        {
            puedeRecuperarEstamina = false;
        }
    }

    public void Tamaño(float tamaño)
    {
        x = tamaño;
        y = tamaño;
        z = tamaño;
        transform.localScale = new Vector3(x, y, z);
    }
    
    public void alimentarse(float cantidad)
    {
        diferencia = maxHambre - hambre;
        if (hambre < maxHambre)
        {
            if (cantidad < diferencia)
            {
                hambre = hambre + cantidad;
            }
        
            else
            {
                hambre = hambre + diferencia; 
            }
        }
        
        else
        {
            return;
        }
    }
    
    public void recuperarEstamina(float cantidad)
    {
        diferencia = maxEstamina - estamina;
        if (estamina < maxEstamina)
        {
            if (cantidad < diferencia)
            {
                estamina = estamina + cantidad;
            }
            else
            {
                estamina = estamina + diferencia; 
            }
        }
        else
        {
            return;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        isInBoat = false;
        if (hit.gameObject.tag ==  "Water")
        {
            Debug.Log("Hola agua");
            //PlayerMovement.Instance._animator.SetTrigger("IsDrawned");
        }
        if (hit.gameObject.tag ==  "Ship" && !isInBoat)
        {
            isInBoat = true;
            Debug.Log("Hola barco");
        }
    }
}
