using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowTracker : MonoBehaviour
{
    private int _numThrows = 0;
    public int NumThrows { get { return _numThrows; } }

    void Start()
    {
        _numThrows = 0;
    }

    public void IncrementThrows()
    {
        _numThrows++;
    }
}
