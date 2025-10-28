using UnityEngine;
using Game.GlobalFunc;
using System.Runtime.CompilerServices;
using System.Collections.Generic;

public class Projectile : Entity
{
    SpriteRenderer sr;
    [Header("Bullet Details")]
    [SerializeField] private float speed = 13f;

    [Header("Target Details")]
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private float suicideDistance = 14f;

    [Header("Summoner Details")]
    Transform summoner;
    private float damage;
    private int penCount;

    [Header("CCD Details")]
    static private readonly RaycastHit2D[] hits = new RaycastHit2D[16];
    CapsuleCollider2D capCol;
    ContactFilter2D contactFilter;

    private void Awake()
    {
        rb ??= GetComponent<Rigidbody2D>();
        capCol ??= GetComponent<CapsuleCollider2D>();
        sr ??= GetComponentInChildren<SpriteRenderer>();
        
        contactFilter = new ContactFilter2D { useLayerMask = true, layerMask = targetLayer, useTriggers = true };
    }

    private void FixedUpdate()
    {
        if (null == rb)
            return;

        ContinuousColliderDetection();
    }

    private void Update()
    {
        SuicideDistanceCheck();
    }

    public void StraightToTarget(Transform summoner, float damage, int penCount)
    {
        if (null == rb)
            return;

        this.summoner ??= summoner;
        this.damage = damage;
        this.penCount = penCount;

        rb.linearVelocity = transform.right * speed;
    }

    private void SuicideDistanceCheck()
    {
        // ��ȯ�� ��ü�� �������� ������ ��� �ı�
        // ( ĳ���Ͱ� �׾ �����ؾ� �ϴµ� ������ ������ )
        if (null == summoner)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
            return;
        }

        float distance = (summoner.position - transform.position).magnitude;

        // TODO :: ������Ʈ Ǯ��
        if (distance > suicideDistance)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    private void ContinuousColliderDetection()
    {
        // ���� �ð����� �̵��� �Ÿ��� 0�� �����ϸ� ������ �ʿ䰡 ����.
        Vector2 velocity = rb.linearVelocity;
        float deltaDistance = velocity.magnitude * Time.fixedDeltaTime;
        if (0 >= deltaDistance)
            return;

        int cnt = rb.Cast(velocity.normalized, contactFilter, hits, deltaDistance);

        IComparer<RaycastHit2D> comparer = System.Collections.Generic.Comparer<RaycastHit2D>.Create(
            (a, b) => a.distance.CompareTo(b.distance));

        if (0 < cnt)
        {
            System.Array.Sort(hits, 0, cnt, comparer);

            for (int i = 0; i < cnt; ++i)
            {
                RaycastHit2D hit = hits[i];
                GameObject target = hit.rigidbody ? hit.rigidbody.gameObject : hit.collider.gameObject;
                OnCollisionTriggerEvent(target);
            }
        }
    }

    private bool OnCollisionTriggerEvent(GameObject colObj)
    {
        if (null == colObj)
            return false;

        int targetMask = colObj.layer;

        if (!LayerUtill.CompareLayerMask(colObj, targetLayer))
            return false;

        IDamageable damageable = colObj.GetComponent<IDamageable>();
        if (null == damageable)
            return false;

        if (0 < penCount)
        {
            --penCount;
            damageable.TakeDamage(damage);
            return true;
        }

        // TODO :: ������Ʈ Ǯ��
        damageable.TakeDamage(damage);
        gameObject.SetActive(false);
        Destroy(gameObject);
        return true;
    }

    private void AsSoonSummonedOverlapCheck()
    {
        Collider2D[] overlapped = new Collider2D[8];
        int cnt = capCol.Overlap(contactFilter, overlapped);

        for (int i = 0; i < cnt; ++i)
        {
            GameObject targetObj = overlapped[i].attachedRigidbody.gameObject ? 
                overlapped[i].attachedRigidbody.gameObject : overlapped[i].gameObject;

            OnCollisionTriggerEvent(targetObj);
        }
    }

    private void OnEnable()
    {
        AsSoonSummonedOverlapCheck();
    }
}
