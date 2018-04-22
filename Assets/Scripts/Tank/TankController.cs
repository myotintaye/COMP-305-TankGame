using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;
using UnityEngineInternal;

public class TankController : MonoBehaviour {

	public float maxSpeed = 1f;
	public float rotationSpeed = 10f;

    public Transform canvas;

	private Rigidbody2D rBody;
	private SpriteRenderer sRend;
	private Animator animator;

	private float moveH;
	private float initialSpeed = 10f;

	private bool isFireCooledDown = true;
	
	public LayerMask defineGround;

	public Transform cannon;
	public Transform bombPlaceholder;

	public bool playerActivated = false;
	public bool tankFacingRight;

	public GameObject gameManager;
	public GameObject bombPrefab;
	public GameObject tooltipTextPrefab;
	public GameObject shieldProtectionPrefab;

	public int bombChance = 0;
	private int health = 40;
	private int maxHealth = 60;
    private int damage = 20;
	private bool dealth = false;

	public Sprite deadImage;
	public Slider healthBar;
	
	public Image overlay;

	private Vector3 cannonRotation;

	private AudioSource _audioSource;
	
 	
	
	// Use this for initialization
	void Start ()
	{

	}

	private void Awake()
	{
		rBody = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		_audioSource = GetComponent<AudioSource>();

		_audioSource.enabled = false;
		shieldProtectionPrefab.SetActive(false);
		
		// Lower the center of mass to avoid rotation issues
		rBody.centerOfMass = new Vector3(0, -1, 0);

	}

	// Update is called once per frame
	void Update () {

		if (Input.GetKey(KeyCode.Space) && bombChance > 0 && playerActivated && isFireCooledDown)
		{
			Fire(initialSpeed);
		}

		/* Rotate cannon */
		if (Input.GetKey(KeyCode.UpArrow) && playerActivated)
		{
			
			cannonRotation = cannon.localEulerAngles;
			
			if (cannon.localEulerAngles.z < 45f)
			{
				cannonRotation.z += rotationSpeed * Time.deltaTime;
			}
			else
			{
				cannonRotation.z = 45f;	
			}			

	
			cannon.localEulerAngles = cannonRotation;
			/* Adjust bomb placeholder pivot rotation */
			cannonRotation.z *= -1;
			bombPlaceholder.localEulerAngles = cannonRotation;

		}
		
		if (Input.GetKey(KeyCode.DownArrow) && playerActivated)
		{
			
			cannonRotation = cannon.localEulerAngles;
			
			if (cannon.localEulerAngles.z > 5f)
			{
				cannonRotation.z -= rotationSpeed * Time.deltaTime;
			}
			else
			{
				cannonRotation.z = 5f;	
			}

			cannon.localEulerAngles = cannonRotation;
			
			/* Adjust bomb placeholder pivot rotation */
			cannonRotation.z *= -1;
			bombPlaceholder.localEulerAngles = cannonRotation;
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
	
	void OnCollisionEnter2D(Collision2D col)
	{
		switch (col.gameObject.tag)
		{
			case "Fuel":
				_audioSource.enabled = true;
				_audioSource.Play();
			
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

				break;
			
			case "Bomb":

				if (!isDead())
				{
					if (shieldProtectionPrefab.active)
					{
						shieldProtectionPrefab.SetActive(false);
						SpawnTooltip(col, "Shield Destroyed");
					}
					else
					{
						health -= damage;
						healthBar.value -= damage;
				
						SpawnTooltip(col, "Damage, -" + damage.ToString());

						checkHealth();						
				
						/* Call game manager to update UI panel */
						gameManager.SendMessage("UpdateHealth", this.gameObject);				
					}
				}
				
				break;
			
			case "Timer":
				_audioSource.enabled = true;
				_audioSource.Play();
			
				/* Call game manager to add time */
				gameManager.SendMessage("AddTime");
			
				Destroy(col.gameObject);

				break;
			
			case "Shield":
				
				Destroy(col.gameObject);
				
				_audioSource.enabled = true;
				_audioSource.Play();
			
				shieldProtectionPrefab.SetActive(true);
				SpawnTooltip(col, "Shield Protection");
			

				break;
			
			case "ExtraBomb":
				_audioSource.enabled = true;
				_audioSource.Play();
				
				bombChance += 1;
		
				/* Call game manager to update UI panel */
				gameManager.SendMessage("UpdateBombChance", this.gameObject);
				
				SpawnTooltip(col, "Bomb +1");
			
				Destroy(col.gameObject);

				break;
			
			default:
				break;
				
		}
		
	}

    private void SpawnTooltip(Collision2D col, String msg)
    {
        var tooltipText = Instantiate(tooltipTextPrefab, transform.position, Quaternion.identity, canvas) as GameObject;

	    tooltipText.transform.position = col.transform.position;
      
        var text = tooltipText.GetComponent<Text>();
        text.text = msg;

        Destroy(tooltipText, 3f);
    }
	

    void Fire(float speed)
	{
		
		GameObject obj = GameObject.Instantiate(bombPrefab, bombPlaceholder.position, Quaternion.identity) as GameObject;
		BombController bomb = obj.GetComponent<BombController>();
		

		Vector3 bombPlaceholderAngle = bombPlaceholder.localEulerAngles;
		bombPlaceholderAngle.z = 360 - bombPlaceholderAngle.z;
		
		obj.transform.localEulerAngles = bombPlaceholderAngle;

		
		/* Rotation range (5, 45) degrees */
		float angle = Mathf.Deg2Rad * (cannon.localEulerAngles.z);
		float xSpeed = initialSpeed * Mathf.Cos(angle);
		float ySpeed = initialSpeed * Mathf.Sin(angle);
		

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
		bombChance += 1;
		rBody.constraints = RigidbodyConstraints2D.None;

		overlay.enabled = false;

	}

	void Deactivate()
	{
		playerActivated = false;
		rBody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
		
		overlay.enabled = true;
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
