using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class worldGen : MonoBehaviour
{
    public int size = 120;
    public int sidesWidth = 10;
    public int treesAmount = 50;
    public int waterHeight = -1;
    public int landHeight = 1;
    public int treesHeight = 2;
    public float sidesOffsetHeight = 0.2f;
    public GameObject sidesGameObject;
    public GameObject areaGameObject;
    public GameObject tree;


    private byte[,] chunkas;
    // Start is called before the first frame update
    void Start()
    {
        generateMap();
    }
    void generateMap()
    {
        byte[,] chunk = new byte[size, size];
        for (int i = 0; i < sidesWidth; i++)
        {
            for (int j = 0; j < size; j++)
            {
                chunk[i, j] = 7;
            }

        }

        for (int i = size - sidesWidth; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                chunk[i, j] = 7;
            }

        }

        for (int i = 0; i < size; i++)
        {
            for (int j = size-sidesWidth; j < size; j++)
            {
                chunk[i, j] = 7;
            }
        }

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < sidesWidth; j++)
            {
                chunk[i, j] = 7;
            }
        }

        for(int i = 0; i < treesAmount; i++)
        {
            chunk[Random.Range(sidesWidth, size - sidesWidth), Random.Range(sidesWidth, size - sidesWidth)] = 5;
        }
        for (int i = 0; i < size; i++)
        {
            string line = "";
            for (int j = 0; j < size; j++)
            {
                line += chunk[i,j];
                if(chunk[i, j] == 7)
                {
                    GameObject obj = Instantiate(sidesGameObject, new Vector3(i, waterHeight, j), Quaternion.identity);
                    obj.transform.parent = this.transform;
                }
                if (chunk[i, j] == 0 || chunk[i, j] == 5)
                {
                    GameObject obj =  Instantiate(areaGameObject, new Vector3(i, landHeight, j), Quaternion.identity);
                    obj.transform.parent = this.transform;
                }
                if(chunk[i, j] == 5)
                {
                    GameObject obj =  Instantiate(tree, new Vector3(i, treesHeight, j), Quaternion.identity);
                    obj.transform.parent = this.transform;
                }
            }
            Debug.Log(line);
            chunkas = chunk;
        }
    }
    public bool CanGoThere(float x, float y)
    {
        int eks = Mathf.RoundToInt(x);
        int why = Mathf.RoundToInt(y);

        if(chunkas[eks, why] == 0)
        {
            Debug.Log("allowed!");
            return true;
        }
        return false;
    }
}
