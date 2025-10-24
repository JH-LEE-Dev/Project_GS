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

    // ȭ�� ���� ��ų ( ���� �ٸ�, Ŭ���� �Ļ� )
    // ���� ������Ʈ ( ���� Gun���� ��� )

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
