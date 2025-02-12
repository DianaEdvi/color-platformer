using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.WSA;
using Object = UnityEngine.Object;

public class TrampolinePhysics : MonoBehaviour
{

    private TrampolineAnimation _animation;
    private bool _closeTrampoline;
    private TopOfTrampoline _topOfTrampoline;
    private Rigidbody2D rb;
    private PlayerMovement _playerMovement;
    [SerializeField] private float launchForce = 500f;
    private bool _isLaunching = false;

    
    // Start is called before the first frame update
    void Start()
    {
        rb = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        
        _animation = GetComponent<TrampolineAnimation>();
        _topOfTrampoline = GetComponentInChildren<TopOfTrampoline>();

        if (_animation == null)
        {
            Debug.LogError("TrampolineAnimation component not found on the same GameObject!", this);
        }
        
        if (_topOfTrampoline == null)
        {
            Debug.LogError("TopOfTrampoline component not found on the same GameObject!", this);
        }

    }

    // Update is called once per frame
    private void Update()
    {
        if (_topOfTrampoline.ReachedTopOfLaunchPad())
        {
            Debug.Log("reached");
            AnimateTrampoline();
            _isLaunching = true;
        }
        
    }

    private void FixedUpdate()
    {
        if (_isLaunching)
        {
            Launch();
        }
    }

    private void Launch()
    { 
        // Zero out the x velocity to avoid influence from horizontal movement
        
        rb.velocity = new Vector2(rb.velocity.x, 0);  // Reset the y velocity 

        // Apply the launch force in the upward direction
        rb.AddForce(new Vector2(rb.velocity.x, launchForce));
        _isLaunching = false;
    }

    private void AnimateTrampoline()
    {
        // Set the proper position according to if the trampoline is opening or closing 
        _animation.ChangePosition(_closeTrampoline ? _animation.GetPrefabs("Open") : _animation.GetPrefabs("Close"),
            _animation.GetAnimationSpeed());
    }

}
