using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMainMenu : MonoBehaviour
{
   private void Awake()
   {
      AudioManager.instance.PlaySong("");
   }

   public void PlayGame()
   {
      AudioManager.instance.PlaySong("UISeleccion");
      SceneManager.LoadScene("MainScene");
   }
   
   public void Quit()
   {
      AudioManager.instance.PlaySong("UIClick");
      Application.Quit();
   }
}
