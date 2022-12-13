using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLanguage : MonoBehaviour
{
    private AudioManager audioManager;

    public static GameLanguage gl;
    public string currentLanguage = "en";

    Dictionary<string, string> langID;

    void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        gl = this;
        
        if (PlayerPrefs.HasKey("GameLanguage"))
        {
            currentLanguage = PlayerPrefs.GetString("GameLanguage");
        }
        WordDefine();
    }

    public void SetLanguage(string langCode)
    {
        audioManager.PLay("ButtonKlick");
        PlayerPrefs.SetString("GameLanguage", langCode);
        currentLanguage = langCode;
    }

    public void ResetLanguage()
    {
        SetLanguage("en");
    }

    public string Say(string text)
    {
        switch (currentLanguage)
        {
            case "se":
                return FindInDict(langID, text);
            default :
                return text;
        }
    }

    public string FindInDict(Dictionary<string, string> selectedLang, string text)
    {
        if (selectedLang.ContainsKey(text))
            return selectedLang[text];
        else
           return "Untranslated";
    }

    public void WordDefine()
    {
        langID = new Dictionary<string, string>()
        {
           // {"English", "Engelska"},
           // {"Swedish", "Svenska"},
            {"Play", "Spela"},
            {"Quit", "Avsluta"},
            {"Pause", "Paus"},
            {"Menu", "Meny"},
            {"Resume", "Fortsätt"},
            {"World Map", "Världskarta"},
            {"Paused", "Pausad"},
            {"New Game", "Nytt Spel"},
            {"Load Game", "Ladda Spel"},
            {"Highscore", "Rekord"},
            {"Time", "Tid"}
        };
    }
}
