using UnityEngine;
using Game.GlobalFunc;

public class Projectile : Entity
{
    SpriteRenderer sr;

    [Header("Details")]
    [SerializeField] private LayerMask targetLayer;
    private float damage;
    private int penCount;

    private void Awake()
    {
        rb ??= GetComponent<Rigidbody2D>();
        col ??= GetComponent<Collider2D>();
        sr ??= GetComponentInChildren<SpriteRenderer>();
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

        // TODO :: 나중엔 오브젝트 풀링으로 바꿔야 함
        damageable.TakeDamage(damage);
        Destroy(this);
    }

    
    public void StraightToTarget(float damage, int penCount, float Speed)
    {
        if (null == rb)
            return;

        this.damage = damage;
        this.penCount = penCount;

        rb.linearVelocity = transform.right * 13f;//Speed;
    }

    [ContextMenu("FireBullet")]
    public void StraightToTarget()
    {
        if (null == rb)
            return;

        rb.linearVelocityX = 15f;//Speed;
    }
}
