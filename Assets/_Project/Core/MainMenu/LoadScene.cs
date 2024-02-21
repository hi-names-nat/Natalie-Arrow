using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField] private int sceneIndex;
    //todo [SerializeField] private Gamemode _gamemode
    
    public void LoadSelectedScene()
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
