using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenu_StartButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private string gameSceneName = "Project_GS";

    private void OnStartButtonClicked()
    {
        Scene_Manager.GetInstance().LoadScene(gameSceneName);

        Debug.Log("게임 시작 버튼이 클릭되었습니다. " + gameSceneName + " 씬으로 전환합니다.");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnStartButtonClicked();
    }
}
