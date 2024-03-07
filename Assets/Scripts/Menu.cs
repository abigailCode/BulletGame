using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{


    //START

    private void Start()
    {
        AudioManager.instance.PlayMusic("MainTheme");
    }

    public void StartGame()
    {
        AudioManager.instance.PlaySFX("Boton");
        SceneController.instance.LoadScene("SampleScene");
    }

    //MENU
    public void GoToMenu()
    {
        AudioManager.instance.PlaySFX("Boton");
        SceneController.instance.LoadScene("Menu");
    }

    //QUIT
    public void QuitGame()
    {
        AudioManager.instance.PlaySFX("Boton");
        Application.Quit();
    }


}
