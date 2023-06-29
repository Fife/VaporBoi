using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ReplacePaper : MonoBehaviour
{
    [SerializeField] private GameObject _paperPrefab;
    private XRSocketInteractor _socket;
    private XRGrabInteractable _grab;
    [SerializeField] private XRInteractionManager _im;
    // Start is called before the first frame update
    void Start()
    {
        _socket = GetComponent<XRSocketInteractor>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_socket.hasSelection){return;}
        GameObject paperInstance = Instantiate(_paperPrefab);
        paperInstance.transform.position = transform.parent.transform.position;
        _grab = paperInstance.GetComponent<XRGrabInteractable>();
        _im.ForceSelect(_socket, _grab);
        
        //Let Course Manager Know we Spawned A Paper
        GameObject.FindGameObjectWithTag("CourseManager").GetComponent<CourseManager>().IncrementPapers();
    }
}