using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public int damage = 5;

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player") == true)
        {
          
            collision.gameObject.GetComponent<PlayerState>().TakeDamage(damage);
        }
        else if (collision.gameObject.CompareTag("Enemy") == true)
        {
            if (collision.gameObject.GetComponent<EnemyFollow>())
            {
                collision.gameObject.GetComponent<EnemyFollow>().TakeDamage(damage);
            }
            else{
                collision.gameObject.GetComponent<EnemyShoot>().TakeDamage(damage);
            }
        }
    }
}
