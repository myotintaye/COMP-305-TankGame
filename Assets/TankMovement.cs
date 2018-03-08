using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovement : MonoBehaviour
{

    // Public variables
    public float maxSpeed = 5f;

    // Private variables
    private Rigidbody2D rBody;
    private Animator animator;


    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        animator.SetFloat("hVelocity", Mathf.Abs(horizontalInput));

        rBody.velocity = new Vector2(horizontalInput * maxSpeed, 0);

        transform.localScale = new Vector3(Mathf.Sign(horizontalInput), 1, 1);
    }
}