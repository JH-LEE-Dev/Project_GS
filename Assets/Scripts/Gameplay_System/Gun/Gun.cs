using NUnit.Framework.Interfaces;
using System;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Gun : MonoBehaviour
{
    protected Player player;
    protected Player_StatComponent playerBaseState;
    protected Entity_MovementComponent entityMoveComp;
    protected Gun_StatComponent gunStats;
    private SpriteRenderer sr;

    [Header("Gun State Details")]
    [SerializeField] protected GrabState currState; 
    [SerializeField] protected string grabbedSortingLayer; 
    [SerializeField] protected string droppedSortingLayer;

    [Header("Offense Details")]
    [SerializeField] protected Transform launchPoint;
    [SerializeField] protected float rotateTime;
    private float rotVal;

    [Header("Collision Details")]
    [SerializeField] protected LayerMask targetLayer;
    [SerializeField] protected float searchBoundary;
    private Transform attackTarget;

    float targetDistancewithDraw = 0f;
    
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
        RotateToTarget();
        Flip();
    }
    private void RotateToTarget()
    {
        if (null == attackTarget)
        {
            attackTarget = RandomSearchObject();
            if (null == attackTarget)
                return;
        }

        Vector2 targetDir = attackTarget.position - transform.position;
        targetDistancewithDraw = targetDir.magnitude;

        if (targetDir.sqrMagnitude < 1e-6f)
            return;

        float targetZ = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;

        float currentZ = transform.eulerAngles.z;
        float nextZ = Mathf.SmoothDampAngle(currentZ, targetZ, ref rotVal, rotateTime, Mathf.Infinity, Time.deltaTime);

        transform.rotation = Quaternion.Euler(0f, 0f, nextZ);
    }

    private Transform RandomSearchObject()
    {
        Collider2D target = Physics2D.OverlapCircle(transform.position, searchBoundary, targetLayer);

        if (null == target)
            return null;

        return target.transform;
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

    private void Flip()
    {
        float eulerAngleZ = transform.eulerAngles.z;
        float singedZ = Mathf.DeltaAngle(0f, eulerAngleZ);

        if (singedZ > 90f || singedZ < -90f)
            sr.gameObject.transform.localScale = new Vector3(1f, -1f, 1f);
        else
            sr.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    private void OnDrawGizmos()
    {
        if (null == launchPoint)
            return;

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(launchPoint.position, 0.15f);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(launchPoint.position, launchPoint.position + transform.right * targetDistancewithDraw);

        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, searchBoundary);
    }
}
