using UnityEngine;
using System.Collections;
using UnityEngine.Animations;


public class AlienSling : MonoBehaviour
{
	private Rigidbody2D alienBody;
	private Rigidbody2D bubble;
	private PlayerState pS;
	private WorldMap worldMap;

	private Animator animator;


	private float offSet = 2.2f;

	private Vector2 startPos;
	private Vector2 endPos;
	private Vector2 direction;
	private float touchTimeStart;
	private float touchTimeFinish;
	private float timeInterval;
	private float animatorCD = 3f;

	private float timer;

	private int controllerNumber;
	private bool mouseController = false;
	public bool swipe = false;

	private bool isGyroCD;
	private float gyroTimer;
	public float gyroCD = 0.2f;

	private float gyroDirectionX;
	private float gyroDirectionY;
	private Vector2 gyroStartPostion;

	public float throwForce = 60f;
	private AudioManager audioM;

	// Start is called before the first frame update
	void Start()
	{
		worldMap = FindObjectOfType<WorldMap>();
		alienBody = GetComponent<Rigidbody2D>();
		bubble = alienBody.GetComponentInChildren<Rigidbody2D>();
		animator = GetComponent<Animator>();
		audioM = FindObjectOfType<AudioManager>();
		pS = GetComponentInParent<PlayerState>();
		controllerNumber = PlayerPrefs.GetInt("controllerNumber");
		if (controllerNumber == 0 || controllerNumber == 2)
		{
			swipe = true;
		}
		else 
		{
			swipe = false;
			ResetGyro();
		}
	}


	// Update is called once per frame
	void Update()
	{
		float distanceToPlayer = Vector3.Distance(transform.localPosition, Vector3.zero);
		if (distanceToPlayer > offSet)
        {
			ResetPostion();
        }

		if (Input.GetKey(KeyCode.M))
		{
			mouseController = true;
		}

		if (!mouseController && !pS.dead)
		{
			if (swipe && Input.touchCount > 0)
			{
				if (Input.GetTouch(0).phase == TouchPhase.Began)
				{
					startPos = Input.GetTouch(0).position;
					touchTimeStart = Time.time;
				}

				if (Input.GetTouch(0).phase == TouchPhase.Ended)
				{
					endPos = Input.GetTouch(0).position;
					touchTimeFinish = Time.time;
					direction = startPos - endPos;
					direction.Normalize();
					timeInterval = touchTimeStart - touchTimeFinish;
					if (timeInterval != 0)
					{
						if (timeInterval > 0.25f)
						{
							audioM.PLay("Swipe");
						}
						alienBody.velocity = Vector3.zero;
						bubble.velocity = Vector3.zero;
						alienBody.AddForce((direction / timeInterval) * throwForce);
						animator.SetInteger("number", 2);
					}
				}
			}
			else if (!swipe && !pS.dead)
			{
				if (!isGyroCD && worldMap.image.enabled == true)
				{
					gyroDirectionX = (Input.acceleration.x - gyroStartPostion.x) * throwForce * Time.deltaTime;
					gyroDirectionY = (Input.acceleration.y - gyroStartPostion.y) * throwForce * Time.deltaTime;
					alienBody.velocity = new Vector2(alienBody.velocity.x + gyroDirectionX, alienBody.velocity.y + gyroDirectionY);
					animator.SetInteger("number", 2);
				}
				else
				{
					gyroTimer += Time.deltaTime;
					if (gyroTimer >= gyroCD)
					{
						gyroTimer = 0;
						isGyroCD = false;
					}
				}
			}
		}
		else if(mouseController && !pS.dead)
		{
			if (Input.GetMouseButtonDown(0))
			{
				touchTimeStart = Time.time;
				startPos = Input.mousePosition;
			}

			if (Input.GetMouseButtonUp(0))
			{
				touchTimeFinish = Time.time;
				endPos = Input.mousePosition;
				direction = startPos - endPos;
				direction.Normalize();
				timeInterval = touchTimeStart - touchTimeFinish;
				if (timeInterval != 0)
				{
					audioM.PLay("Swipe");
					bubble.velocity = Vector3.zero;
					alienBody.velocity = Vector3.zero;
					alienBody.AddForce((direction / timeInterval) * throwForce);
					animator.SetInteger("number", 2);
				}
			}
		}
		if (animator.GetInteger("number") == 2)
        {
			timer += Time.deltaTime;
			if (timer >= animatorCD)
			{
				timer = 0;
				animator.SetInteger("number", 1);
			}
		}
		else
        {
			timer = 0;
        }

	}

	public void OnCollisionEnter2D(Collision2D collision)
	{
		if (!mouseController && !swipe && collision.gameObject.tag == "Player")
		{
			isGyroCD = true;
			animator.SetInteger("number", 1);
		}
	}

	public void ResetGyro()
	{
		if (!mouseController && !swipe)
		{
			gyroStartPostion = new Vector2(Input.acceleration.x, Input.acceleration.y);
		}
	}


	public void ResetPostion()
	{
		
		alienBody.velocity = Vector3.zero;
		alienBody.angularVelocity = 0;
		alienBody.transform.localPosition = Vector3.zero;
		alienBody.transform.localRotation = Quaternion.identity;
	}


	public void ResetBubble()
    {
		bubble.velocity = Vector3.zero;
		bubble.angularVelocity = 0;
    }

	public IEnumerator Wait()
    {
		yield return new WaitForSeconds(0.5f);
		yield return null;
    }
}
