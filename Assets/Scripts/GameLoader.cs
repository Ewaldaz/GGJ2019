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

    [SyncVar(hook = "Changed")]
    public int playersConnected = 0;

    public void Changed(int newVal)
    {
        Debug.Log($"New val: {newVal}");
    }

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
        if (NetworkServer.connections.Count > 0)
        {
            playersConnected = NetworkServer.connections.Count;
        }
        txtOnline.text = $"Online: {playersConnected}";
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
