using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Lights : MonoBehaviour
{

    [SerializeField] private Light2D globalLight;
    [SerializeField] private Color defaultColor;
    [SerializeField] private SpriteRenderer switchRenderer;
    [SerializeField] private Switches switches;
    [SerializeField] private Light2D[] lamps;

    private float _currentColorValue = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Make a coroutine
        if (switches.getIsLightOn())
        {
            globalLight.color = switchRenderer.color;
            // globalLight.color = Color.Lerp(defaultColor, switchRenderer.color, _currentColorValue);
        }
        else
        {
            globalLight.color = defaultColor;
            // globalLight.color = Color.Lerp(switchRenderer.color, defaultColor, _currentColorValue);;
        }

        _currentColorValue += 0.01f;
        foreach (var lamp in lamps)
        {
            lamp.color = globalLight.color;
        }



    }
}
