using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera camera_01;
    [SerializeField] private CinemachineVirtualCamera camera_02;
    [SerializeField] private CinemachineVirtualCamera camera_03;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && gameObject.tag == "Activate_Camera_02")
        {
            if (camera_01.Priority > camera_02.Priority )
            {
                camera_01.Priority = camera_01.Priority - 1;
                camera_02.Priority = camera_02.Priority + 1;
                camera_03.Priority = 9;
            }
            else
            {
                camera_01.Priority = camera_01.Priority + 1;
                camera_02.Priority = camera_02.Priority - 1;
                camera_03.Priority = 9;
            }

        }
        else
        {
            camera_01.Priority = camera_01.Priority - 1;
            camera_02.Priority = camera_02.Priority - 1;
            camera_03.Priority = 12;
        }
    }
}
