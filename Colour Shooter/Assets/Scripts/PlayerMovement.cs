using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    private Vector2 _movement;
    private Vector2 _targetVelocity;
    private Vector3 _velocity = Vector3.zero;
    [SerializeField] private float smoothTime = 0.2f;
    [SerializeField] private float jumpForce;
    private bool _isGrounded = false;

    void Update()
    {
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
        // rb.velocity = new Vector2(_movement.x * speed * Time.fixedDeltaTime, rb.velocity.y);
        var velocity = rb.velocity;
        _targetVelocity = new Vector2(_movement.x * speed, velocity.y);
        rb.velocity = Vector3.SmoothDamp(velocity, _targetVelocity, ref _velocity, smoothTime);

    }

    private void Jump()
    {
        rb.AddForce(new Vector2(rb.velocity.x, jumpForce));
        _isGrounded = false;  // Temporarily set false to prevent multiple jumps
    }

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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _isGrounded = false;
        }
    }
}