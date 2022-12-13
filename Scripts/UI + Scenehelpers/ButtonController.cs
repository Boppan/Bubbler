using UnityEngine;

public class ButtonController : MonoBehaviour
{
    private CircleCollider2D cc2D;
    private SpriteRenderer sR;
    public Sprite greenButton;
    public GameObject laser;
    // Start is called before the first frame update
    void Start()
    {
        cc2D = GetComponent<CircleCollider2D>();
        sR = GetComponent<SpriteRenderer>();
    }

	public void OnTriggerEnter2D(Collider2D collider)
	{
        if (collider.gameObject.tag == "Player")
        {
            FindObjectOfType<AudioManager>().PLay("ButtonPressed");

            sR.sprite = greenButton;
            laser.GetComponentInChildren<LaserTurnOff>().LaserDisable();
            
            Destroy(cc2D);
            Destroy(this);
            
        }
	}
}
