using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WaveUI : MonoBehaviour, IUIAction
{
    [Header("UI Details")]
    [SerializeField] GameObject wavePrepareTimeUI;
    [SerializeField] GameObject waveDeclareUI;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text declareText;
    [SerializeField] private string waveStartDeclareStr;
    [SerializeField] private string prepareStartDeclareStr;
    [SerializeField] private string bossStartDeclareStr;

    public void OnEnable()
    {
        Game_Controller.OnDeclareState += HandleDeclareStateUI;
        Game_Controller.OnStartState += HandleStartStateUI;
    }

    public void OnDisable()
    {
        Game_Controller.OnDeclareState -= HandleDeclareStateUI;
        Game_Controller.OnStartState -= HandleStartStateUI;
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }

    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    public void HideTimerUI()
    {
        timerText.gameObject.SetActive(false);
    }

    public void ShowTimerUI()
    {
        timerText.gameObject.SetActive(true);
    }

    public void SetTimerText(int curTime)
    {
        timerText.text = curTime.ToString();
    }

    public void ShowDecalreUI()
    {
        declareText.gameObject.SetActive(true);
    }

    public void HideDecalreUI()
    {
        declareText.gameObject.SetActive(false);
    }

    void HandleDeclareStateUI(GameState state)
    {
        HideTimerUI();
        ShowDecalreUI();

        switch (state)
        {
            case GameState.Prepare:
                declareText.SetText(prepareStartDeclareStr);
                break;
            case GameState.Wave:
                declareText.SetText(waveStartDeclareStr);
                break;
            case GameState.BossWave:
                declareText.SetText(bossStartDeclareStr);
                break;
        }
    }

    void HandleStartStateUI()
    {
        ShowTimerUI();
        HideDecalreUI();
    }
}
