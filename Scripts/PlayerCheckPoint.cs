using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckPoint : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player") == true)
        {
            collider.GetComponent<PlayerState>().ChangeRespawnPosition(gameObject);
            Destroy(this);
        }
    }
}
