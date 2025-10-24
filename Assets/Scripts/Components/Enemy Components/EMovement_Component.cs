using UnityEngine;

public class EMovement_Component : Entity_MovementComponent
{
    [Header("Target Details")]
    private GameObject target;

    [Header("Movement Details")]
    private int facingDir = 1;
    [SerializeField] private float flipThreshold = 0.5f;

    public void SetTarget(GameObject player)
    {
        target = player;
    }

    private void Update()
    {
        HandleFlip();

        if (target != null)
            MoveToTarget(target.transform, sampleSpeed);
    }

    public override void MoveToTarget(Transform targetTransform, float speed, float scaleFactor = 1f)
    {
        if (target == null)
            return;

        //Target을 향한 방향 계산
        Vector3 dirToTarget = target.transform.position - transform.position;
        dirToTarget.Normalize();

        rb.linearVelocity = dirToTarget * sampleSpeed * scaleFactor;
    }

    private int CalcDir()
    {
        if (target == null)
            return 1;

        if (Mathf.Abs(target.transform.position.x - transform.position.x) < flipThreshold)
            return facingDir;

        if (target.transform.position.x > transform.position.x)
            return 1;
        else
            return 0;
    }

    public virtual void HandleFlip()
    {
        int nxtDir = CalcDir();

        if (facingDir != nxtDir)
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;

            transform.localScale = scale;
        }

        facingDir = nxtDir;
    }
}
