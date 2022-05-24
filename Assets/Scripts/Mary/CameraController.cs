using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera camera_01;
    [SerializeField] private CinemachineVirtualCamera camera_02;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (camera_01.Priority > camera_02.Priority )
            {
                camera_01.Priority = camera_01.Priority - 1;
                camera_02.Priority = camera_02.Priority + 1;
            }
            else
            {
                camera_01.Priority = camera_01.Priority + 1;
                camera_02.Priority = camera_02.Priority - 1;
            }

        }
    }
}
