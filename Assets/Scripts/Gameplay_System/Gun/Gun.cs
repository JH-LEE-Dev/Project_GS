using NUnit.Framework.Interfaces;
using UnityEngine;

public class Gun : MonoBehaviour
{
    protected Player player;
    protected Player_StatComponent playerBaseState;
    protected Entity_MovementComponent entityMoveComp;
    private SpriteRenderer sr;

    [Header("Gun State Details")]
    [SerializeField] protected GrabState currState; 
    [SerializeField] protected string grabbedSortingLayer; 
    [SerializeField] protected string droppedSortingLayer;

    [Header("Offense Details")]
    [SerializeField] protected Transform launchPoint;
    protected Gun_StatComponent gunStats;
    // TOOD:: health, combat, Level

    // 화기 전용 스킬 ( 각자 다름, 클래스 파생 )

    public virtual void Initialize(Player player)
    {
        this.player ??= player;
        playerBaseState ??= player.GetComponent<Player_StatComponent>();
        entityMoveComp ??= GetComponent<Entity_MovementComponent>();
        sr ??= GetComponentInChildren<SpriteRenderer>();
        gunStats ??= GetComponent<Gun_StatComponent>();

        ChangeFollowHand(GrabState.grabbed);
        gunStats?.CashingPlayerStat(playerBaseState);
    }

    public virtual void Update()
    {
        MoveToPlayer();
    }

    private void MoveToPlayer()
    {
        if (null == entityMoveComp || null == player || null == gunStats)
            return;

        entityMoveComp.MoveToTargetLerp(player.transform, gunStats.GetTotalSpeed());
        Debug.Log("Gun - MoveToPlayer: Run.");
    }

    public void ChangeFollowHand(GrabState newState)
    {
        if (GrabState.grabbed == newState)
            sr.sortingLayerName = grabbedSortingLayer;
        else 
            sr.sortingLayerName = droppedSortingLayer;

        currState = newState;
    }

    private void OnDrawGizmos()
    {
        if (null == launchPoint)
            return;

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(launchPoint.position, 0.3f);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(launchPoint.position, launchPoint.position + transform.right * 1f);
    }
}
