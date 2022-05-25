using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraController : MonoBehaviour
{
    [SerializeField] private List<CinemachineVirtualCamera> cameras;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
        
        }
    }
}
