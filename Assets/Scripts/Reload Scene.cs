using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReloadScene : MonoBehaviour
{
    public Button restartButton; // Asigna este botón en el Inspector

    void Start()
    {
        if (restartButton != null)
        {
            restartButton.onClick.AddListener(ReloadGeneralScene);
        }
    }

    void ReloadGeneralScene()
    {
        SceneManager.LoadScene("GENERAL"); // Reinicia la escena completamente
    }
}


