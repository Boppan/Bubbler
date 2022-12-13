using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    private AudioManager audioManager;
    private AlienSling aS;
    private WorldMap worldMap;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        aS = FindObjectOfType<AlienSling>();
        worldMap = FindObjectOfType<WorldMap>();
    }

    public void Resume()
    {
        audioManager.PLay("ButtonKlick");
        pauseMenu.SetActive(false);
        aS.ResetGyro();
        worldMap.enabled = true;
        Time.timeScale = 1f;
    }

   public void Pause()
    {
        audioManager.PLay("ButtonKlick");
        pauseMenu.SetActive(true);
        worldMap.enabled = false;
        Time.timeScale = 0f;
    }

   public void HomeMenu(int sceneID)
    {
        audioManager.PLay("ButtonKlick");
        Time.timeScale = 1f;
        PlayerPrefs.SetInt("SavedScene", SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(sceneID);
    }

    public void QuitGame()
    {
        audioManager.PLay("ButtonKlick");
        PlayerPrefs.SetInt("SavedScene", SceneManager.GetActiveScene().buildIndex);
        Application.Quit();
    }


}