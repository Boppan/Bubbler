using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{

    public float speed;
    public int damage = 10;
    public int healthPoints = 10;

    public GameObject deathEffect;

    private PlayerState player;


    private Transform target;

    [SerializeField]
    float agroRange;

    private Animator animator;

    private AudioManager audioManager;




    // Start is called before the first frame update
    void Start()
    {
        

        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        player = target.GetComponent<PlayerState>();

        animator = gameObject.GetComponent<Animator>();

        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        float distToPlayer = Vector2.Distance(transform.position, target.position);

        if(distToPlayer < agroRange) 
        {
            
            animator.SetBool("InAggroRange", true);
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            
        } else
        {
            animator.SetBool("InAggroRange", false);
        }

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player") == true)
        {
            if (player.canKillEnemies == true)
            {
                TakeDamage(player.damage);
            }
            else
            {
                player.TakeDamage(damage);
                audioManager.PLay("EnemyFollowAttack");
                animator.SetTrigger("hasHitPlayer");
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
    void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }


}
