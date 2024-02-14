using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    //todo [SerializeField] private Gamemode _gamemode
    // Start is called before the first frame update
    
    public void LoadSelectedScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
