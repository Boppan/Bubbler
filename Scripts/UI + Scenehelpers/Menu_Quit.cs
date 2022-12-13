using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Quit : MonoBehaviour
{
    private AudioManager audioManager;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();

    }
    public void QuitGame()
    {
        audioManager.PLay("ButtonKlick");
        Application.Quit();
    }
}
