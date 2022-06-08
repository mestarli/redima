using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowInstructions : MonoBehaviour
{
    public static ShowInstructions Instance;
    public GameObject panelShow;
    [SerializeField] private Text textDetras;
    [SerializeField] private Text tecla;
    [SerializeField] private string tecla_text;
    [SerializeField] private string textDetras_text;

    private void Awake()
    {
        Instance = this;
    }

    private void OnTriggerStay(Collider collider)
    {
        textDetras.text = textDetras_text;
        tecla.text = tecla_text;
        if(collider.tag == "Player")
        {
            panelShow.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        if(collider.tag == "Player")
        {
            panelShow.SetActive(false);
        }
    }
}
