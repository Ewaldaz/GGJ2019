using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class NetworkConnectionManager : MonoBehaviourPunCallbacks
{
    public Text txtUsername;
    public bool TriesToConnectToMaster;
    public bool TriesToConnectToRoom;
    public Text txtOnlineCount;
    public GameObject Top1;
    public GameObject Top2;

    public void StartGame()
    {
        Debug.Log("StartGame");
        Top1.SetActive(false);
        Top2.SetActive(true);

        ConnectToMaster();
    }

    public void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void ConnectToMaster()
    {
        //Settings (all optional and only for tutorial purpose)
        PhotonNetwork.OfflineMode = false;           //true would "fake" an online connection
        PhotonNetwork.NickName = txtUsername.text;       //to set a player name
        PhotonNetwork.AutomaticallySyncScene = true; //to call PhotonNetwork.LoadLevel()
        PhotonNetwork.GameVersion = "v1";            //only people with the same game version can play together

        TriesToConnectToMaster = true;
        //PhotonNetwork.ConnectToMaster(ip,port,appid); //manual connection
        PhotonNetwork.ConnectUsingSettings();           //automatic connection based on the config file in Photon/PhotonUnityNetworking/Resources/PhotonServerSettings.asset

    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        TriesToConnectToMaster = false;
        Debug.Log("Connected to Master!");

        //PhotonNetwork.CreateRoom("Peter's Game 1"); //Create a specific Room - Error: OnCreateRoomFailed
        //PhotonNetwork.JoinRoom("Peter's Game 1");   //Join a specific Room   - Error: OnJoinRoomFailed  
        PhotonNetwork.JoinRandomRoom(); //Join a random Room     - Error: OnJoinRandomRoomFailed  
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        TriesToConnectToMaster = false;
        TriesToConnectToRoom = false;
        Debug.Log(cause);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        //no room available
        //create a room (null as a name means "does not matter")
        PhotonNetwork.CreateRoom("GGJ2019", new RoomOptions { MaxPlayers = 20 });
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        Debug.Log(message);
        base.OnCreateRoomFailed(returnCode, message);
        TriesToConnectToRoom = false;
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        TriesToConnectToRoom = false;
        Debug.Log("Master: " + PhotonNetwork.IsMasterClient + " | Players In Room: " + PhotonNetwork.CurrentRoom.PlayerCount + " | RoomName: " + PhotonNetwork.CurrentRoom.Name);
        SceneManager.LoadScene("WorldPun");
    }

    // Update is called once per frame
    void Update()
    {
        if (txtOnlineCount)
        {
            txtOnlineCount.text = $"Online: {PhotonNetwork.CurrentRoom.PlayerCount}";

            List<Scoreboard> scores = new List<Scoreboard>();

            Debug.Log(PhotonNetwork.LocalPlayer);

            if (!(PhotonNetwork.LocalPlayer is null))
            {
                int score = PhotonNetwork.LocalPlayer.CustomProperties["score"] != null ? (int)PhotonNetwork.LocalPlayer.CustomProperties["score"] : 0;
                
                scores.Add(new Scoreboard() { Nickname = PhotonNetwork.LocalPlayer.NickName, Score = score });
                Debug.Log($"{PhotonNetwork.LocalPlayer.NickName}: {score}");
            }

            try
            {
                foreach (var player in PhotonNetwork.PlayerListOthers)
                {
                    int score = player.CustomProperties["score"] != null ? (int)player.CustomProperties["score"] : 0;
                    scores.Add(new Scoreboard() { Nickname = player.NickName, Score = score });
                }
            }
            catch { }

            foreach (var s in scores.OrderByDescending(x => x.Score))
            {
                txtOnlineCount.text += $"{Environment.NewLine}{s.Nickname}: {s.Score}";
            }

            
            //foreach (var player in GameObject.FindGameObjectsWithTag("Player"))
            //{
            //    txtOnlineCount.text += $"{Environment.NewLine}{player.GetComponent<PlayerPun>().Name}: {player.GetComponent<PlayerPun>().Score}";
            //}
        }
        if (txtUsername && PhotonNetwork.NickName != string.Empty)
        {
            txtUsername.text = PhotonNetwork.NickName;
        }
    }

    private struct Scoreboard
    {
        public string Nickname { get; set; }
        public int Score { get; set; }
    }
}
