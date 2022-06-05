using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaroController : MonoBehaviour
{
    // Variables
    public GameObject faro;
    public float angleRotation;
    
    // Update is called once per frame
    void Update()
    {
        UpdateFaro();
    }

    public void UpdateFaro()
    {
        faro.transform.Rotate(0, angleRotation * Time.deltaTime, 0);
    }
}
