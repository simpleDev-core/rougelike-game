using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public string sceneName;
    void Start()
    {
        // Only specifying the sceneName or sceneBuildIndex will load the Scene with the Single mode
    }
    public void Load()
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);

    }
}