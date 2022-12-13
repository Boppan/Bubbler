using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFan : MonoBehaviour
{
    private BoxCollider2D boxCollider2D;
    private SpriteRenderer spriteRenderer;
    public Sprite greenButton;
    public GameObject fan;
    public GameObject windAnimation;
    // public GameObject laser;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        
        if (collider.gameObject.tag == "Player")
        {

            FindObjectOfType<AudioManager>().PLay("ButtonPressed");

            spriteRenderer.sprite = greenButton;
            fan.GetComponentInChildren<WindBlow>().FanDisable();
           // laser.GetComponentInChildren<LaserTurnOff>().LaserDisable();


            Destroy(boxCollider2D);
            Destroy(this);
            Destroy(windAnimation);


        }
    }
}

