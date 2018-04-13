using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngineInternal;

public class TankController : MonoBehaviour {

	public float maxSpeed = 1f;

    public Transform canvas;
    public GameObject tooltipTextPrefab;

	private Rigidbody2D rBody;
	private SpriteRenderer sRend;
	private Animator animator;

	private float moveH;
	private float initialSpeed;

	private bool isFired = false;
	
//	public Transform groundCheck;
	public LayerMask defineGround;

	public GameObject bombPrefab;
	public Transform cannon;

	public bool playerActivated = false;
	public bool tankFacingRight = true;

	public GameObject gameManager;

	private int health = 40;
	private int bombChance = 0;
    private int damage = 20;
	private bool dealth = false;

	public Sprite deadImage;
 
	
	// Use this for initialization
	void Start ()
	{
		rBody = GetComponent<Rigidbody2D>();
//		sRend = GetComponent<SpriteRenderer>();
		animator = GetComponent<Animator>();
		
		// Lower the center of mass to avoid rotation issues
		rBody.centerOfMass = new Vector3(0, -1, 0);
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey(KeyCode.Space) && isFired == false && playerActivated == true)
		{
			initialSpeed  = Input.GetAxis("Jump") * 5;
			
			Fire(initialSpeed);
			isFired = true;
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

			health += 20;
			
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
				
				SpawnTooltip(col, "Damage, -20");

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
		
		obj.transform.position = cannon.position;

		if (tankFacingRight)
		{	
			obj.GetComponent<Rigidbody2D>().velocity = new Vector2(1f, 1f) * initialSpeed;
		}
		else
		{
			/* Flip the bomb */
			Vector2 newScale = obj.transform.localScale;
			newScale.x *= -1;
			obj.transform.localScale = newScale;
			
			obj.GetComponent<Rigidbody2D>().velocity = new Vector2(-1f, 1f) * initialSpeed;
		}
		
		bombChance -= 1;
		
		/* Call game manager to update UI panel */
		gameManager.SendMessage("UpdateBombChance", this.gameObject);
	}
	


	void checkHealth()
	{
		if (health <= 0)
		{
			/* Died */
			dealth = true;

			/* Replace sprite renderer */
			gameObject.GetComponent<SpriteRenderer>().sprite = deadImage; 
			
			/* Call game manager to update UI panel */
			gameManager.SendMessage("UpdateBombChance", this.gameObject);
			gameManager.SendMessage("UpdateDealthInfo", this.gameObject);
			
		}
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
	
	
	void Activate()
	{
		playerActivated = true;
		isFired = false;
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
		tankFacingRight = false;
	}
}
