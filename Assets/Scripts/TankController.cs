using System.Collections;
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
	
	// Use this for initialization
	void Start ()
	{
		rBody = GetComponent<Rigidbody2D>();
//		sRend = GetComponent<SpriteRenderer>();
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey(KeyCode.Space) && isFired == false)
		{
			initialSpeed  = Input.GetAxis("Jump") * 5;
			
			fire(initialSpeed);
			isFired = true;
		}
	}
	
	void FixedUpdate()
	{

		// Read input
		moveH = Input.GetAxis("Horizontal");

		// Set speed variable in animator
//		animator.SetFloat("hVelocity", Mathf.Abs(moveH));

		// Set character velocity
		rBody.velocity = new Vector2(moveH * maxSpeed, 0);
	}
	
	
	
	void OnCollisionEnter2D(Collision2D col)
	{
		Debug.Log("Triggered by tank");
		
		if(col.gameObject.name == "Fuel")
		{
			// Refuel tank
			Debug.Log("Tank1 refueled by 20");
			Destroy(col.gameObject);
		}
	}

	void fire(float speed)
	{
		Debug.Log("Firing, initial speed = " + speed.ToString());

		GameObject obj = GameObject.Instantiate(bombPrefab) as GameObject;
		BombController bomb = obj.GetComponent<BombController>();
		
		obj.transform.position = cannon.position;
		obj.GetComponent<Rigidbody2D>().velocity = new Vector2(1f, 1f) * initialSpeed;
		
	}
}
