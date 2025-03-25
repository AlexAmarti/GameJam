using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void StartGame()
    {
        Debug.Log("Cargando escena GENERAL...");
        SceneManager.LoadScene("GENERAL"); // Asegúrate de que el nombre esté tal cual
    }

    public void QuitGame()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }
    public void OpenCredits()
    {
        Debug.Log("Abriendo créditos...");
        SceneManager.LoadScene("CREDITOS");
    }
}
