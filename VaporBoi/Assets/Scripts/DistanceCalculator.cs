using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceCalculator : MonoBehaviour
{
    float distance = 0f;
    Vector3 _lastCoord, _currentCoord;
    // Start is called before the first frame update
    void Start()
    {
        _currentCoord = transform.position;
        _currentCoord = new Vector3(_currentCoord.x, 0, _currentCoord.z);
        _lastCoord = _currentCoord;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _currentCoord = transform.position;
        
        //Strip Y coordinate
        _currentCoord = new Vector3(_currentCoord.x, 0, _currentCoord.z);

        distance += (_currentCoord - _lastCoord).magnitude;
        _lastCoord = _currentCoord;
        Debug.Log(distance);
    }
}
