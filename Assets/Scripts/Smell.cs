using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smell : MonoBehaviour
{
    // these objects instantiated by players onEnter to new tile
    private AuraController myManager;
    private Color myColor;
    // Start is called before the first frame update
    void Start()
    {
        myManager = GetComponent<AuraController>();
        myColor = myManager.myColor;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
