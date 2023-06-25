using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceCalculator : MonoBehaviour
{
    private float _distance = 0f;
    private Vector3 _lastCoord, _currentCoord;
    private bool _isTracking = false; 
    // Start is called before the first frame update
    void Start()
    {
        _currentCoord = transform.position;
        _currentCoord = new Vector3(_currentCoord.x, 0, _currentCoord.z);
        _lastCoord = _currentCoord;
    }

    public void StartTracking()
    {
        _isTracking = true;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!_isTracking){ return; }

        _currentCoord = transform.position;
        //Strip Y coordinate
        _currentCoord = new Vector3(_currentCoord.x, 0, _currentCoord.z);
        _distance += (_currentCoord - _lastCoord).magnitude;
        _lastCoord = _currentCoord;

    }
}
