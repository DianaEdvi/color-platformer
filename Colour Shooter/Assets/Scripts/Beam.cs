using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{
    private Vector3 _targetPosition;
    [SerializeField] private Vector3 targetRotation = new Vector3(0, 0, -90);
    
    [SerializeField] private GameObject baseBlock;
    
    private Vector3 _targetTopPosition;
    [SerializeField] private GameObject topBlock;

    private bool _changePosition = false;

    private Vector3[] targets;
    private Transform[] _transforms;
    


    private void Start()
    {
        var baseBlockHeight = baseBlock.GetComponent<Renderer>().bounds.size.y;
        var baseBlockPosition = baseBlock.transform.position;
        
        _targetPosition.y = baseBlockPosition.y + baseBlockHeight/2;
        _targetPosition.x = baseBlockPosition.x;
        
        _targetTopPosition = new Vector3(baseBlock.transform.position.x, baseBlock.transform.position.y + baseBlockHeight/2, baseBlock.transform.position.z);

        _transforms = new Transform[] { transform, topBlock.transform };
        targets = new Vector3[] { _targetPosition, targetRotation, _targetTopPosition };



    }

    private void Update()
    {
        if (Input.GetButtonDown("Vertical"))
        {
            _changePosition = true;
        }
        ChangePosition(_transforms, targets, 0.02f);
    }
    
    private void ChangePosition(Transform[] transforms, Vector3[] targetPositions, float speed)
    {
        if (!_changePosition) return;
            transforms[0].rotation = Quaternion.Lerp(transforms[0].rotation, Quaternion.Euler(targetPositions[1]), speed);
            transforms[0].position = Vector3.Lerp(transforms[0].position, targetPositions[0], speed);
            transforms[1].position = Vector3.Lerp(transforms[1].position, targetPositions[2], speed/4);
    }
}
