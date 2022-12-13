using UnityEngine;

public class Gravity : MonoBehaviour
{
    [SerializeField] [Range(0f, 20f)]public float attractForce = 5f;
    public Rigidbody2D bubbleRB2D;
    private Rigidbody2D alienRB2D;
    private Transform bubbleTransform;
    private Transform alienTransform;
    // Start is called before the first frame update
    void Start()
    {
        alienRB2D = FindObjectOfType<AlienSling>().GetComponent<Rigidbody2D>();
        bubbleTransform = bubbleRB2D.transform;
        alienTransform = alienRB2D.transform;
    }

	private void FixedUpdate()
	{
        Attract();
	}

	private void Attract()
    {
        Vector2 direction = bubbleTransform.position - alienTransform.position;
        alienRB2D.AddForce(direction.normalized * attractForce);
    }
}
