using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
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
        var nc = networkManager.StartHost();
        if (nc is null)
        {
            networkManager.StartClient();
        }

        txtName.text = GameManager.Instance.Name;
    }

    void Update()
    {
        txtOnline.text = $"Online: {networkManager.numPlayers}";
       // txtOnline.text = $"Online: {NetworkServer.connections.Count}";
    }

    public void OnPlayerConnected(NetworkIdentity player)
    {
        Debug.Log("Player connected");
    }

    public void OnPlayerDisconnected(NetworkIdentity player)
    {
        Debug.Log("Player disconnected");
    }

    public virtual void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        //
    }

}
