using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        if (player.InputHandler.PauseInput)
        {
            player.InputHandler.UsePauseInput();

            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        pauseMenuUI.SetActive(false);
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void MainMenu()
    {
        // Return to main menu\
        player.saveGame();
        StartCoroutine(ChangeScene());
        GameIsPaused = false;
    }

    IEnumerator ChangeScene()
    {
        yield return SceneManager.LoadSceneAsync("TitleScreen");
    }

    public void Quit()
    {
        player.saveGame();
        GameIsPaused = false;
        Application.Quit();
    }

}
