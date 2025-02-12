using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopOfTrampoline : MonoBehaviour
{
    private bool _isColliding = false;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _isColliding = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && gameObject.CompareTag("Launch"))
        {
            _isColliding = false;
        }
    }

    public bool ReachedTopOfLaunchPad()
    {
        return _isColliding;
    }
}
