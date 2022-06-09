using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pokedex : MonoBehaviour
{

    [SerializeField] private Image image_pokedex;

    [SerializeField] private string title_pokedex_txt;

    [SerializeField] private Text title_pokedex;

    [SerializeField] private string text_pokedex_txt;

    [SerializeField] private Text text_pokedex;

    [SerializeField] private Image item_pokedex;

    // Start is called before the first frame update
    public void showInfo()
    {
        if (gameObject.transform.GetChild(1).gameObject.tag == "Active")
        {
            item_pokedex.gameObject.SetActive(true);
            item_pokedex.sprite = image_pokedex.sprite;
            title_pokedex.text = title_pokedex_txt;
            text_pokedex.text = text_pokedex_txt;
        }
    }
}    
