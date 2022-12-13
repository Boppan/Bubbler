using UnityEngine;
using UnityEngine.UI;

public class GyroButton : MonoBehaviour
{
    public int activationNumber = 0;
    private int activated = 1;
    private int deactivated = 2;
    private Image image;
    private Sprite offSprite;
    public Sprite onSprite;
    private AudioManager audioManager;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();

    }

    private void Awake()
	{
        image = GetComponent<Image>();
        offSprite = image.sprite;
        activationNumber = PlayerPrefs.GetInt("controllerNumber");
        if (activationNumber != deactivated && activationNumber != activated)
        {
            activationNumber = deactivated;
        }
        

        if (activationNumber == activated)
        {
            image.sprite = onSprite;
        }
        else
        {
            image.sprite = offSprite;
        }
    }

	public void OnClickGyro()
    {
        if (activationNumber == activated)
        {
            audioManager.PLay("ButtonKlick");

            activationNumber = deactivated;
            image.sprite = offSprite;
        }
        else
        {
            audioManager.PLay("ButtonKlick");

            activationNumber = activated;
            image.sprite = onSprite;
        }
    }
}
