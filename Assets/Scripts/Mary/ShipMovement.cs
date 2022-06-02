using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (ShipFloatMovement))]
public class ShipMovement : MonoBehaviour
{
    // Variables
    [Header("Componentes")]
    [SerializeField] private Rigidbody rb;

    [Header("Variables")]
    [Space(15)]
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float rotationSpeed = 200.0f;
    private float x, y;
    void Awake()
    {

        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Llamada de los métodos de movimiento, ataque con espada y ataque con libro
        BoatMovement();
    }
    private void BoatMovement()
    {
        // Funciones
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        // Movimiento player
        transform.Translate(y * Time.deltaTime * speed, 0, 0);
        
        // Rotación player
        transform.Rotate(0, x * Time.deltaTime * rotationSpeed, 0);
    }
}
