using UnityEngine;

public class Entity_MovementComponent : MonoBehaviour
{
    protected Rigidbody2D rb;

    [Header("Movement Details")]
    [SerializeField] protected float stopDist;

    [SerializeField] protected bool testMove;
    [SerializeField] protected Transform sampleTarget;
    [SerializeField] protected float sampleSpeed;

    private void Awake()
    {
        rb ??= GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!testMove || null == sampleTarget)
            return;

        MoveToTarget(sampleTarget, sampleSpeed);
        Debug.Log("Target: " + sampleTarget.name);
    }

    public virtual void MoveToTarget(Transform targetTransform, float speed)
    {
        if (null == rb)
            return;

        Vector3 targetDir = (targetTransform.position - transform.position);
        Vector2 finalDir = new Vector2(targetDir.x, targetDir.y).normalized;
        float distanceScalar = targetDir.magnitude;

        if (distanceScalar <= stopDist)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        rb.linearVelocity = finalDir * speed;
    }

    public void SetSpeed(float _speed)
    {
        sampleSpeed = _speed;
    }
}
