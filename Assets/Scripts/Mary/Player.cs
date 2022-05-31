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
    
    [SerializeField] private Vector3 posicionPlayer;
    private void Awake()
    {
        //estamina = maxEstamina;
        //hambre = maxHambre;
        posicionPlayer = gameObject.transform.position;
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
            Debug.Log("Esta tocando el agua");
            PlayerMovement.Instance._animator.SetTrigger("IsDrawned");
            gameObject.GetComponent<PlayerMovement>().enabled = false;
            StartCoroutine(ResetPlayerToHome());
        }
        if (hit.gameObject.tag ==  "Ship" && !isInBoat)
        {
            isInBoat = true;
            Debug.Log("Hola barco");
        }
    }
    
    IEnumerator ResetPlayerToHome()
    {
      
        yield return new WaitForSeconds(2f);
        gameObject.transform.position = posicionPlayer;
        PlayerMovement.Instance._animator.ResetTrigger("IsDrawned");
        PlayerMovement.Instance._animator.SetTrigger("Default");
        yield return new WaitForSeconds(0.01f);
        gameObject.GetComponent<PlayerMovement>().enabled = true;
        PlayerMovement.Instance._animator.ResetTrigger("Default");
    }
    
}
