using UnityEngine;

public class Gun : MonoBehaviour
{
    protected Player player;
    protected Player_StatComponent baseStats;
    protected bool isFollowHand; 

    // ȭ�Ⱑ ���� ������ ���� �ɷ� ����
    // ȭ�� ���� ��ų
    // ȭ�� ���� ��ȭ ����

    public void Initialize(Player player)
    {
        this.player = player;
        baseStats = player.GetComponent<Player_StatComponent>();
        isFollowHand = false;
    }


}
