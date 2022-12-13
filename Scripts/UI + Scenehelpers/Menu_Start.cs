using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Start : MonoBehaviour
{
    private AudioManager audioManager;


    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void ButtonBounce()
    {
    }
    public void StartGame()
    {
        audioManager.PLay("ButtonKlick");
        int i = FindObjectOfType<GyroButton>().activationNumber;
        PlayerPrefs.SetInt("controllerNumber", i);
        SceneManager.LoadScene(1);
    }

    public void LoadGame()
    {
        audioManager.PLay("ButtonKlick");
        int i = FindObjectOfType<GyroButton>().activationNumber;
        PlayerPrefs.SetInt("controllerNumber", i);
        SceneManager.LoadScene(PlayerPrefs.GetInt("SavedScene"));
    }
}