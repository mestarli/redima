using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowInstructions : MonoBehaviour
{
    [SerializeField] private GameObject panelShow;
    [SerializeField] private Text textDetras;
    [SerializeField] private Text tecla;
    [SerializeField] private string tecla_text;
    [SerializeField] private string textDetras_text;

    private void OnTriggerEnter(Collider collider)
    {
        textDetras.text = textDetras_text;
        tecla.text = tecla_text;
        if(collider.tag == "Player")
        {
            panelShow.SetActive(true);
            StartCoroutine(ResetPanel());
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        if(collider.tag == "Player")
        {
            panelShow.SetActive(false);
        }
    }
    
    IEnumerator ResetPanel()
    {
      
        yield return new WaitForSeconds(2f);
        panelShow.SetActive(false);
    }
}
