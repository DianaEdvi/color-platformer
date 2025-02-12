using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;

public class Light : MonoBehaviour
{

    [SerializeField] private Light2D globalLight;
    [SerializeField] private Color defaultColor;
    [SerializeField] private SpriteRenderer switchRenderer;
    [FormerlySerializedAs("switches")] [SerializeField] private Switch lightSwitch;
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
        if (lightSwitch.getIsLightOn())
        {
            globalLight.color = switchRenderer.color;
        }
        else
        {
            globalLight.color = defaultColor;
        }

        _currentColorValue += 0.01f;
        foreach (var lamp in lamps)
        {
            lamp.color = globalLight.color;
        }



    }
}
