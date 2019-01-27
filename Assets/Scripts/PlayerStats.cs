using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public Material[] materials;
    private int positionRange = 30;

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

        var x = Random.Range(-positionRange, positionRange);
        var z = Random.Range(-positionRange, positionRange);
        gameObject.transform.position = new Vector3(x, gameObject.transform.position.y, z);
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
