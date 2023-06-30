using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenaltyBox : MonoBehaviour
{
    private float _timerThreshold = 1f;
    private float _timer; 
    private GameObject _paper = null; 
    private bool _isTriggered = false;
    public bool IsTriggered { get { return _isTriggered; } }

    private float _enteredDistance =0f;
    private float _exitedDistance = 0f;
    private float _totalDistance = 0f;
    public float PenaltyDistance { get { return _totalDistance; } }


    // Start is called before the first frame update
    void Start()
    {
        _timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(_paper == null) { return; }

        //If the paper has slowed to a stop, start the timer
        if(_paper.transform.parent.GetComponent<Rigidbody>().velocity.magnitude <= 0.01)
        {
            _timer += Time.deltaTime;
        }

        //If the timer ticked over the thershold
        //Reset the timer, and also mark the hole as completed
        if (_timer > _timerThreshold)
        {
            _timer = 0f;
            _isTriggered = true;
            //Debug.Log("Penalty!");
            _paper = null;
        }

    }

    public void turnOffTrigger()
    {
        _isTriggered = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Paper") { _paper = other.gameObject; }
        if(other.gameObject.tag == "Player") 
        { 
            _enteredDistance = other.gameObject.GetComponent<DistanceCalculator>().Distance;
            Debug.Log("Player Entered");
            _isTriggered = true;
        }
        
    }
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject == _paper) { _paper = null; }
        if(other.gameObject.tag == "Player") 
        { 
            _exitedDistance = other.gameObject.GetComponent<DistanceCalculator>().Distance;
            _totalDistance = _exitedDistance - _enteredDistance;
            Debug.Log("Player Exitied");
        }
    }
}
