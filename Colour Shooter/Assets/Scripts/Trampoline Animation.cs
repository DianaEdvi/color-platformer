using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampolineAnimation : MonoBehaviour
{
    [SerializeField] private GameObject top; 
    [SerializeField] private GameObject bottom; 
    [SerializeField] private GameObject[] beams;
    [SerializeField] private GameObject closedPrefab; 
    [SerializeField] private GameObject openPrefab;

    private bool _closeTrampoline;
    
    // Start is called before the first frame update
    private void Start()
    {
        SetInitialPosition(closedPrefab);
    }

    // Update is called once per frame
    private void Update()
    {
        // Temporary input for testing 
        if (Input.GetKeyDown(KeyCode.N))
        {
            _closeTrampoline = true;
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            _closeTrampoline = false;
        }

        if (_closeTrampoline)
        {
            ChangePosition(openPrefab,0.02f);
        }
        else
        {
            ChangePosition(closedPrefab,0.02f);
        }
        
    }

    
    private void SetInitialPosition(GameObject targetPrefab)
    {
        var currentPosition = transform.position;
        closedPrefab.transform.position = currentPosition;
        openPrefab.transform.position = currentPosition;
        
        top.transform.position = targetPrefab.transform.Find("Top").transform.position;
        bottom.transform.position = targetPrefab.transform.Find("Bottom").transform.position;

        for (var i = 0; i < beams.Length; i++)
        {
            beams[i].transform.position = targetPrefab.transform.GetChild(i).position;
            beams[i].transform.rotation = targetPrefab.transform.GetChild(i).rotation;
        }
    }
    
    private void ChangePosition(GameObject targetPrefab, float speed)
    {
        for (var i = 0; i < beams.Length; i++)
        {
            beams[i].transform.rotation = Quaternion.Lerp(beams[i].transform.rotation, targetPrefab.transform.GetChild(i).rotation,speed);
            beams[i].transform.position = Vector3.Lerp(beams[i].transform.position, targetPrefab.transform.GetChild(i).position, speed);
        }
        top.transform.position = Vector3.Lerp(top.transform.position, targetPrefab.transform.GetChild(5).position, speed);
    }
}
