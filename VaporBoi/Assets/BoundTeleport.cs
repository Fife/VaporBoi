using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundTeleport : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void  OnTriggerExit(Collider other) {
        
        if (other.gameObject.tag == "Player")
        {
            other.transform.parent.GetComponent<Transform>().position = GameObject.Find("Player Spawn").GetComponent<Transform>().position ;
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}