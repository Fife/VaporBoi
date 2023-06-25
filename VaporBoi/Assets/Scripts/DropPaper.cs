using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DropPaper : MonoBehaviour
{
    private XRSocketInteractor _socket;

    void Start()
    {
        _socket = GetComponent<XRSocketInteractor>();
    }

    public void dropPaper()
    {
        //Check socket to see if there is an interactable
        //If that interactable is a paper
        //Set its velocity to 0
        IXRSelectInteractable paper = _socket.GetOldestInteractableSelected();

        if (paper == null) { return; } 
        paper.transform.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0,-20,0);

    }
}
