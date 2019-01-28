using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickedUp : MonoBehaviour
{


    public bool voice;


    // Start is called before the first frame update
    void Start()
    {
        //rng which voice sound to play
    }


    void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter");
        Debug.Log(other.gameObject.tag);

        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Pick up event");
            other.gameObject.GetComponent<PlayerPun>().IncreaseScore();
        }
    }

}
