﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

public class TankController : MonoBehaviour {

	public float maxSpeed = 2f;

	private Rigidbody2D rBody;
	private SpriteRenderer sRend;
	private Animator animator;

	private float moveH;
	private float initialSpeed;

	private bool isFired = false;
	
	public Transform groundCheck;
	public LayerMask defineGround;

	public GameObject bombPrefab;
	public Transform cannon;

	public bool playerActivated = false;
	public bool tankFacingRight = true;

	public GameObject gameManager;

	private int health = 100;
	
	// Use this for initialization
	void Start ()
	{
		rBody = GetComponent<Rigidbody2D>();
//		sRend = GetComponent<SpriteRenderer>();
		animator = GetComponent<Animator>();
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
		}
	}
	
	
	
	void OnCollisionEnter2D(Collision2D col)
	{
		
		if(col.gameObject.name == "Fuel")
		{
			// Refuel tank
			Debug.Log("Tank1 refueled by 20");
			Destroy(col.gameObject);

			health += 20;
		}		
		
//		Check if hit by enemy tank
		if (col.gameObject.CompareTag("Bomb"))
		{

			Debug.Log("Hit by bomb");
				
			health -= 20;
			gameManager.SendMessage("UpdateHealth", this.gameObject);
			
		}
	}
	

	void Fire(float speed)
	{
		Debug.Log("Firing, initial speed = " + speed.ToString());

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
	}
	


	void Activate()
	{
		playerActivated = true;
		isFired = false;
	}

	void Deactivate()
	{
		playerActivated = false;
	}

	void SetTankDirectionToLeft()
	{
		tankFacingRight = false;
	}

	public int GetHealth()
	{
		return health;
	}
}