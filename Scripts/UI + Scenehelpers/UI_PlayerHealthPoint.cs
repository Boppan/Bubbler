using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerHealthPoint : MonoBehaviour
{

    public PlayerState playerState;

    private Slider slider;

    int playerHealthPoints;

    void Start()
    {
        playerHealthPoints = playerState.initialHP;
        slider = gameObject.GetComponent<Slider>();
        slider.wholeNumbers = true;
        slider.maxValue = playerHealthPoints;

    }

    void Update()
    {
        slider.value = playerState.healthPoints;
    }
}
