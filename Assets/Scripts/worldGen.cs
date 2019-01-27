using System;
using UnityEngine;
using Random = UnityEngine.Random;


public class worldGen : MonoBehaviour
{
    public int tileSize = 10;
    public int size = 120;
    public int sidesWidth = 10;
    public int treesAmount = 250;
    public float waterHeight = -2.5f;
    public int landHeight = 0;
    public int treesHeight = 0;
    public float sidesOffsetHeight = 2.5f;
    public GameObject sidesGameObject;
    public GameObject areaGameObject;
    public GameObject cornerGameObject;
    public GameObject[] objects;


    private byte[,] chunkas;
    // Start is called before the first frame update
    void Start()
    {
        generateMap();
    }
    void generateMap()
    {
        byte[,] chunk = new byte[size, size];
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                chunk[i, j] = 7;
            }
        }

        for (int i = sidesWidth; i < size-sidesWidth; i++)
        {
            for (int j=sidesWidth; j<size-sidesWidth; j++)
            {
                chunk[i,j] = 69;
            }
        }

        for (int j = sidesWidth; j < size-sidesWidth; j++)
        {
            chunk[sidesWidth-1, j] = 12;
            chunk[size-sidesWidth, j] = 11;
            chunk[j, sidesWidth-1] = 22;
            chunk[j, size-sidesWidth] = 21;
        }

        // corners
        int cornerX = (size / 2 - sidesWidth) * tileSize;
        GameObject obj = Instantiate(cornerGameObject, new Vector3(cornerX, waterHeight, cornerX), Quaternion.Euler(0, 0, 0));
        obj.transform.parent = transform;
        obj = Instantiate(cornerGameObject, new Vector3(-cornerX, waterHeight, cornerX), Quaternion.Euler(0, 90, 0));
        obj.transform.parent = transform;
        obj = Instantiate(cornerGameObject, new Vector3(cornerX, waterHeight, -cornerX), Quaternion.Euler(0, 180, 0));
        obj.transform.parent = transform;
        obj = Instantiate(cornerGameObject, new Vector3(-cornerX, waterHeight, -cornerX), Quaternion.Euler(0, 270, 0));
        obj.transform.parent = transform;

        // objects
        for (int i = 0; i < treesAmount; i++)
        {
            chunk[Random.Range(sidesWidth, size - sidesWidth), Random.Range(sidesWidth, size - sidesWidth)] = 5;
        }

        // terrain
        for (int i = 0; i < size; i++)
        {
            string line = "";
            for (int j = 0; j < size; j++)
            {
                line += chunk[i,j];
                obj = Instantiate(sidesGameObject, new Vector3((i - size / 2) * tileSize, waterHeight, (j - size / 2) * tileSize), Quaternion.identity);
                obj.transform.parent = transform;

                if (chunk[i, j] == 7)
                {
                    obj = Instantiate(areaGameObject, new Vector3(i - size / 2, landHeight-0.5f, j - size / 2), Quaternion.identity);  
                    obj.transform.parent = transform;
                }
                if (chunk[i, j] == 5)
                {
                    obj =  Instantiate(areaGameObject, new Vector3((i - size / 2) * tileSize, landHeight, (j - size / 2) * tileSize), Quaternion.identity);
                    obj.transform.parent = transform;
                    var index = Random.Range(0, objects.Length);
                    obj =  Instantiate(objects[index], new Vector3((i - size / 2) * tileSize, objects[index].transform.position.y, (j - size / 2) * tileSize), objects[index].transform.rotation);
                    obj.transform.parent = transform;
                }
                if (chunk[i, j] == 11)
                {
                    obj = Instantiate(areaGameObject, new Vector3((i - size / 2 - 0.07f) * tileSize, landHeight- sidesOffsetHeight, (j - size / 2) * tileSize), Quaternion.Euler(0,0,-30));
                    obj.transform.parent = transform;
                }
                if (chunk[i, j] == 12)
                {
                    obj = Instantiate(areaGameObject, new Vector3((i - size / 2 + 0.07f) * tileSize, landHeight- sidesOffsetHeight, (j - size / 2) * tileSize), Quaternion.Euler(0,0,30));
                    obj.transform.parent = transform;
                }
                if (chunk[i, j] == 21)
                {
                    obj = Instantiate(areaGameObject, new Vector3((i - size / 2) * tileSize, landHeight- sidesOffsetHeight, (j-0.07f - size / 2) * tileSize), Quaternion.Euler(30,0,0));
                    obj.transform.parent = transform;
                }
                if (chunk[i, j] == 22)
                {
                    obj = Instantiate(areaGameObject, new Vector3((i - size / 2) * tileSize, landHeight- sidesOffsetHeight, (j+0.07f - size / 2) * tileSize), Quaternion.Euler(-30,0,0));
                    obj.transform.parent = transform;
                }
                if(chunk[i, j] == 69)
                {
                    obj =  Instantiate(areaGameObject, new Vector3((i - size / 2) * tileSize, landHeight, (j - size / 2) * tileSize), Quaternion.identity);
                    obj.transform.parent = transform;
                }
                var bottomLands = new int[]
                {
                    7, 11, 12, 21, 22
                };

                if(Array.IndexOf(bottomLands, chunk[i, j]) > -1)
                {
                    obj = Instantiate(areaGameObject, new Vector3((i - size / 2) * tileSize, landHeight - sidesOffsetHeight * 2, (j - size / 2) * tileSize), Quaternion.identity);
                    obj.transform.parent = transform;
                }
            }

          //  Debug.Log(line);
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
