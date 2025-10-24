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

        Debug.Log("���� ���� ��ư�� Ŭ���Ǿ����ϴ�. " + gameSceneName + " ������ ��ȯ�մϴ�.");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnStartButtonClicked();
    }
}
