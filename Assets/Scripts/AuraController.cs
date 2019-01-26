using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraController : MonoBehaviour
{
    //this is the individual player smell controller
    // mechanic thing
    // this is just data of color
    public Color myColor;
    public bool RnG_RGB;


    // Start is called before the first frame update
    void Start()
    {
        if (RnG_RGB){
            myColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        }
    }
}
