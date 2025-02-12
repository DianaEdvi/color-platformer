using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrampolineAnimation : MonoBehaviour
{
    // The separate parts of the trampoline
    private GameObject _top; 
    private GameObject _bottom; 
    private GameObject[] _beams;
    [SerializeField] private float animationSpeed = 0.02f;
    [SerializeField] private GameObject trampoline; // The parent trampoline object
    [SerializeField] private GameObject closedPrefab; // The closed template
    [SerializeField] private GameObject openPrefab; // The open template 
    
    // Start is called before the first frame update
    private void Start()
    {
        SetGameObjects();
        SetInitialPosition(closedPrefab);
    }
    

    /**
     * Find the game objects instead of having to assign them in the hierarchy every time
     */
    private void SetGameObjects()
    {
        // Find child objects relative to 'trampoline'
        _top = trampoline.transform.Find("Top").gameObject;
        _bottom = trampoline.transform.Find("Bottom").gameObject;
        _beams = new[]
        {
            trampoline.transform.Find("Bottom beam left").gameObject, 
            trampoline.transform.Find("Bottom beam right").gameObject,
            trampoline.transform.Find("Top beam right").gameObject, 
            trampoline.transform.Find("Top beam left").gameObject
        };
    }
    
    /**
     * Sets the position of the template prefabs to the position of the current object (aka make all transformations relative to the current object) 
     */
    private void SetInitialPosition(GameObject targetPrefab)
    {
        // The current position of the object
        var currentPosition = trampoline.transform.position;
        
        // Make template positions relative to current position
        closedPrefab.transform.position = currentPosition;
        openPrefab.transform.position = currentPosition;
    }
    
    /**
     * Uses Lerp functions to perform the animations for opening and closing the trampolines
     */
    public void ChangePosition(GameObject targetPrefab, float speed)
    {
        // Assign rotation and position for the beams 
        for (var i = 0; i < _beams.Length; i++)
        {
            _beams[i].transform.rotation = Quaternion.Lerp(_beams[i].transform.rotation, targetPrefab.transform.GetChild(i).rotation,speed);
            _beams[i].transform.position = Vector3.Lerp(_beams[i].transform.position, targetPrefab.transform.GetChild(i).position, speed);
        }
        
        // Assign position for top
        _top.transform.position = Vector3.Lerp(_top.transform.position, targetPrefab.transform.GetChild(5).position, speed);
    }

    public GameObject GetPrefabs(string type)
    {
        return type switch
        {
            "Open" => openPrefab,
            "Close" => closedPrefab,
            _ => null
        };
    }

    public float GetAnimationSpeed()
    {
        return animationSpeed;
    }
}
