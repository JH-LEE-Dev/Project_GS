using UnityEngine;
using Game.GlobalFunc;

public class Projectile : Entity
{
    SpriteRenderer sr;

    [Header("Details")]
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private float suicideDistance = 14f;
    private float damage;
    private int penCount;

    Transform summoner;


    private void Awake()
    {
        rb ??= GetComponent<Rigidbody2D>();
        col ??= GetComponent<Collider2D>();
        sr ??= GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        SuicideDistanceCheck();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (null == collision)
            return;

        GameObject colObj = collision.gameObject;
        int targetMask = colObj.layer;

        if (!LayerUtill.CompareLayerMask(colObj, targetLayer))
            return;

        IDamageable damageable = colObj.GetComponent<IDamageable>();
        if (null == damageable)
            return;

        if (0 < penCount)
        {
            --penCount;
            damageable.TakeDamage(damage);
            return;
        }

        // TODO :: ������Ʈ Ǯ��
        damageable.TakeDamage(damage);
        Destroy(gameObject);
    }

    
    public void StraightToTarget(Transform summoner, float damage, int penCount, float Speed)
    {
        if (null == rb)
            return;

        this.summoner = summoner;
        this.damage = damage;
        this.penCount = penCount;

        rb.linearVelocity = transform.right * 13f;//Speed;
    }

    private void SuicideDistanceCheck()
    {
        // ��ȯ�� ��ü�� �������� ������ ��� �ı�
        // ( ĳ���Ͱ� �׾ �����ؾ� �ϴµ� ������ ������ )
        if (null == summoner)
        {
            Destroy(gameObject);
            return;
        }

        float distance = (summoner.position - transform.position).magnitude;

        // TODO :: ������Ʈ Ǯ��
        if (distance > suicideDistance)
            Destroy(gameObject);
    }

    [ContextMenu("FireBullet")]
    public void StraightToTarget()
    {
        if (null == rb)
            return;

        rb.linearVelocityX = 15f;//Speed;
    }
}
