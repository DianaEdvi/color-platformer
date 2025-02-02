using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lights : MonoBehaviour
{
    [SerializeField] private GameObject topHole;
    [SerializeField] private GameObject bottomHole;
    [SerializeField] private GameObject light;
    private bool lightIsOn;

    // Update is called once per frame
    void Update()
    {
        if (lightIsOn && light.transform.position.y < topHole.transform.position.y)
        {
            light.transform.position += new Vector3(0, 0.01f, 0);
        }
        else if (!lightIsOn && light.transform.position.y > bottomHole.transform.position.y)
        {
            light.transform.position -= new Vector3(0, 0.01f, 0);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            lightIsOn = !lightIsOn;
        }
    }
}
