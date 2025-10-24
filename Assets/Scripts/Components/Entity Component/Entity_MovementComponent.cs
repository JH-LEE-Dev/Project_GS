using UnityEngine;

public class Entity_MovementComponent : MonoBehaviour
{
    private Rigidbody2D rb;

    [Header("Movement Details")]
    [SerializeField] private float stopDist;

    [SerializeField] private bool testMove;
    [SerializeField] private Transform sampleTarget;
    [SerializeField] private float sampleSpeed;

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
}
