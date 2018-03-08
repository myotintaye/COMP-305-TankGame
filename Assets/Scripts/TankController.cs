using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour {

	public float maxSpeed = 0.8f;

	private Rigidbody2D rBody;
	private SpriteRenderer sRend;
	private Animator animator;

	private float moveH;
	private bool isRight = true;
	
	// Use this for initialization
	void Start ()
	{
		rBody = GetComponent<Rigidbody2D>();
//		sRend = GetComponent<SpriteRenderer>();
//		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void FixedUpdate()
	{

		// Read input
		moveH = Input.GetAxis("Horizontal");

//		// Set speed variable in animator
//		animator.SetFloat("Speed", Mathf.Abs(moveH));

		// Set character velocity
		rBody.velocity = new Vector2(moveH * maxSpeed, rBody.velocity.y);
		
	}
}
