using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTurnOff : MonoBehaviour
{
    
    public SpriteRenderer laser;
    private BoxCollider2D boxCollider;
    private CircleCollider2D circleCollider;
    private AudioManager audioManager;

    public void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        boxCollider = GetComponentInChildren<BoxCollider2D>();
        circleCollider = GetComponent<CircleCollider2D>();
    }

    public void LaserDisable()
    {
        FindObjectOfType<AudioManager>().PLay("LaserOff");

        laser.enabled = false;
        boxCollider.enabled = false;
        circleCollider.enabled = false;
        audioManager.STop("Laser");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            audioManager.LOop("Laser");
            audioManager.PLay("Laser");

        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            audioManager.LOop("Laser");
        }
        
    }

}
