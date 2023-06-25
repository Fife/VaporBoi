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
        if(other.gameObject.tag != "Paper") { return; }
        _paper = other.gameObject;
    }
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject == _paper) { _paper = null; }
    }
}
