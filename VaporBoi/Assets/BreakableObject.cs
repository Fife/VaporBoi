using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BreakableObject : MonoBehaviour
{
    [SerializeField] private GameObject _regularModelInstance;
    [SerializeField] private GameObject _brokenModelPrefab;
    private Vector3 _gnomeOffset = new Vector3(0,-0.144f, 0);
    bool _isTriggered = false;
    public bool IsTriggered { get { return _isTriggered; } }

    private Transform _tempTransform;
    void Start()
    {
        //_regularModelInstance = (GameObject)Instantiate(_regularModelPrefab, transform.parent.gameObject.transform, false);
        //_regularModelInstance.transform.position += _gnomeOffset;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if(!_isTriggered && (other.gameObject.tag== "Player" || other.gameObject.tag== "Paper" ) )
        {
            Debug.Log("Entered");
            Instantiate(_brokenModelPrefab, transform.parent.gameObject.transform, false);
            Destroy(_regularModelInstance);
            _isTriggered = true;
        }
    }
}
