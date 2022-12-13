using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class NextLevel : MonoBehaviour
{
    [SerializeField] int levelToLoad;
    public bool realLevel = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") == true)
        {

            FindObjectOfType<AudioManager>().PLay("NextLevel");

            if (realLevel)
            {
                TimerController.instance.EndTimer();
            }

            SceneManager.LoadScene(levelToLoad);
            //if (collision.GetComponent<QuestComplete>().isQuestComplete == true) {
            // n?got m?l


            //}

        }
    }
}
