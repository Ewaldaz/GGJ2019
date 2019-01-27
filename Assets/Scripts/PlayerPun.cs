using Photon.Pun;
using System.Collections;
using UnityEngine;

public class PlayerPun : MonoBehaviourPun, IPunObservable
{
    float x;
    float z;
    float jump;
    Vector3 prevLoc;
    public float moveSpeed = 20f;
    public float lookSpeed = 10;
    bool running = false;
    bool up = false;
    bool fired = false;
    float jumpVelocity = 500f;
    public int Score = 0;
    public string Name;
    protected Rigidbody Rigidbody;
    public ParticleSystem PeeParticles;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        //destroy the controller if the player is not controlled by me
        // if (!photonView.IsMine && GetComponent<PlayerController>() != null)
        //     Destroy(GetComponent<PlayerController>());
    }
    public static void RefreshInstance(ref PlayerPun player, PlayerPun Prefab)
    {
        Debug.Log("RefreshInstance");
        var position = Vector3.zero;
        var rotation = Quaternion.identity;
        if (player != null)
        {
            position = player.transform.position;
            rotation = player.transform.rotation;
            PhotonNetwork.Destroy(player.gameObject);
        }

        player = PhotonNetwork.Instantiate(Prefab.gameObject.name, position, rotation).GetComponent<PlayerPun>();
    }
    

    void Update()
    {
        if (photonView.IsMine)
        {
            ReadInputs();
            if (Name == string.Empty)
            {
                Name = PhotonNetwork.LocalPlayer.NickName;
            }
        }
    }

    public void IncreaseScore(int amount = 1)
    {
        if (photonView.IsMine)
        {
            Score += amount;

            if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("score"))
            {
                PhotonNetwork.LocalPlayer.CustomProperties.Remove("score");
            }
            PhotonNetwork.LocalPlayer.CustomProperties.Add("score", Score);
        }
    }

    void FixedUpdate()
    {
        prevLoc = transform.position;
        transform.position = transform.position + new Vector3(x * moveSpeed * Time.deltaTime, 0, z * moveSpeed * Time.deltaTime);

        #region Rotation and animations
        if (transform.position != prevLoc || running)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(transform.position - prevLoc), Time.fixedDeltaTime * lookSpeed);
              GetComponent<Animator>().Play("begimas");
        }
        else
        {
            if (GetComponent<Rigidbody>().velocity.y <= 0 && GetComponent<Rigidbody>().velocity.y > -0.01)
            {
                GetComponent<Animator>().Play("Idle");
            }
        }
        #endregion Rotation and animations

        #region Jump
        if (up)
        {
            if (GetComponent<Rigidbody>().velocity.y <= 0 && GetComponent<Rigidbody>().velocity.y > -0.01)
            {
                GetComponent<Rigidbody>().AddForce(0, jumpVelocity, 0);
            }
            GetComponent<Animator>().Play("jump");
            up = false;
        }
        #endregion Jump
        
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
        // if (PeeParticles != null)
        // {
        if (!PeeParticles.isPlaying)
        {
            var obj = Instantiate(PeeParticles, transform.position + new Vector3(0, 2, 0), transform.rotation);
            obj.Play();
        }
     //   obj.Play();
            yield return new WaitForSeconds(3f);
         //   GameObject.Destroy(obj, 3f);
       // }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(Name);
            stream.SendNext(Score);
            stream.SendNext(Rigidbody.position);
            stream.SendNext(Rigidbody.rotation);
            stream.SendNext(Rigidbody.velocity);
        }
        else
        {
            Name = (string)stream.ReceiveNext();
            Score = (int)stream.ReceiveNext();

            Debug.Log($"{Name}: {Score}");
            foreach (var player in PhotonNetwork.PlayerListOthers)
            {
                if (Name == player.NickName)
                {
                    if (player.CustomProperties.ContainsKey("score"))
                    {
                        player.CustomProperties.Remove("score");
                    }
                    player.CustomProperties.Add("score", Score);
                }
            }

            Rigidbody.position = (Vector3)stream.ReceiveNext();
            Rigidbody.rotation = (Quaternion)stream.ReceiveNext();
            Rigidbody.velocity = (Vector3)stream.ReceiveNext();
        }
    }
}
