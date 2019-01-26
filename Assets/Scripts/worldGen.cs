using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class worldGen : MonoBehaviour
{
    public int size = 120;
    public int sidesWidth = 10;
    public int treesAmount = 50;
    public float waterHeight = -0.1f;
    public int landHeight = 0;
    public int treesHeight = 1;
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
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                chunk[i, j] = 7;
            }

        }

        for (int i = sidesWidth; i < size-sidesWidth; i++){
            for (int j=sidesWidth; j<size-sidesWidth; j++){
                chunk[i,j] = 69;
            }
        }


        for (int j = sidesWidth; j < size-sidesWidth; j++){
            chunk[sidesWidth-1, j] = 12;
            chunk[size-sidesWidth, j] = 11;
            chunk[j, sidesWidth-1] = 22;
            chunk[j, size-sidesWidth] = 21;
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
                    obj =  Instantiate(areaGameObject, new Vector3(i, landHeight-0.5f, j), Quaternion.identity);  
                    obj.transform.parent = this.transform;
                }
                if (chunk[i, j] == 5)
                {
                    GameObject obj =  Instantiate(areaGameObject, new Vector3(i, landHeight, j), Quaternion.identity);
                    obj.transform.parent = this.transform;
                    obj =  Instantiate(tree, new Vector3(i, treesHeight, j), Quaternion.identity);
                    obj.transform.parent = this.transform;
                }
                if (chunk[i, j] == 11){
                    GameObject obj = Instantiate(areaGameObject, new Vector3(i-0.07f, landHeight-0.25f, j), Quaternion.Euler(0,0,-30));
                    //obj.transform.rotation = new Quaternion(30,0,0,0);
                    obj.transform.parent = this.transform;
                }
                if (chunk[i, j] == 12){
                    GameObject obj = Instantiate(areaGameObject, new Vector3(i+0.07f, landHeight-0.25f, j), Quaternion.Euler(0,0,30));
                    obj.transform.parent = this.transform;
                }
                if (chunk[i, j] == 21){
                    GameObject obj = Instantiate(areaGameObject, new Vector3(i, landHeight-0.25f, j-0.07f), Quaternion.Euler(30,0,0));
                    obj.transform.parent = this.transform;
                }
                if (chunk[i, j] == 22){
                    GameObject obj = Instantiate(areaGameObject, new Vector3(i, landHeight-0.25f, j+0.07f), Quaternion.Euler(-30,0,0));
                    obj.transform.parent = this.transform;
                }
                if(chunk[i, j] == 69)
                {
                    GameObject obj =  Instantiate(areaGameObject, new Vector3(i, landHeight, j), Quaternion.identity);
                    obj.transform.parent = this.transform;
                }
                //if(chunk[])
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
