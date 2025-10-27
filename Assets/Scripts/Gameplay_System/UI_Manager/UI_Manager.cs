using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    //Instance var
    private static UI_Manager instance;

    [Header("UI Details")]
    [SerializeField] private GameObject wavePrepareUI_Obj;

    private WaveUI wavePrepareUI;

    public static UI_Manager GetInstance()
    {
        if (instance == null)
        {
            GameObject obj = new GameObject("UIManager");
            instance = obj.AddComponent<UI_Manager>();
            DontDestroyOnLoad(obj);
        }

        return instance;
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        GameObject wavePrepareUIInstance = Instantiate(wavePrepareUI_Obj);
        wavePrepareUI = wavePrepareUIInstance.GetComponent<WaveUI>();

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void UpdateWaveTimer(int curTime)
    {
        wavePrepareUI.SetTimerText(curTime);
    }
}
