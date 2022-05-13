using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float hambre;
    private float maxHambre;
    
    public float estamina;
    private float maxEstamina;

    private float diferencia;
    private void Awake()
    {
        estamina = maxEstamina;
        hambre = maxHambre;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void alimentarse(float cantidad)
    {
        diferencia = maxHambre - hambre;
        if (hambre < maxHambre)
        {
            if (cantidad < diferencia)
            {
                hambre += hambre + cantidad;
            }
            else
            {
                hambre += hambre + diferencia; 
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
                estamina += estamina + cantidad;
            }
            else
            {
                estamina += estamina + diferencia; 
            }
        }
        else
        {
            return;
        }
    }
}
