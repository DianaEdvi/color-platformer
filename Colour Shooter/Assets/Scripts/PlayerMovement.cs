using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb; // The rigidbody of the player
    [SerializeField] private float speed; // Speed of player
    private Vector2 _movement; // Current direction
    private Vector2 _targetVelocity; // Goal speed
    private Vector3 _velocity = Vector3.zero; // idk
    [SerializeField] private float smoothTime = 0.2f; // Amount of smoothing
    [SerializeField] private float jumpForce; // How high you jump
    private bool _isGrounded = false; // Whether touching ground 

    void Update()
    {
        // Get horizontal input
        _movement.x = Input.GetAxisRaw("Horizontal");
        // Flip();
        
        // Allow jump only if grounded
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        // Apply horizontal movement
        var velocity = rb.velocity;
        _targetVelocity = new Vector2(_movement.x * speed, velocity.y);
        rb.velocity = Vector3.SmoothDamp(velocity, _targetVelocity, ref _velocity, smoothTime);

    }

    /**
     * Makes the player jump
     */
    private void Jump()
    {
        rb.AddForce(new Vector2(rb.velocity.x, jumpForce));
        _isGrounded = false;  // Temporarily set false to prevent multiple jumps
    }

    /**
     * Changes the direction the player sprite is facing 
     */
    private void Flip()
    {
        var flipped = rb.transform.localScale;
        if (_movement.x < 0)
        {
            flipped.x = -1;
        }
        else if (_movement.x > 0)
        {
            flipped.x = 1;
        }
        rb.transform.localScale = flipped;
    }

    /**
     * Checks collision with ground
     */
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
    }

    /**
     * Checks leaving collision with ground 
     */
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _isGrounded = false;
        }
    }
}