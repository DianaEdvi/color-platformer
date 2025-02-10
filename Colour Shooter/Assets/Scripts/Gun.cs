using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Gun : MonoBehaviour
{
    private Vector3 mousePos; // Position of mouse on the screen
    [SerializeField] private Transform spawner; // Where the bullets will come from
    [SerializeField] private GameObject paintballPrefab; // The paintball template
    [SerializeField] private SpriteRenderer ballColor; // The current color of the paintball 
    [SerializeField] private Color firstColor; // The color of the first ball
    [SerializeField] private Color secondColor; // The color of the second ball 
    
    // Update is called once per frame
    void Update()
    {
        // Take aim
        Aim();
        
        // Take input and fire 
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot(firstColor);
        }
        else if (Input.GetButtonDown("Fire2"))
        {
            Shoot(secondColor);
        }
        
    }
    
    private void Aim()
    {
        // Get the mouse position in world space
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0; // Ensure 2D positioning

        // Calculate the direction from the object to the mouse
        var direction = mousePos - transform.position;

        // Calculate the angle based on direction
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
       
        // Apply the calculated angle to the gun's rotation
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    /**
     * Instantiates a new bullet prefab and sets its color appropriately 
     */
    private void Shoot(Color color)
    {
        ballColor.color = color;
        Instantiate(paintballPrefab, spawner.position, spawner.rotation);
    }
}
