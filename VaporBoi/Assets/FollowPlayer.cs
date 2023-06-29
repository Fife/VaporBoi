using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private float _yOffset = 0.65f;
    [SerializeField] private Vector3 _xzOffset = new Vector3(0f,0f,0f);
    private Transform _cameraTransform;
    // Start is called before the first frame update
    void Start()
    {
        _cameraTransform = GameObject.Find("Main Camera").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(_cameraTransform.position.x, _yOffset, _cameraTransform.position.z); 
        transform.position += _xzOffset;

    }
}
