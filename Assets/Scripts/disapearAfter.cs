using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disapearAfter : MonoBehaviour
{
    // Start is called before the first frame update
    // makes thing disapear after time
    public float myTime = 15f;
    

    // Update is called once per frame
    void Update()
    {
        myTime -= Time.deltaTime;
        if (myTime <= 0f) {
            this.gameObject.SetActive(false);
        }
    }
}
