using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMainMenu : MonoBehaviour
{
   
   public void PlayGame()
   {
      SceneManager.LoadScene("MainScene");
   }
   
   public void Quit()
   {
      Application.Quit();
   }
}
