using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : MonoBehaviour
{
    float x;
    float z;
    float jump;
    Vector3 prevLoc;
    public float lookSpeed = 10;
    bool running = false;
    bool up = false;
    float jumpVelocity = 500f;

    // Update is called once per frame
    void Update()
    {
        ReadInputs();
    }

    void FixedUpdate()
    {
        prevLoc = transform.position;
        transform.position = transform.position + new Vector3(x, 0, z);

        #region Rotation and animations
        if (transform.position != prevLoc || running)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(transform.position - prevLoc), Time.fixedDeltaTime * lookSpeed);
          //  GetComponent<Animator>().Play("Running");
        }
        else
        {
           // GetComponent<Animator>().Play("Idle");
        }
        #endregion Rotation and animations

        #region Jump
        if (up)
        {
            if (GetComponent<Rigidbody>().velocity.y <= 0 && GetComponent<Rigidbody>().velocity.y > -0.01)
            {
                GetComponent<Rigidbody>().AddForce(0, jumpVelocity, 0);
            }
            up = false;
        }
        #endregion Jump
    }

    private void ReadInputs()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        jump = Input.GetAxis("Jump");
        if (jump != 0)
        {
            up = true;
        }
    }
}
