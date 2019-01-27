using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class CameraFollow : MonoBehaviour
{
    public Transform Player;
    public float TransitionSpeed = 5f;
    public Vector3 Offset;
    public Vector3 LookBeneath; // this could be used for start.

    public void Start()
    {
    }

    public void Update()
    {
        if (!Player)
        {
            Player = FindObjectOfType<GameManager2>().LocalPlayer?.GetComponent<Transform>();
            //  Player = NetworkManager.singleton.client?.connection?.playerControllers.FirstOrDefault()?.gameObject?.GetComponent<Transform>();
        }
    }

    public void FixedUpdate()
    {
        if (Player)
        {
            Vector3 position = Player.position + Offset;
            Vector3 transition = Vector3.Lerp(transform.position, position, TransitionSpeed * Time.deltaTime);
            transform.position = transition;
        }
    }
}
