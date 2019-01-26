using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public Material[] materials;
    public int MaterialIndex = -1;
    public string Name { get; set; }

    void Start()
    {
        if (MaterialIndex == -1)
        {
            MaterialIndex = Random.Range(0, materials.Length);
        }
        gameObject.GetComponentInChildren<Renderer>().material = materials[MaterialIndex];
    }
}
