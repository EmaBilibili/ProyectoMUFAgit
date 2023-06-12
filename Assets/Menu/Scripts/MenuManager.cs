using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject pauseMenuCanvas;

    private bool isPaused = false;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

    void TogglePauseMenu()
    {
        isPaused = !isPaused;
        pauseMenuCanvas.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;

        Cursor.visible = isPaused; // Mostrar u ocultar el cursor
        Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked; // Desbloquear o bloquear el cursor
    }

    public void PlayGame()
    {
        Debug.Log("cargando juego");
        SceneManager.LoadScene("MainScene");
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ExitGame()
    {
        Debug.Log("Salir del juego");
        Application.Quit();
    }

    public void PlayMenu()
    {
        Debug.Log("Regresando al Menu ");
        SceneManager.LoadScene("MainMenu");
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

    }
}
