using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Shooter : MonoBehaviour
{
    private Vector3 mousePos;
    [SerializeField] private Transform spawner;
    [SerializeField] private GameObject paintballPrefab;
    [SerializeField] private SpriteRenderer ballColor;
    [SerializeField] private Color firstColor;
    [SerializeField] private Color secondColor;
    


    // Update is called once per frame
    void Update()
    {
        Aim();
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

    private void Shoot(Color color)
    {
        ballColor.color = color;
        Instantiate(paintballPrefab, spawner.position, spawner.rotation);
    }
}
