using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraGranja : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cameraGranja;
    [SerializeField] private CinemachineFreeLook freeLookComponent;

    void OnTriggerStay(Collider collider)
    {
        if(collider.tag == "Player")
        {
            cameraGranja.Priority = 14;
            freeLookComponent.Priority = 10;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if(collider.tag == "Player")
        {
            cameraGranja.Priority = 10;
            freeLookComponent.Priority = 14;
        }
    }
}
