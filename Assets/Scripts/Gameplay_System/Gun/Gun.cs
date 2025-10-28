using NUnit.Framework.Interfaces;
using System;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Gun : Entity
{
    protected Player player;
    protected Player_StatComponent playerStatComp;
    protected Entity_MovementComponent entityMoveComp;
    protected Gun_StatComponent gunStatComp;
    private SpriteRenderer sr;

    [Header("Gun State Details")]
    [SerializeField] protected GrabState currState; 
    [SerializeField] protected string grabbedSortingLayer; 
    [SerializeField] protected string droppedSortingLayer;

    [Header("Offense Details")]
    [SerializeField] protected Transform launchPoint;
    [SerializeField] protected float rotateTime;
    [SerializeField] protected GameObject bulletPrefab;
    private float rotVel;
    private float targetDistForGizmo = 0f;
    private float fireTime;

    [Header("Collision Details")]
    [SerializeField] protected LayerMask targetLayer;
    [SerializeField] protected float searchBound;
    private Transform aimTarget;
   
    // TOOD:: health, combat

    // 화기 전용 스킬 ( 각자 다름, 클래스 파생 )

    public virtual void Initialize(Player player)
    {
        this.player ??= player;

        // - [ Default Attributes ] -
        rb ??= GetComponent<Rigidbody2D>();
        col ??= GetComponent<Collider2D>();
        sr ??= GetComponentInChildren<SpriteRenderer>();

        // - [ Components ] -
        playerStatComp ??= player.GetComponent<Player_StatComponent>();
        gunStatComp ??= GetComponent<Gun_StatComponent>();
        entityMoveComp ??= GetComponent<Entity_MovementComponent>();

        ChangeFollowHand(GrabState.dropped);
        gunStatComp?.CashingPlayerStat(playerStatComp);
    }

    public virtual void FixedUpdate()
    {
        MoveToPlayer();
    }

    public virtual void Update()
    {
        RotateToTarget();
        Flip();
        FireSystem();
    }

    private void RotateToTarget()
    {
        if (null == aimTarget)
        {
            aimTarget = RandomSearchObject();
            if (null == aimTarget)
                return;
        }

        Vector2 targetDir = aimTarget.position - transform.position;
        targetDistForGizmo = targetDir.magnitude;

        if (targetDir.sqrMagnitude < 1e-6f)
            return;

        float targetZ = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;

        float currentZ = transform.eulerAngles.z;
        float nextZ = Mathf.SmoothDampAngle(currentZ, targetZ, ref rotVel, rotateTime, Mathf.Infinity, Time.deltaTime);

        transform.rotation = Quaternion.Euler(0f, 0f, nextZ);
    }

    private Transform RandomSearchObject()
    {
        Collider2D target = Physics2D.OverlapCircle(transform.position, searchBound, targetLayer);

        if (null == target)
            return null;

        return target.transform;
    }

    private void MoveToPlayer()
    {
        if (null == entityMoveComp || null == player || null == gunStatComp)
            return;

        if (GrabState.grabbed != currState)
            return;

        entityMoveComp.MoveToTargetLerp(player.transform, gunStatComp.GetTotalSpeed());
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

    public void FireSystem()
    {
        if (null == bulletPrefab || null == aimTarget || null == gunStatComp)
            return;

        // 총알 발사속도 계산
        if (Time.time - fireTime < gunStatComp.GetCalcFireSpeed())
            return;

        GameObject summonedObj = Instantiate(bulletPrefab, launchPoint.position, transform.rotation);
        Projectile projectile = summonedObj?.GetComponent<Projectile>();

        // 여기서 대미지 계산, 관통 수 계산
        bool isCrit;
        float totalDamage = gunStatComp.GetTotalDamage(out isCrit, 1f);
        int totalPenCount = gunStatComp.GetTotalPenCount();

        projectile?.StraightToTarget(transform, totalDamage, totalPenCount);
        fireTime = Time.time;
    }

    private void OnDrawGizmos()
    {
        if (null == launchPoint)
            return;

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(launchPoint.position, 0.15f);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(launchPoint.position, launchPoint.position + transform.right * targetDistForGizmo);

        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, searchBound);
    }
}
