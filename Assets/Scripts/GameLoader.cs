using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GameLoader : NetworkBehaviour
{
    public NetworkManager networkManager;
    public Text txtName;
    public Text txtOnline;
    public List<string> onlinePlayers;
    
    void Start()
    {
        onlinePlayers = new List<string>();
        var client = networkManager.StartClient();
       // if (!client.isConnected)
       // {
            var nm = networkManager.StartHost();
       // }

        txtName.text = GameManager.Instance.Name;
    }
    //void OnFailedToConnect(NetworkConnectionError error)
    //{
    //    Debug.Log("Could not connect to server: " + error);
    //}

    void Update()
    {
        txtOnline.text = $"Online: {NetworkServer.connections.Count}";
    }
    public virtual void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        //
    }

}
