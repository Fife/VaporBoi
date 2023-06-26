using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
/*
    Throw Correction

    The main issue with the default physics is that it doesnt account for 
    many of the forces that go into a throw, namely:

    1. Controller Center of Mass
    2. Controller Angular Velocity

    Another thing that makes things weird is that:
    3. Velocity transfer of throw is only applied on 1 frame. This leads
    to a lack of velocity transfer if there is a quick flicking motion. 
    In the real world, the force is continually applied over a timeframe. 

    Solutions:
    1. Calculate Linear and angular velocity based on controller center of mass
    2. Keep track of the historical velocity average and apply that on the 
    velocity transfer instead of using the instantaneous values. 

    This script contains part 2 of the solution, and the historical average velocity
    values are accessable through their public interfaces. 
    Part 1 of the solution is attached to the controller and is triggerd at "onSelectExit"
    in the XR interaction manager. 

*/

public class ThrowCorrection : MonoBehaviour
{
    static int MAXFRAMES = 5;
    private Vector3 _averageLinearVelocity; 
    public Vector3 AverageLinearVelocity{ get{return _averageLinearVelocity;}}

    private Vector3 _averageAngularVelocity; 
    public Vector3 AverageAngularVelocity{ get{return _averageAngularVelocity;}}

    private Vector3[] _historicLinearVelocity = new Vector3[MAXFRAMES]; 
    private Vector3[] _historicAngularVelocity = new Vector3[MAXFRAMES];
    private int _frameIndex = 0; 
    private Rigidbody _rb;
    private Rigidbody _controllerRB;

    // Start is called before the first frame update
    void Start()
    {   
        _rb = GetComponent<Rigidbody>();
        //Initialize the Historic Velocity Arrays and frame counter
        for (int i =0; i < MAXFRAMES; i++)
        {
            _historicLinearVelocity[i] = new Vector3(0f,0f,0f);
            _historicAngularVelocity[i] = new Vector3(0f,0f,0f);
        }   
        _averageLinearVelocity = new Vector3(0f,0f,0f);
        _averageAngularVelocity = new Vector3(0f,0f,0f);
    }

    void FixedUpdate()
    {
        updateVelocityHistory();
    }

    void updateVelocityHistory() 
    {
        //Reset index if we are beyond the bounds of the array
        if (_frameIndex >= MAXFRAMES){ _frameIndex = 0; }

        //Store the historical frams in their arrays and incrememnt the frame index
        _historicLinearVelocity[_frameIndex] = _rb.velocity;
        _historicAngularVelocity[_frameIndex] = _rb.angularVelocity; 
        _frameIndex++;
    }

    void applyVelocityHistory()
    {
        //Apply Linear Velocity History
        if (_historicLinearVelocity == null){ return; } 
        Vector3 linearVelocityAverage = GetVectorAverage(_historicLinearVelocity);
        if (linearVelocityAverage != null ) {_rb.velocity = linearVelocityAverage;} 

        //Apply Angular Velocity History 
        if (_historicAngularVelocity == null){ return; }
        Vector3 angularVelocityAverage = GetVectorAverage(_historicLinearVelocity);
        if (angularVelocityAverage != null ) { _rb.angularVelocity = angularVelocityAverage; } 
        
        //Update the current Velocity Averages
        _averageLinearVelocity = linearVelocityAverage;
        _averageAngularVelocity = angularVelocityAverage;
    } 

    Vector3 GetVectorAverage(Vector3[] input)
    {
        Vector3 output = new Vector3(0,0,0);
        foreach(Vector3 vector in input){ output += vector; }
        return output; 
    }

    public void GrabEnd()
    {
        /*
        //Calculate Forces from Controller 
        Vector3 controllerCross = new Vector3(0f,0f,0f);

        var grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable.isSelected) {
            var interactor = grabInteractable.firstInteractorSelecting as XRBaseInteractor;
            if (interactor == null) { Debug.Log("Interactor is NULL"); return; }
            
            _controllerRB = interactor.transform.parent.gameObject.GetComponent<Rigidbody>();
            controllerCross = Vector3.Cross(_controllerRB.angularVelocity, _rb.transform.position - _controllerRB.centerOfMass);
        }

        Vector3 fullLinearVelocity = controllerCross + _rb.velocity;
        
        _rb.angularVelocity = _controllerRB.angularVelocity;

        */


        applyVelocityHistory();
        _rb.velocity = _averageAngularVelocity;
        _rb.angularVelocity = _averageAngularVelocity;

        GameObject.FindGameObjectWithTag("Player").GetComponent<ThrowTracker>().IncrementThrows();
        
    }
}


