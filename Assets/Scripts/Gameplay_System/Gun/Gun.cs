using UnityEngine;

public class Gun : MonoBehaviour
{
    protected Player player;

    // ȭ�Ⱑ ���� ������ ���� �ɷ� ����
    // ȭ�� ���� ��ų
    // ȭ�� ���� ��ȭ ����

    public void Initialize(Player player)
    {
        this.player??= player;
    }
}
