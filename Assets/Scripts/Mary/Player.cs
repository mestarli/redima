using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    public float hambre;
    private float maxHambre = 100;
    public bool puedeAlimentarse;
    
    public float estamina;
    private float maxEstamina = 150;
    public bool puedeRecuperarEstamina;
    public bool cursorEnabled;

    [SerializeField] private bool isInBoat;
    [SerializeField] private bool SetActiveBoat;
    [SerializeField] private GameObject Boat;
    [SerializeField] private GameObject BoatColliders;

    private float diferencia;

    private float x;
    private float y;
    private float z;
    
    [SerializeField] private Vector3 posicionPlayer;
    [SerializeField] private Vector3 posicionBarco;
    private void Awake()
    {
        Instance = this;
        //estamina = maxEstamina;
        //hambre = maxHambre;
        posicionPlayer = gameObject.transform.position;
        posicionBarco = Boat.transform.position;
        //to hide the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        PuedeAlimentarse();
        PuedeRecuperarEstamina();
        if (isInBoat && Input.GetKeyDown(KeyCode.G) && SetActiveBoat)
        {

            Boat.GetComponent<ShipMovement>().enabled = false;
            Boat.gameObject.GetComponent<ShipFloatMovement>().enabled = false;
            gameObject.GetComponent<PlayerMovement>().enabled = true;
            gameObject.transform.parent = null;
            PlayerMovement.Instance._animator.SetLayerWeight(1, 0);
            Boat.transform.GetChild(0).GetComponent<CinemachineVirtualCamera>().Priority = 10;
            BoatColliders.SetActive(false);
            StartCoroutine(ResetExitBoat(false));
        }

        if (isInBoat && Input.GetKeyDown(KeyCode.G) && !SetActiveBoat)
        {
            Boat.GetComponent<ShipMovement>().enabled = true;
            Boat.gameObject.GetComponent<ShipFloatMovement>().enabled = true;
            gameObject.GetComponent<PlayerMovement>().enabled = false;
            gameObject.transform.parent = Boat.transform;
            gameObject.transform.position = Boat.transform.GetChild(1).transform.position;
            gameObject.transform.rotation = Boat.transform.GetChild(1).transform.rotation;
            BoatColliders.SetActive(true);
            Boat.transform.GetChild(0).GetComponent<CinemachineVirtualCamera>().Priority = 14;
            PlayerMovement.Instance._animator.SetLayerWeight(1, 1);
            StartCoroutine(ResetExitBoat(true));
        }

        // Habilitaremos el cursor con la tecla alt izquierda
        if (!cursorEnabled && Input.GetKeyDown(KeyCode.LeftAlt))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            gameObject.GetComponent<CameraController>().enabled = false;
            StartCoroutine(ResetCursor(true));
        }else if (cursorEnabled && Input.GetKeyDown(KeyCode.LeftAlt))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            gameObject.GetComponent<CameraController>().enabled = true;
            StartCoroutine(ResetCursor(false));
            cursorEnabled = false;
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
            PlayerMovement.Instance._animator.SetTrigger("IsDrawned");
            gameObject.GetComponent<PlayerMovement>().enabled = false;
            StartCoroutine(ResetPlayerToHomeFromWater());
        }
        if (hit.gameObject.tag ==  "Ship" && !isInBoat)
        {
            isInBoat = true;
        }
    }
    
    IEnumerator ResetPlayerToHomeFromWater()
    {
      
        yield return new WaitForSeconds(2f);
        gameObject.transform.position = posicionPlayer;
        Boat.transform.position = posicionBarco;
        PlayerMovement.Instance._animator.ResetTrigger("IsDrawned");
        PlayerMovement.Instance._animator.SetTrigger("Default");
        yield return new WaitForSeconds(0.01f);
        gameObject.GetComponent<PlayerMovement>().enabled = true;
        PlayerMovement.Instance._animator.ResetTrigger("Default");
    }
    
    IEnumerator ResetExitBoat(bool estaBarco)
    {
      
        yield return new WaitForSeconds(2f);
        SetActiveBoat = estaBarco;
    }
    
    IEnumerator ResetCursor(bool enable)
    {
      
        yield return new WaitForSeconds(0.2f);
        cursorEnabled  = enable;
    }
    
    
}
