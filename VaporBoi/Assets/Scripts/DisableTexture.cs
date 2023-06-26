using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableTexture : MonoBehaviour{

    [SerializeField] bool enableDebugTextures = false; 

    void Start () 
    {
        if (enableDebugTextures) { return; }

        if(Application.isPlaying)
        {
            GetComponent<Renderer>().enabled = false;
        }
      
        else return;
    }
}