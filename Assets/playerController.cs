using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public worldGen world;
    // Update is called once per frame
    int x = 50;
    int y = 50;
    int height = 2;
    void Update()
    {
      if(Input.GetKeyDown(KeyCode.D))
        {
            if(world.CanGoThere(x - 1, y))
            {
                x -=1;
            }
            Debug.Log(x + ":" + y);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (world.CanGoThere(x + 1, y))
            {
                x+=1;
            }
            Debug.Log(x + ":" + y);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (world.CanGoThere(x, y-1))
            {
                y-=1;
            }
            Debug.Log(x + ":" + y);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (world.CanGoThere(x, y + 1))
            {
                y +=1;
            }
            Debug.Log(x + ":" + y);
        }
        setPlayerPos(x, y);
    }
    void setPlayerPos(int x, int y)
    {
        Debug.Log(x + ":" + y);
        this.transform.position = new Vector3(x, height, y);
    }
}
