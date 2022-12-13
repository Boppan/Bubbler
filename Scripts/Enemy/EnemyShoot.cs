using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{

    public float speed;
    public float stoppingDistance;
    public float retreatDistance;
    public float startTimeBtwShots;

    public int healthPoints = 10;

    private float timeBtwShots;
    

    [SerializeField]
    float agroRange;

    public GameObject deathEffect;
    public GameObject projectile;
    public Transform playerTransform;
    private PlayerState player;
    private Animator animator;

    private AudioManager audioManager;


    

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        player = playerTransform.GetComponent<PlayerState>();

        animator = gameObject.GetComponent<Animator>();

        audioManager = FindObjectOfType<AudioManager>();

        timeBtwShots = startTimeBtwShots;
    }

    // Update is called once per frame
    void Update()
    {
        float distToPlayer = Vector2.Distance(transform.position, playerTransform.position);
        if (distToPlayer < agroRange)
        {
            
            if (Vector2.Distance(transform.position, playerTransform.position) > stoppingDistance)
            {
                
                transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
            }
            else if (Vector2.Distance(transform.position, playerTransform.position) < stoppingDistance && Vector2.Distance(transform.position, playerTransform.position) > retreatDistance)
            {
                transform.position = this.transform.position;
            }
            else if (Vector2.Distance(transform.position, playerTransform.position) < retreatDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, -speed * Time.deltaTime);
            }

            if (timeBtwShots <= 0)
            {
                audioManager.PLay("WhenEnemyShoot");
                animator.SetTrigger("hasShot");
                Instantiate(projectile, transform.position, Quaternion.identity);
                timeBtwShots = startTimeBtwShots;
            }
            else
            {
                timeBtwShots -= Time.deltaTime;
            }
        }
    }

    public void TakeDamage(int takeDamageBythisMuch)
    {
        audioManager.PLay("EnemyDamage");
        healthPoints -= takeDamageBythisMuch;
        if (healthPoints <= 0)
        {
            audioManager.PLay("EnemyDie");
            Die();
        }

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
       if(player.canKillEnemies == true)
        {
            TakeDamage(player.damage);
        }
    }
    void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
