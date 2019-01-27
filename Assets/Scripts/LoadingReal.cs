using System.Collections; 
using System.Collections.Generic;
 using UnityEngine;
  public class LoadingReal : MonoBehaviour 
  {
       public float mySpeed=15f;
        private float x,y,z;

// Update is called once per frame 
void Update() 
{ 
    x = gameObject.transform.position.x + Time.deltaTime * mySpeed;
     y = gameObject.transform.position.y;
      z = gameObject.transform.position.z;
       this.gameObject.transform.position = new Vector3 (x,y,z);
} }