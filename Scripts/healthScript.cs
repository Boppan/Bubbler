using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthScript : MonoBehaviour
{

    public PlayerState playerState;
    [SerializeField] private Animator animator;
    
    [SerializeField] private Image healthImage;

    private int playerHealth;
    private int initialHP;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        
        playerHealth = playerState.healthPoints;
        initialHP = playerState.initialHP;
    }

   

    public void healthUpdate()
    {
        playerHealth = playerState.healthPoints;
        healthImage.fillAmount = (float) playerHealth / initialHP;
        
    }
}
