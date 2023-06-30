using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowBox : MonoBehaviour
{
    [SerializeField] GameObject _windowPrefab; 
    private bool _isTriggered = false;
    public bool IsTriggered { get { return _isTriggered; } }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(!_isTriggered && (other.gameObject.tag== "Paper" ) )
        {
            //Play Particle Animation and sound
            //Alert the Game Manager that the end was triggered by throwing it through the window
            //Delete the Paper
            Destroy(other.gameObject);
            _isTriggered = true;
            Instantiate(_windowPrefab, GetComponent<Transform>());
            GetComponent<AudioSource>().Play();
        }
    }
}
