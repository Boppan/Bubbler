 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;


public class PlayerState : MonoBehaviour
{
    public int healthPoints = 10;
    public int initialHP = 10;

    private AlienSling alienSling;

    public bool canKillEnemies = false;

    public int damage = 4;

    private healthScript healthScript;

    private Animator alienAnimator;

    public Animator bubbleAnimator;

    private Transform alienTransform;

    private Animator healthAnimator;


    [SerializeField]
    private GameObject respawnPosition;

    [SerializeField] private GameObject startPosition;
    [SerializeField] private bool useStartPosition = true;
    public bool dead = false;

    public GameObject bubbleBurstParticle;

    private AudioManager audioM;

    [SerializeField] private float thrust;

    private Rigidbody2D rb;
    private float dmgCD = 0.25f;
    private float dmgTimer;


    void Start()
    {
        healthAnimator = FindObjectOfType<healthScript>().GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        bubbleAnimator = GetComponent<Animator>();
        alienAnimator = FindObjectOfType<AlienSling>().GetComponent<Animator>();
        alienAnimator.SetInteger("number", 1);
        healthScript = FindObjectOfType<healthScript>();
        alienSling = GetComponentInChildren<AlienSling>();
        healthPoints = initialHP;
        alienTransform = alienSling.GetComponent<Transform>();
        audioM = FindObjectOfType<AudioManager>();
        if (useStartPosition == true)
        {
            gameObject.transform.position = startPosition.transform.position;
        }
        respawnPosition = startPosition;
        dmgTimer = dmgCD;

        TimerController.instance.BeginTime();
    }

	private void Update()
	{
        if (dead == true)
        {
            alienTransform.localScale = new Vector3(alienTransform.localScale.x - 0.265f * Time.deltaTime, alienTransform.localScale.y - 0.265f * Time.deltaTime, alienTransform.localScale.z) /** Time.deltaTime*/;
        }

        if (dmgTimer < dmgCD)
        {
            dmgTimer += Time.deltaTime;
        }
	}


	public void TakeDamage(int takeDamageBythisMuch)
    {

        if ((canKillEnemies == false && dmgTimer >= dmgCD) || takeDamageBythisMuch == 100)
        {

            audioM.PLay("PlayerTakeDamage");

            healthPoints -= takeDamageBythisMuch;
            ShakeOnDamage.Instance.ShakeCamera(2.5f, .1f);
            healthScript.healthUpdate();
            healthAnimator.SetTrigger("TakeDamage");
            canKillEnemies = false;
            bubbleAnimator.SetBool("activePowerup", false);


            if (healthPoints <= 0)
            {
                Die();
            }

        }

   

    void Die()
        {
            audioM.PLay("PlayerDeath");
            dead = true;
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<EdgeCollider2D>().enabled = false;
            GetComponentInChildren<CircleCollider2D>().enabled = false;
            GetComponentInChildren<Rigidbody2D>().velocity = Vector2.zero;
            GetComponentInChildren<Rigidbody2D>().isKinematic = true;
            GameObject bubbleBurstParticleClone = Instantiate(bubbleBurstParticle, transform.position, Quaternion.identity);

            Destroy(bubbleBurstParticleClone, 2);
            alienAnimator.SetInteger("number", 0);

            Respawn();

        }

    }

    //knockback när vi åker på fienden
    private void OnCollisionStay2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Enemy") && canKillEnemies == false)

        {
            Rigidbody2D enemy = collision.gameObject.GetComponent<Rigidbody2D>();
            Vector2 difference = transform.position - enemy.transform.position;
            difference = difference.normalized * thrust;
            rb.AddForce(difference, ForceMode2D.Impulse);
        }
        
    }

  

    public void ChangeRespawnPosition(GameObject newRespawnPosition)
    {
        respawnPosition.transform.position = newRespawnPosition.transform.position;
    }

    public void Respawn()
    {
        StartCoroutine(WaitBeforeRespawn());

    }


    IEnumerator WaitBeforeRespawn()
    {
        yield return new WaitForSeconds(2);

        audioM.PLay("Respawn");

        healthPoints = initialHP;
        gameObject.transform.position = respawnPosition.transform.position;
        dead = false;
        alienTransform.localScale = new Vector3(0.53f, 0.53f, alienTransform.localScale.z);


        alienSling.ResetBubble();
        alienSling.Wait();
        alienSling.ResetPostion();
        healthScript.healthUpdate();
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<EdgeCollider2D>().enabled = true;
        GetComponentInChildren<CircleCollider2D>().enabled = true;
        GetComponentInChildren<Rigidbody2D>().isKinematic = false;
        alienAnimator.SetInteger("number", 1);
    }

}
