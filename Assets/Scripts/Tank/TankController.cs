using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngineInternal;

public class TankController : MonoBehaviour {

	public float maxSpeed = 1f;
	public float rotationSpeed = 10f;

    public Transform canvas;
    public GameObject tooltipTextPrefab;

	private Rigidbody2D rBody;
	private SpriteRenderer sRend;
	private Animator animator;

	private float moveH;
	private float initialSpeed = 10f;

	private bool isFireCooledDown = true;
	
//	public Transform groundCheck;
	public LayerMask defineGround;

	public GameObject bombPrefab;
	public Transform cannon;
	public Transform bombPlaceholder;

	public bool playerActivated = false;
	public bool tankFacingRight;

	public GameObject gameManager;

	private int health = 40;
	private int maxHealth = 60;
	public int bombChance = 1
		;
    private int damage = 20;
	private bool dealth = false;

	public Sprite deadImage;
	public Slider healthBar;

	private Vector3 cannonRotation;
	
 
	
	// Use this for initialization
	void Start ()
	{

		
	}

	private void Awake()
	{
		rBody = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		
		// Lower the center of mass to avoid rotation issues
		rBody.centerOfMass = new Vector3(0, -1, 0);

//		if (!tankFacingRight)
//		{
//			Debug.Log("Flip() in Initialization" + this.ToString());
//			Flip();
//		}		
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetKey(KeyCode.Space) && bombChance > 0 && playerActivated && isFireCooledDown)
		{
//			initialSpeed  = Input.GetAxis("Jump") * 5;
			
			Fire(initialSpeed);
		}

		/* Rotate cannon */
		if (Input.GetKey(KeyCode.UpArrow) && playerActivated)
		{
			
			cannonRotation = cannon.localEulerAngles;
				
//			Debug.Log("cannon = " + cannon.localEulerAngles.z.ToString());
			
			if (cannon.localEulerAngles.z < 45f)
			{
				cannonRotation.z += rotationSpeed * Time.deltaTime;
			}
			else
			{
				cannonRotation.z = 45f;	
			}			

//			if (tankFacingRight)
//			{
//				if (cannon.localEulerAngles.z < 45f)
//				{
//					cannonRotation.z += rotationSpeed * Time.deltaTime;
//				}
//				else
//				{
//					cannonRotation.z = 45f;	
//				}			
//			}
//			else
//			{
//				if (cannon.localEulerAngles.z > -45f)
//				{
//					cannonRotation.z -= rotationSpeed * Time.deltaTime;
//				}
//				else
//				{
//					cannonRotation.z = -45f;	
//				}					
//			}
	
			cannon.localEulerAngles = cannonRotation;
			/* Adjust bomb placeholder pivot rotation */
			cannonRotation.z *= -1;
			bombPlaceholder.localEulerAngles = cannonRotation;

		}
		
		if (Input.GetKey(KeyCode.DownArrow) && playerActivated)
		{
			
			cannonRotation = cannon.localEulerAngles;
			
//			Debug.Log("cannon = " + cannon.localEulerAngles.z.ToString());
			
			if (cannon.localEulerAngles.z > 5f)
			{
				cannonRotation.z -= rotationSpeed * Time.deltaTime;
			}
			else
			{
				cannonRotation.z = 5f;	
			}

//			if (tankFacingRight)
//			{
//				if (cannon.localEulerAngles.z > 5f)
//				{
//					cannonRotation.z -= rotationSpeed * Time.deltaTime;
//				}
//				else
//				{
//					cannonRotation.z = 5f;	
//				}
//			}
//			else
//			{
//				if (cannon.localEulerAngles.z < -5f)
//				{
//					cannonRotation.z += rotationSpeed * Time.deltaTime;
//				}
//				else
//				{
//					cannonRotation.z = -5f;	
//				}			
//			}


			cannon.localEulerAngles = cannonRotation;
			
			/* Adjust bomb placeholder pivot rotation */
			cannonRotation.z *= -1;
			bombPlaceholder.localEulerAngles = cannonRotation;
			
//			Debug.Log("bombPlaceholder = " + bombPlaceholder.transform.localEulerAngles.z.ToString());
			
		}
	}
	
	void FixedUpdate()
	{

		if (playerActivated)
		{
		
			// Read input
			moveH = Input.GetAxis("Horizontal");

			// Set character velocity
			rBody.velocity = new Vector2(moveH * maxSpeed, 0);	
			
//			RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 1f, defineGround);
//
//			if (hit.collider != null && Mathf.Abs(hit.normal.x) > 0.1f)
//			{
//				rBody.velocity = new Vector2(rBody.velocity.x - (hit.normal.x * 0.2f), rBody.velocity.y);
//				Debug.Log("Enter normalize, velocity = " + rBody.velocity.ToString());
//			}
			
			// Check direction and flip sprite
			if (moveH > 0 && !tankFacingRight)
			{
				Flip();
			}
			else if (moveH < 0 && tankFacingRight)
			{
				Flip();
			}

		}
	}
	
	void Flip()
	{
		Vector3 temp = this.transform.localScale;
		temp.x *= -1;
		this.transform.localScale = temp;

		/* Don't flip the health bar */
		Vector3 tempHealthBar = this.transform.Find("Canvas").transform.localScale;
		Transform healthBarTransform = this.transform.Find("Canvas").transform;
		tempHealthBar.x *= -1;
		healthBarTransform.localScale = tempHealthBar;
		
		tankFacingRight = !tankFacingRight;
	}
	
//	// @NOTE Must be called from FixedUpdate() to work properly
//	void NormalizeSlope () {
//		// Attempt vertical normalization
//
//		bool grounded = true;
//		
//		if (grounded) {
//			RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 1f, defineGround);
//		
//			if (hit.collider != null && Mathf.Abs(hit.normal.x) > 0.1f) {
//				Rigidbody2D body = GetComponent<Rigidbody2D>();
//				// Apply the opposite force against the slope force 
//				// You will need to provide your own slopeFriction to stabalize movement
//				body.velocity = new Vector2(body.velocity.x - (hit.normal.x * 0.2f), body.velocity.y);
//
//				//Move Player up or down to compensate for the slope below them
//				Vector3 pos = transform.position;
//				pos.y += -hit.normal.x * Mathf.Abs(body.velocity.x) * Time.deltaTime * (body.velocity.x - hit.normal.x > 0 ? 1 : -1);
//				transform.position = pos;
//			}
//		}
//	}
//	
	
	void OnCollisionEnter2D(Collision2D col)
	{
		
		if(col.gameObject.CompareTag("Fuel"))
		{
			// Refuel tank
			Destroy(col.gameObject);

			if (health + 20 < maxHealth)
			{
				health += 20;			
			}
			else
			{
				health = maxHealth;
			}

			healthBar.value = health;
			
			SpawnTooltip(col, "Refuel, +20");
			
			/* Call game manager to update UI panel */
			gameManager.SendMessage("UpdateHealth", this.gameObject);
		}		
		
//		Check if hit by enemy tank
		if (col.gameObject.CompareTag("Bomb"))
		{

			if (!isDead())
			{
				health -= damage;
				healthBar.value -= damage;
				
				SpawnTooltip(col, "Damage, -" + damage.ToString());

				checkHealth();						
				
				/* Call game manager to update UI panel */
				gameManager.SendMessage("UpdateHealth", this.gameObject);
			}
		}
	}

    private void SpawnTooltip(Collision2D col, String msg)
    {
        var tooltipText = Instantiate(tooltipTextPrefab,transform.position, Quaternion.identity, canvas) as GameObject;

	    tooltipText.transform.position = col.transform.position;
      
        var text = tooltipText.GetComponent<Text>();
        text.text = msg;

        Destroy(tooltipText, 3f);
    }
	

    void Fire(float speed)
	{
		
		GameObject obj = GameObject.Instantiate(bombPrefab) as GameObject;
		BombController bomb = obj.GetComponent<BombController>();
		
		obj.transform.position = bombPlaceholder.position;

		Vector3 bombPlaceholderAngle = bombPlaceholder.localEulerAngles;
		bombPlaceholderAngle.z = 360 - bombPlaceholderAngle.z;
		
		obj.transform.localEulerAngles = bombPlaceholderAngle;

//		Debug.Log("bomb angle = " + obj.transform.localEulerAngles.z.ToString());
		
		
		/* Rotation range (5, 45) degrees */
		float angle = Mathf.Deg2Rad * (cannon.localEulerAngles.z);
		float xSpeed = initialSpeed * Mathf.Cos(angle);
		float ySpeed = initialSpeed * Mathf.Sin(angle);
		
		
//		Debug.Log("------ Firing ------" + bombChance.ToString());
//		Debug.Log("angle = " + angle.ToString());
//		Debug.Log("xSpeed = " + xSpeed.ToString());
//		Debug.Log("ySpeed = " + ySpeed.ToString());

		if (tankFacingRight)
		{	
			
			obj.GetComponent<Rigidbody2D>().velocity = new Vector2(xSpeed, ySpeed);
		}
		else
		{
			/* Flip the bomb */
			Vector2 newScale = obj.transform.localScale;
			newScale.x *= -1;
			obj.transform.localScale = newScale;

			xSpeed *= -1;
			obj.GetComponent<Rigidbody2D>().velocity = new Vector2(xSpeed, ySpeed);
		}
		
		bombChance -= 1;
		isFireCooledDown = false;
		
		/* Call game manager to update UI panel */
		gameManager.SendMessage("UpdateBombChance", this.gameObject);
		
		/* Start Cool Down */
		Invoke("FireCoolDown", 1);
	}


	void FireCoolDown()
	{
		isFireCooledDown = true;
	}

	void checkHealth()
	{
		if (health <= 0)
		{
			/* Died */
			dealth = true;

			/* Replace sprite renderer */
			gameObject.GetComponent<SpriteRenderer>().sprite = deadImage;
			
			/* Adjust position because of different sprite image */
			Vector3 tankPosition = gameObject.transform.position;
			tankPosition.y -= 0.3f;
			gameObject.transform.position = tankPosition;
			
			/* Call game manager to update UI panel */
			gameManager.SendMessage("UpdateBombChance", this.gameObject);
			gameManager.SendMessage("UpdateDealthInfo", this.gameObject);
			
			/* Hide health bar */
			healthBar.gameObject.SetActive(false);
			
			/* Hide cannon */
			cannon.gameObject.SetActive(false);

		}
	}

	
	void Activate()
	{
		playerActivated = true;
		bombChance = 1;
		rBody.constraints = RigidbodyConstraints2D.None;

	}

	void Deactivate()
	{
		playerActivated = false;
		bombChance = 0;
		rBody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
	}

	void SetTankDirectionToLeft()
	{
		Debug.Log("SetTankDirectionToLeft()" + this.ToString());
		Flip();
		tankFacingRight = false;
	}
	
	
		
	public int GetHealth()
	{
		return health;
	}

	public int GetBombChance()
	{
		return bombChance;
	}

	public bool isDead()
	{
		return dealth;
	}

}
