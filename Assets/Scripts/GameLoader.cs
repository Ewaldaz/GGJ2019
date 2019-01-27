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

        txtName.text = GameManagerOld.Instance.Name;
    }

}
