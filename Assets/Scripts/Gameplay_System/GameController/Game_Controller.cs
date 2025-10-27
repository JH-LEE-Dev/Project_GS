using System;
using System.Collections;
using UnityEngine;

public class Game_Controller : MonoBehaviour
{
    public static event Action OnStartState;
    public static event Action OnStartWave;
    public static event Action<GameState> OnDeclareState;
    
    [Header("Wave Control")]
    [SerializeField] private int waveCnt = 15;
    [SerializeField] private float prepareTime = 150f;
    [SerializeField] private float waveTime = 150f;
    private GameState gameState = GameState.Prepare;
    private float curTime;
    private int curWaveCnt;
    [SerializeField] private float delayTime = 3;
    bool bWaveChanged = false;

    private void Awake()
    {
        curTime = 0;
        curWaveCnt = 0;
        gameState = GameState.SceneChanged;
    }

    private void Start()
    {

    }

    private void Update()
    {
        UpdateTimer();
    }

    private void UpdateTimer()
    {
        curTime -= Time.deltaTime;

        if (curTime < 0)
            curTime = 0;

        int flooredTime = (int)(curTime - Time.deltaTime);

        UI_Manager.GetInstance().UpdateWaveTimer(flooredTime);
        
        if (flooredTime <= 0 && bWaveChanged == false)
            HandleGameState();
    }

    private void ChangeGameState(GameState state)
    {
        if (gameState == state)
            return;

        gameState = state;

        switch (state)
        {
            case GameState.Prepare:
                curTime = (int)prepareTime;
                break;
            case GameState.Wave:
                curTime = (int)waveTime;
                break;
            case GameState.BossWave:
                break;
        }

        bWaveChanged = false;

        OnStartState.Invoke();
    }

    private void HandleGameState()
    {
        bWaveChanged = true;

        switch (gameState)
        {
            case GameState.Prepare:

                StartCoroutine(HandlePrepareEnd());

                break;
            case GameState.Wave:

                StartCoroutine(HandleStartWave());

                break;
            case GameState.SceneChanged:
            case GameState.BossWave:

                StartCoroutine(HandleStartPrepare());

                break;
        }
    }

    private IEnumerator HandleStartWave()
    {
        ++curWaveCnt;

        GameState nxtState = GameState.Wave;

        if (curWaveCnt == waveCnt)
        {
            nxtState = GameState.BossWave;
        }

        OnDeclareState.Invoke(nxtState);
 
        yield return new WaitForSeconds(delayTime);

        if (nxtState == GameState.BossWave)
        {
            ChangeGameState(GameState.BossWave);
        }
        else
        {
            curTime = waveTime;
            OnStartWave.Invoke();
        }
    }

    private IEnumerator HandlePrepareEnd()
    {
        OnDeclareState.Invoke(GameState.Wave);

        yield return new WaitForSeconds(delayTime);

        OnStartWave.Invoke();
        ChangeGameState(GameState.Wave);
    }

    private IEnumerator HandleStartPrepare()
    {
        OnDeclareState.Invoke(GameState.Prepare);

        yield return new WaitForSeconds(delayTime);

        ChangeGameState(GameState.Prepare);
    }
}
