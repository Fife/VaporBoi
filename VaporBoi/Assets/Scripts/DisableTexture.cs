using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DisableTexture : MonoBehaviour{
   void Start () 
   {
        if(Application.isPlaying)
        {
            GetComponent<Renderer>().enabled = false;
        }
      
        else return;
    }
}