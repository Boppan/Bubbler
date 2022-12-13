using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindBlow : MonoBehaviour
{
    [SerializeField] [Range(0f, 1000f)] 
    private float windstrength = 1000f;

    bool isTurnedOff;

    [SerializeField] private string bubble = "Player";
    private Rigidbody2D rigidBody;
    private AudioManager audioManager;
    private Animator animator;
    private AlienSling aS;

    private void Start()
    {
        isTurnedOff = false; 
        rigidBody = FindObjectOfType<PlayerState>().GetComponent<Rigidbody2D>();
        audioManager = FindObjectOfType<AudioManager>();
        animator = GetComponentInParent<Animator>();
        aS = FindObjectOfType<AlienSling>();
        
    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
        if (aS.swipe == false && collision.gameObject.tag == "Player")
        {
            aS.gyroCD = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (aS.swipe == false && collision.gameObject.tag == "Player")
        {
            aS.gyroCD = 0.2f;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (isTurnedOff = false||collision.tag == bubble)
        {
            audioManager.PLay("Wind"); 
            rigidBody.AddForce(transform.up * windstrength * Time.deltaTime);
        }
    }
    public void FanDisable()
    {

        isTurnedOff = true;
        animator.SetTrigger("TurnedOff");
        FindObjectOfType<AudioManager>().PLay("LaserOff");

        windstrength = 0f;

    }
}
