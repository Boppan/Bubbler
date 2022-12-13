using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneWindBlow : MonoBehaviour
{
    [SerializeField] [Range(0f, 1000f)] 
    private float windstrength = 1000f;

    
    
    
    private Rigidbody2D rb;

    private void Start()
    {
        
        rb = FindObjectOfType<CutScene>().GetComponent<Rigidbody2D>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
       
        rb.AddForce(transform.up * windstrength * Time.deltaTime);
        
    }
   
}
