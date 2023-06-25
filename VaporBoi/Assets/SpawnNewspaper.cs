using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class SpawnNewspaper : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    bool _lastPress, _buttonDown; 
    //Get the Controller Input Device
    //Get the XR Node Attached to the input device
    //Check its button state
    private void Awake() {}
    private void Start() {}
    
    private void Update()
    {
        List<InputDevice> m_device = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Right, m_device);

        if(m_device.Count == 1)
        {
            CheckPrimary(m_device[0]);
        }

    }

    private void CheckPrimary(InputDevice device)
    {
        device.TryGetFeatureValue(CommonUsages.primaryButton, out _buttonDown);
        if (_buttonDown == true && _lastPress == false) 
        {
            //Spawn Prefab
            Vector3 offset = new Vector3(0,0.05f,0);
            offset += transform.position;
            Rigidbody newspaper = Instantiate(_prefab, offset, Quaternion.identity).GetComponent<Rigidbody>();

            newspaper.velocity = new Vector3(0,3f,0);
        }
        _lastPress = _buttonDown;
    }
}
