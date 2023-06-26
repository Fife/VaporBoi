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
        //if(other.transform.gameObject.tag != "Player" || other.transform.gameObject.tag != "Paper") { return; }
        
        if(_hasLeft == false)
        {
            Debug.Log("Starting Game!");
            GameObject.FindGameObjectWithTag("CourseManager").GetComponent<CourseManager>().StartGame();
            _hasLeft = true; 
            GameObject.FindGameObjectWithTag("Player").GetComponent<DistanceCalculator>().StartTracking();
        }
    }
}
