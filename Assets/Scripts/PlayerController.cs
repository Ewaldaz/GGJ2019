using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float x;
    float z;
    // Update is called once per frame
    void Update()
    {
        ReadInputs();
    }

    void FixedUpdate()
    {
        transform.position = transform.position + new Vector3(x, 0, z);
    }

    private void ReadInputs()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
    }
}
