using UnityEngine;

public enum GrabState {  grabbed, dropped, end }

public class Gun : MonoBehaviour
{
    protected Player player;
    protected Player_StatComponent playerBaseState;
    protected Entity_MovementComponent entityMoveComp;
    private SpriteRenderer sr;

    [Header("Gun State Details")]
    [SerializeField] protected GrabState currState; 
    [SerializeField] protected SortingLayer grabbedSortingLayer; 
    [SerializeField] protected SortingLayer droppedSortingLayer;

    [Header("Offense Details")]
    [SerializeField] protected Transform launchPoint;
    [SerializeField] protected Gun_StatComponent gunStats;

    // 화기 전용 스킬 ( 각자 다름, 클래스 파생 )
    // 레벨 컴포넌트 ( 공용 Gun에서 사용 )

    public virtual void Initialize(Player player)
    {
        this.player ??= player;
        playerBaseState ??= player.GetComponent<Player_StatComponent>();
        entityMoveComp ??= GetComponent<Entity_MovementComponent>();
        sr ??= GetComponentInChildren<SpriteRenderer>();

        currState = GrabState.dropped;
    }

    public virtual void Update()
    {
        MoveToPlayer();
    }

    private void MoveToPlayer()
    {
        if (null == entityMoveComp || null == player || null == playerBaseState)
            return;

        //entityMoveComp.MoveToTarget(player.transform, )
    }

    public void ChangeFollowHand(GrabState newState)
    {
        if (GrabState.grabbed == newState)
            sr.sortingLayerName = "GrabbedGun";

        currState = newState;
    }
}
