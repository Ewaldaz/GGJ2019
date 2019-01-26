using UnityEngine;
using UnityEngine.Networking;

public class PlayerStats : NetworkBehaviour
{
    public Material[] materials;

    //[SyncVar(hook = "UpdateMaterial")]
    //public int MaterialIndex = 0;

    //  [SyncVar]
    public string Name;

    //[SyncVar(hook = "UpdateMaterial")]
   // public Material material;

    //public void UpdateMaterial(int index)
    //{
    //    Debug.Log("MaterialIndex: " + MaterialIndex);
    //    gameObject.GetComponentInChildren<Renderer>().material = materials[MaterialIndex];
    //}
    void Start()
    {
        int index = Random.Range(0, materials.Length);
        gameObject.GetComponentInChildren<Renderer>().material = materials[index];
      //  gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ;

        // if (!isLocalPlayer)
        //// {
        //     Name = GameManager.Instance.Name;
        //// }
        // if (MaterialIndex > -1)
        // {
        //     Debug.Log(MaterialIndex);
        //     gameObject.GetComponentInChildren<Renderer>().material = materials[MaterialIndex];
        // }
    }
}
