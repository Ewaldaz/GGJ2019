using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public string Name { get; set; }
    
    public void StartGame(Text name)
    {
        Name = name.text;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
