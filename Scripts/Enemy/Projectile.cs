using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public int damage = 1;

    private Transform player;
    private Vector2 target;
    private PlayerState playerState;

    private void Start()
    {
        playerState = FindObjectOfType<PlayerState>();
        player = playerState.transform;
        target = new Vector2(player.position.x, player.position.y);
    }


    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if(transform.position.x == target.x && transform.position.y == target.y)
        {
            DestroyProjectile();
        }
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.CompareTag("Player"))
        {
            DestroyProjectile();
            playerState.TakeDamage(damage);
        } 
        if (hitInfo.CompareTag("Wall"))
        {
            DestroyProjectile();
        }
        
    }


    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
