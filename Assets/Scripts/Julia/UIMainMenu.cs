using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMainMenu : MonoBehaviour
{
   private void Start()
   {
      AudioManager.instance.PlaySong("Musica_MenuPrincipal");
   }

   public void PlayGame()
   {
      AudioManager.instance.PlaySong("UISeleccion");
      AudioManager.instance.StopSong("Musica_MenuPrincipal");
      SceneManager.LoadScene("MainScene");
   }
   
   public void Quit()
   {
      AudioManager.instance.PlaySong("UIClick");
      Application.Quit();
   }
}
