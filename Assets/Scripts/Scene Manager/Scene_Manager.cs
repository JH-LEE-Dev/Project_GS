using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Manager : MonoBehaviour
{
    private static Scene_Manager instance;

    public static Scene_Manager GetInstance()
    {
        if (instance == null)
        {
            GameObject obj = new GameObject("SceneManager");
            instance = obj.AddComponent<Scene_Manager>();
            DontDestroyOnLoad(obj);
        }

        return instance;
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Debug.Log("Destroy ");
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}