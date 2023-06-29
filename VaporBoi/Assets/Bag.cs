using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Bag : MonoBehaviour
{
    [SerializeField] private GameObject _grabberPrefab;
    [SerializeField] private GameObject _paperPrefab;
    private GameObject _activeHand; 
    bool _lastPress, _buttonDown; 
    //Get the Controller Input Device
    //Get the XR Node Attached to the input device
    //Check its button state
    private void Awake() {}
    private void Start() {}
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag != "Hands"){return;}
        _activeHand = other.gameObject;
        List<InputDevice> m_device = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Right, m_device);

        if(m_device.Count == 1)
        {
            CheckPrimaryButton(m_device[0]);
        }

    }

    private void CheckPrimaryButton(InputDevice device)
    {
        device.TryGetFeatureValue(CommonUsages.primaryButton, out _buttonDown);
        if (_buttonDown == true && _lastPress == false) 
        {
            //If button is pressed Instantiate Grabber       
            Instantiate(_grabberPrefab, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        }
        _lastPress = _buttonDown;
    }

    public void SpawnPaper()
    {
        //Spawn Prefab
        Vector3 offset = new Vector3(0,0.1f,0);
        offset += transform.position;            
        Rigidbody newspaper = Instantiate(_paperPrefab, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        
        //Let Course Manager Know we Spawned A Paper
        GameObject.FindGameObjectWithTag("CourseManager").GetComponent<CourseManager>().IncrementPapers();
        
        //Add Some Velocity so it "pops" up
        //newspaper.velocity = new Vector3(0,3f,0);

        //Spawn the Paper Prefab
        //Attach it to the hand that is currently in the 
    }

}