using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour {

	private Rigidbody2D rBody;
	private Animator animator;

	// Use this for initialization
	void Start () {
		rBody = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		
		Debug.Log("Box Initiated");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
