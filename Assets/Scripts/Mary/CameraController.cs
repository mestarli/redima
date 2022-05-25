using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraController : MonoBehaviour
{
    [SerializeField] private List<CinemachineVirtualCamera> cameras;
    [SerializeField] private CinemachineVirtualCamera activeCamera;
    [SerializeField] private string tagTrigger;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tagTrigger = gameObject.tag;
            Debug.Log("Tag "+tagTrigger);
            foreach (var camera in cameras)
            {
                Debug.Log("Tag de la camara "+camera.tag);
                if (camera.tag.Contains(tagTrigger) && camera.tag != tagTrigger)
                {
                    if (camera.Priority == 10)
                    {
                        camera.Priority = 12;
                        continue;
                    }
                    if (camera.Priority == 12)
                    {
                        camera.Priority = 10;
                        continue;
                    }
                }
                if (camera.tag.Contains(tagTrigger) && camera.tag == tagTrigger)
                {
                    if (camera.Priority == 10)
                    {
                        camera.Priority = 12;
                        continue;
                    }
                    if (camera.Priority == 12)
                    {
                        camera.Priority = 10;
                        continue;
                    }
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tagTrigger = gameObject.tag;
            Debug.Log("Tag Exit"+tagTrigger);
            foreach (var camera in cameras)
            {
                if (camera.tag.Contains(tagTrigger) && camera.tag != tagTrigger)
                {
                    if (camera.Priority == 10)
                    {
                        camera.Priority = 12;
                        continue;
                    }
                    if (camera.Priority == 12)
                    {
                        camera.Priority = 10;
                        continue;
                    }
                }
                if (camera.tag.Contains(tagTrigger) && camera.tag == tagTrigger)
                {
                    if (camera.Priority == 10)
                    {
                        camera.Priority = 12;
                        continue;
                    }
                    if (camera.Priority == 12)
                    {
                        camera.Priority = 10;
                        continue;
                    }
                }
            }
        }
    }
}
