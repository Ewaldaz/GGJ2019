using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameLoader : MonoBehaviour
{
    public NetworkManager networkManager;
    
    void Start()
    {
        var client = networkManager.StartClient();
        if (!client.isConnected)
        {
            networkManager.StartHost();
        }
    }
    
}
