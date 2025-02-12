using System.Collections;
using UnityEngine;

public class TrampolineAnimation : MonoBehaviour
{
    private GameObject _top;
    private GameObject _bottom;
    private GameObject[] _beams;
    
    [SerializeField] private float animationSpeed = 0.1f;
    [SerializeField] private GameObject trampoline; 
    [SerializeField] private GameObject closedPrefab; 
    [SerializeField] private GameObject openPrefab; 
    
    private bool _isActive = false;
    private bool _isAnimating = false;

    private TopOfTrampoline _topOfTrampoline;

    private void Start()
    {
        _topOfTrampoline = GetComponentInChildren<TopOfTrampoline>();
        SetGameObjects();
        SetInitialPosition(closedPrefab);
    }

    private void Update()
    {
        if (_topOfTrampoline.ReachedTopOfLaunchPad() && !_isAnimating)
        {
            Debug.Log("Starting trampoline animation...");
            StartCoroutine(AnimateTrampoline());
        }
    }

    private void SetGameObjects()
    {
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

    private void SetInitialPosition(GameObject targetPrefab)
    {
        var currentPosition = trampoline.transform.position;
        closedPrefab.transform.position = currentPosition;
        openPrefab.transform.position = currentPosition;
    }

    public void ChangePosition(GameObject targetPrefab, float speed)
    {
        for (var i = 0; i < _beams.Length; i++)
        {
            _beams[i].transform.rotation = Quaternion.Lerp(_beams[i].transform.rotation, targetPrefab.transform.GetChild(i).rotation, speed);
            _beams[i].transform.position = Vector3.Lerp(_beams[i].transform.position, targetPrefab.transform.GetChild(i).position, speed);
        }

        _top.transform.position = Vector3.Lerp(_top.transform.position, targetPrefab.transform.GetChild(5).position, speed);
    }

    private IEnumerator AnimateTrampoline()
    {
        _isAnimating = true;

        // Open animation
        Debug.Log("Opening trampoline...");
        yield return LerpToTarget(openPrefab);

        // Wait before closing
        yield return new WaitForSeconds(0.5f);

        // Close animation
        Debug.Log("Closing trampoline...");
        yield return LerpToTarget(closedPrefab);

        _isAnimating = false;
    }
    
    private IEnumerator LerpToTarget(GameObject targetPrefab)
    {
        float elapsedTime = 0f;
        float duration = 1f;  // Adjust this for a smoother animation

        Vector3[] startPositions = new Vector3[_beams.Length + 1];
        Quaternion[] startRotations = new Quaternion[_beams.Length];

        // Store initial positions and rotations
        for (var i = 0; i < _beams.Length; i++)
        {
            startPositions[i] = _beams[i].transform.position;
            startRotations[i] = _beams[i].transform.rotation;
        }
        startPositions[_beams.Length] = _top.transform.position;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime * animationSpeed;
            float t = elapsedTime / duration;

            for (var i = 0; i < _beams.Length; i++)
            {
                _beams[i].transform.position = Vector3.Lerp(startPositions[i], targetPrefab.transform.GetChild(i).position, t);
                _beams[i].transform.rotation = Quaternion.Lerp(startRotations[i], targetPrefab.transform.GetChild(i).rotation, t);
            }

            _top.transform.position = Vector3.Lerp(startPositions[_beams.Length], targetPrefab.transform.GetChild(5).position, t);

            yield return null;
        }

        // Ensure final position is exact
        for (var i = 0; i < _beams.Length; i++)
        {
            _beams[i].transform.position = targetPrefab.transform.GetChild(i).position;
            _beams[i].transform.rotation = targetPrefab.transform.GetChild(i).rotation;
        }
        _top.transform.position = targetPrefab.transform.GetChild(5).position;
    }
}
