using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PowerUp : MonoBehaviour
{

    public GameObject explodeEffect;
    public float duration = 3f;
    
    


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            FindObjectOfType<AudioManager>().PLay("PowerUp");

            StartCoroutine (Pickup(collision));
        }
    }

    IEnumerator Pickup(Collider2D player)
    {
        Instantiate(explodeEffect, transform.position, transform.rotation);

        PlayerState state = player.GetComponent<PlayerState>();
        state.canKillEnemies = true;
        state.bubbleAnimator.SetBool("activePowerup", true);

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;

        yield return new WaitForSeconds(duration);

        state.canKillEnemies = false;
        state.bubbleAnimator.SetBool("activePowerup", false);

        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<CircleCollider2D>().enabled = true;

    }
}
