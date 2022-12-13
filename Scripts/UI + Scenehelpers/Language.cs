using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Language : MonoBehaviour
{
    public string defaultText = "Default text";
    Text currentText;

    private void Start()
    {
        currentText = GetComponent<Text>();

    }

    private void Update()
    {
        currentText.text = GameLanguage.gl.Say(defaultText);
    }

}
