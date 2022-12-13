using UnityEngine;

public class BlackholeGravity : MonoBehaviour
{
    [SerializeField] 
    [Range(0f, 20f)] 
    
    public float attractForce = 0.5f;
    private Rigidbody2D bubbleRB2D;
    private Transform bubbleTransform;
    private float offset = 7f;

    void Start()
    {
        bubbleRB2D = FindObjectOfType<AlienSling>().GetComponentInParent<Rigidbody2D>();
        bubbleTransform = bubbleRB2D.transform;
    }

    private void FixedUpdate()
    {
        Attract();
    }

    private void Attract()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, bubbleTransform.position);
        if (distanceToPlayer <= offset)
        {
            Vector2 direction = this.transform.position - bubbleTransform.position;
            bubbleRB2D.AddForce(direction * attractForce);
        }
    }

      private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, offset);
    }
}
