using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{
    private Vector3 _targetPosition;
    [SerializeField] private Vector3 targetRotation = new Vector3(0, 0, -90);
    private float _beamHeight;
    
    [SerializeField] private GameObject baseBlock;
    
    private Vector3 _targetTopPosition;
    [SerializeField] private GameObject topBlock;

    private bool _changePosition = false;
    


    private void Start()
    {
        var baseBlockHeight = baseBlock.GetComponent<Renderer>().bounds.size.y;
        _beamHeight = GetComponent<Renderer>().bounds.size.y / transform.lossyScale.y;

        var baseBlockPosition = baseBlock.transform.position;
        _targetPosition.y = baseBlockPosition.y + baseBlockHeight/2 + _beamHeight/4;
        _targetPosition.x = baseBlockPosition.x;
        
        _targetTopPosition = new Vector3(baseBlock.transform.position.x, baseBlock.transform.position.y + baseBlockHeight + _beamHeight/2, baseBlock.transform.position.z);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Vertical"))
        {
            _changePosition = true;
        }
        ChangePosition(0.02f);
    }
    
    private void ChangePosition(float speed)
    {
        if (!_changePosition) return;
            
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(targetRotation), speed);
            transform.position = Vector3.Lerp(transform.position, _targetPosition, speed);
            topBlock.transform.position = Vector3.Lerp(topBlock.transform.position, _targetTopPosition, speed/4);
            
    }
}
