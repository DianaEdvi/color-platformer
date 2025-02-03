using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Switches : MonoBehaviour
{
    [SerializeField] private GameObject topHole; // The top position of the light
    [SerializeField] private GameObject bottomHole; // The bottom position of the light
    [SerializeField] private new GameObject light; // The actual position of the light 
    [SerializeField] private float lightSpeed = 0.01f;
    private bool _lightIsOn; // Whether the light is on or off

    // Update is called once per frame
    void Update()
    {
        FlipLight();
    }

    /**
     * Changes the light to on/off
     */
    private void FlipLight()
    {
        // Change the position of the light according to where it is supposed to be
        if (_lightIsOn && light.transform.position.y < topHole.transform.position.y)
        {
            light.transform.position += new Vector3(0, lightSpeed, 0);
        }
        else if (!_lightIsOn && light.transform.position.y > bottomHole.transform.position.y)
        {
            light.transform.position -= new Vector3(0, lightSpeed, 0);
        }
    }

    /**
     * Turn the light on if the player triggers it
     */
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _lightIsOn = !_lightIsOn;
        }
    }

    public bool getIsLightOn()
    {
        return _lightIsOn;
    }
    
    
}
