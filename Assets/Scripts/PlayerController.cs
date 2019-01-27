using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using PlayerManager;

public class PlayerController : NetworkMessageHandler
{
    float x;
    float z;
    float jump;
    Vector3 prevLoc;
    public float lookSpeed = 10;
    bool running = false;
    bool up = false;
    bool fired = false;
    float jumpVelocity = 500f;

    public string playerID;
    public bool canSendNetworkMovement;
  // public float speed;
    public float networkSendRate = 5;
    public float timeBetweenMovementStart;
    public float timeBetweenMovementEnd;

    [Header("Lerping Properties")]
    public bool isLerpingPosition;
    public bool isLerpingRotation;
    public Vector3 realPosition;
    public Quaternion realRotation;
    public Vector3 lastRealPosition;
    public Quaternion lastRealRotation;
    public float timeStartedLerping;
    public float timeToLerp;

    public GameObject bulletPrefab;
    
    public float bulletSpeed = 100f;
    
    private void Start()
    {
        playerID = "player" + GetComponent<NetworkIdentity>().netId.ToString();
        transform.name = playerID;
        Manager.Instance.AddPlayerToConnectedPlayers(playerID, gameObject);

        if (isLocalPlayer)
        {
            Manager.Instance.SetLocalPlayerID(playerID);

         //   Camera.main.transform.position = transform.position + new Vector3(0, 0, -20);
          //  Camera.main.transform.rotation = Quaternion.Euler(0, 0, 0);

            canSendNetworkMovement = false;
            RegisterNetworkMessages();
        }
    }

    private void RegisterNetworkMessages()
    {
        Debug.Log("RegisterNetworkMessages");
        NetworkManager.singleton.client.RegisterHandler(movement_msg, OnReceiveMovementMessage);
    }

    private void OnReceiveMovementMessage(NetworkMessage _message)
    {
        Debug.Log("OnReceiveMovementMessage");
        PlayerMovementMessage _msg = _message.ReadMessage<PlayerMovementMessage>();

        if (_msg.objectTransformName != transform.name)
        {
            Manager.Instance.ConnectedPlayers[_msg.objectTransformName].GetComponent<PlayerController>().ReceiveMovementMessage(_msg.objectPosition, _msg.objectRotation, _msg.time);
        }
    }

    public void ReceiveMovementMessage(Vector3 _position, Quaternion _rotation, float _timeToLerp)
    {
        Debug.Log("ReceiveMovementMessage");
        lastRealPosition = realPosition;
        lastRealRotation = realRotation;
        realPosition = _position;
        realRotation = _rotation;
        timeToLerp = _timeToLerp;

        if (realPosition != transform.position)
        {
            isLerpingPosition = true;
        }

        if (realRotation.eulerAngles != transform.rotation.eulerAngles)
        {
            isLerpingRotation = true;
        }

        timeStartedLerping = Time.time;
    }



    void Update()
    {
        if (isLocalPlayer)
        {
            ReadInputs();
        }
    }

    void FixedUpdate()
    {
        prevLoc = transform.position;
        transform.position = transform.position + new Vector3(x, 0, z);

        #region Rotation and animations
        if (transform.position != prevLoc || running)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(transform.position - prevLoc), Time.fixedDeltaTime * lookSpeed);
              GetComponent<Animator>().Play("begimas");
        }
        else
        {
             GetComponent<Animator>().Play("Idle");
        }
        #endregion Rotation and animations

        #region Jump
        if (up)
        {
            if  (GetComponent<Rigidbody>().velocity.y <= 0 && GetComponent<Rigidbody>().velocity.y > -0.01)
            { 
                   
                GetComponent<Rigidbody>().AddForce(0, jumpVelocity, 0);
            }
            up = false;
        }
        #endregion Jump

        if (!canSendNetworkMovement)
        {
            canSendNetworkMovement = true;
            StartCoroutine(StartNetworkSendCooldown());
        }
    }

    private IEnumerator StartNetworkSendCooldown()
    {
        timeBetweenMovementStart = Time.time;
        yield return new WaitForSeconds((1 / networkSendRate));
        SendNetworkMovement();
    }

    private void SendNetworkMovement()
    {
        timeBetweenMovementEnd = Time.time;
        SendMovementMessage(playerID, transform.position, transform.rotation, (timeBetweenMovementEnd - timeBetweenMovementStart));
        canSendNetworkMovement = false;
    }

    public void SendMovementMessage(string _playerID, Vector3 _position, Quaternion _rotation, float _timeTolerp)
    {
        Debug.Log("SendMovementMessage");
        PlayerMovementMessage _msg = new PlayerMovementMessage()
        {
            objectPosition = _position,
            objectRotation = _rotation,
            objectTransformName = _playerID,
            time = _timeTolerp
        };

        NetworkManager.singleton.client.Send(movement_msg, _msg);
    }

    private void ReadInputs()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        jump = Input.GetAxis("Jump");
        if (jump != 0)
        {
            up = true;
        }
        
        if (Input.GetAxis("Fire1") != 0 && !fired)
        {
            fired = true;
            StartCoroutine(ShootBullet());
        }
    }

    private IEnumerator ShootBullet()
    {
        CmdShootBullet();
        yield return new WaitForSecondsRealtime(0.5f);
        fired = false;
    }

    [Command]
    private void CmdShootBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
        NetworkServer.Spawn(bullet);
        Destroy(bullet, 1.0f);
    }
    
}
