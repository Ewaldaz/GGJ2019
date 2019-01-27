using Photon.Pun;
using System.Collections;
using UnityEngine;

public class PlayerPun : MonoBehaviourPun
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
        
    private void Awake()
    {
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
            //  GetComponent<Animator>().Play("Running");
        }
        else
        {
            // GetComponent<Animator>().Play("Idle");
        }
        #endregion Rotation and animations

        #region Jump
        if (up)
        {
            if (GetComponent<Rigidbody>().velocity.y <= 0 && GetComponent<Rigidbody>().velocity.y > -0.01)
            {
                GetComponent<Rigidbody>().AddForce(0, jumpVelocity, 0);
            }
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
        yield return new WaitForSecondsRealtime(0.5f);
        fired = false;
    }    
}
