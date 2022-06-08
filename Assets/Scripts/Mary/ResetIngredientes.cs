using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetIngredientes : MonoBehaviour
{
    [SerializeField] private GameObject objectToRespawn;
    [SerializeField] private bool isRespawnActive;

    // Update is called once per frame
    void Update()
    {
        if (TimeManager.instance.isNight)
        {
            if (!objectToRespawn.active && !isRespawnActive)
            {
                objectToRespawn.SetActive(true);
                isRespawnActive = true;
            }
        }
        else
        {
            isRespawnActive = false;
        }
    }
}
