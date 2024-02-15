using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    //todo [SerializeField] private Gamemode _gamemode
    
    public void LoadSelectedScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
