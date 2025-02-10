using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paintball : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        // Launch bullet 
        rb.velocity = transform.right * speed;
    }

    /**
     * Check for collisions 
     */
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Ignore player hitbox 
        if (other.CompareTag("Player")) return;
        
        // Ignore game objects with no sprite renderer 
        if (other.gameObject.GetComponent<SpriteRenderer>() == null) return;
        
        // Otherwise, change the color of the hit object to the paintball color 
        other.gameObject.GetComponent<SpriteRenderer>().color = gameObject.GetComponent<SpriteRenderer>().color;
        Destroy(gameObject);
    }
}
