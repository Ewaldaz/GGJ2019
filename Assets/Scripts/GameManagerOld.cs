using System;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerOld : NetworkBehaviour
{
    public static GameManagerOld Instance;
    public Text txtOnline;
  //  public SyncListString onlinePlayers = new SyncListString();

   // [SyncVar]
    public int playersConnected = 0;
    

    public void Changed(int newVal)
    {
        txtOnline.text = $"Online: {playersConnected}" + Environment.NewLine;

      //  foreach (var name in onlinePlayers)
      //  {
      //      txtOnline.text += name;
      //  }
    }

    void Update()
    {
        if (NetworkServer.connections.Count > 0 && playersConnected != NetworkServer.connections.Count)
        {
            playersConnected = NetworkServer.connections.Count;
          //  Changed(playersConnected);
        //  NetworkServer.SendToAll()
        }
    }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }
        else
        {
          //  Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public string Name { get; set; }
    
    public void StartGame(Text name)
    {
        Name = name.text;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

       // onlinePlayers.Add(Name);
    }
}
