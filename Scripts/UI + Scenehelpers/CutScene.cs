using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScene : MonoBehaviour
{

    [SerializeField] private GameObject startPosition;
    [SerializeField] private bool useStartPosition = true;

    bool isGettingSmaller = false;

    private AudioManager audioManager;

    private Rigidbody2D rigidBody;

    private Animator alienAnimator;

    private BoxCollider2D boxCollider2D;

    public Animator poofAnimator;

    
    

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        alienAnimator = GetComponentInChildren<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        
        
        audioManager = FindObjectOfType<AudioManager>();

        if (useStartPosition == true)
        {
            audioManager.PLay("WindBlow");
            gameObject.transform.position = startPosition.transform.position;
        }
    }

    private void Update()
    {
        if (isGettingSmaller == true)
        {
            transform.localScale = new Vector3(transform.localScale.x - 0.4f * Time.deltaTime, transform.localScale.y - 0.4f * Time.deltaTime, transform.localScale.z) /** Time.deltaTime*/;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("SmallerArea") == true)
        {
            isGettingSmaller = true;
        } else if (collision.gameObject.CompareTag("PoofArea") == true)
        {
            poofAnimator.SetTrigger("Poof");
            audioManager.PLay("Poof");
        }
        
    }


}
