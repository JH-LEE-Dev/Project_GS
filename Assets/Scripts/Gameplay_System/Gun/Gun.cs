using UnityEngine;

public class Gun : MonoBehaviour
{
    protected Player player;
    protected Player_StatComponent baseStats;
    protected bool isFollowHand; 

    // 화기가 게임 내에서 가질 능력 스탯
    // 화기 전용 스킬
    // 화기 매판 강화 스탯

    public void Initialize(Player player)
    {
        this.player = player;
        baseStats = player.GetComponent<Player_StatComponent>();
        isFollowHand = false;
    }


}
