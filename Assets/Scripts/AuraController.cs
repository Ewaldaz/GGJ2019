using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraController : MonoBehaviour
{
    //this is the individual player smell controller
    // mechanic thing
    public Color myColor;



    // Start is called before the first frame update
    void Start()
    {
        myColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
