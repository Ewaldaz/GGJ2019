using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager2 : MonoBehaviourPunCallbacks
{
    public PlayerPun PlayerPrefab;
    public PlayerPun LocalPlayer;

    private void Awake()
    {
        if (!PhotonNetwork.IsConnected)
        {
            SceneManager.LoadScene("WelcomeScreen");
            return;
        }
    }
    void Start()
    {
        LocalPlayer = PhotonNetwork.Instantiate("Dodgeanimated", Vector3.zero, Quaternion.identity).GetComponent<PlayerPun>();
      //  PlayerPun.RefreshInstance(ref LocalPlayer, PlayerPrefab);
        //  LocalPlayer = PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity).GetComponent<PlayerPun>();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
      //  PlayerPun.RefreshInstance(ref LocalPlayer, PlayerPrefab);
    }



    //public override void OnPlayerPropertiesUpdate(Photon.Realtime.Player target, ExitGames.Client.Photon.Hashtable changedProps)
    //{
    //    foreach (var change in changedProps)
    //        Debug.Log("Property " + change.Key + " of player " + target.UserId + " changed to " + change.Value);
    //}
}
