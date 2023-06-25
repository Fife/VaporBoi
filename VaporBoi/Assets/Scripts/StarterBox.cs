using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarterBox : MonoBehaviour
{
    private bool _hasLeft = false;
    public bool HasLeft { get {return _hasLeft; } }

    void Start() { _hasLeft = false; }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag != "Player") { return; }
        _hasLeft = true; 
        other.gameObject.GetComponent<DistanceCalculator>().StartTracking();

    }
}
