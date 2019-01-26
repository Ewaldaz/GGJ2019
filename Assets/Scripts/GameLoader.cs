using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GameLoader : NetworkBehaviour
{
    public NetworkManager networkManager;
    public Text txtName;

    void Start()
    {
        var nc = networkManager.StartHost();
        if (nc is null)
        {
            networkManager.StartClient();
        }

        txtName.text = GameManager.Instance.Name;
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
        Debug.Log("OnServerAddPlayer");
    }

}
