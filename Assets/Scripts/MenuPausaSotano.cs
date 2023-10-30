using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausaSotano : MonoBehaviour
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

    public void TogglePauseMenu()
    {
        isPaused = !isPaused;
        pauseMenuCanvas.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;

        Cursor.visible = isPaused; // Mostrar u ocultar el cursor
        Debug.Log("cursor visible");
        Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked; // Desbloquear o bloquear el cursor
        Debug.Log("cursor desbloqueado");
    }

    public void PlayGame()
    {
        Debug.Log("cargando juego");
        SceneManager.LoadScene("MainScene");
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    public void PlaySotano()
    {
        Debug.Log("cargando juego");
        SceneManager.LoadScene("sotano");
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
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

    }

    public void PlayOptions()
    {
        Debug.Log("Ir a Opciones");
        SceneManager.LoadScene("Options");
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    
    public void PlayCredits()
    {
        Debug.Log("Ir a Creditos");
        SceneManager.LoadScene("FinalStory");
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}