using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller2 : MonoBehaviour
{
    // Start is called before the first frame update
    //added to a parent empty object
    public GameObject playerBody;

    public Vector2 myInput, direction;

    private string LogStart="PlayerLog";
    private string LogMessage;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        myInput = new Vector2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));


        direction = myInput.normalized;
        doDebug();

        Debug.Log(LogMessage);
        clearLog();
    }

    private void FixedUpdate()
    {
        playerBody.transform.position += new Vector3(myInput.x, 0, myInput.y); // love how simple this is
    }

    //I just Love these, very nice and not hard way to do debug better


    //call doDebug once every frame
    private void doDebug() {
        addToLog("magic stick of direction normalized");
        addToLog("\nDirection :");
        addToLog(direction.ToString());
        addToLog("\nInput:");
        addToLog(myInput.ToString());



    }
    //end of frame, after print, do cleanup. 
    private void clearLog()
    {
        LogMessage = LogStart;
    }
    // this is a good. One GameObject can print out many variables without clunking up the console.
    private void addToLog(string words)
    {
        LogMessage = string.Format("{0} {1}", LogMessage, words);
    }
}
