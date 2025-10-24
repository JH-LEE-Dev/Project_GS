using UnityEngine;

public class EMovement_Component : Entity_MovementComponent
{
    [Header("Target Details")]
    private GameObject target;

    [Header("Movement Details")]
    [SerializeField] private float speed;
    private int facingDir = 1;
    

    public void SetTarget(GameObject player)
    {
        target = player;
    }

    public void SetSpeed(float _speed)
    {
        speed = _speed;
    }

    private void Update()
    {
        HandleFlip();
        MoveToTarget();
    }

    private void MoveToTarget()
    {
        if (target == null)
            return;

        //Target을 향한 방향 계산
        Vector3 dirToTarget = target.transform.position - transform.position;
        dirToTarget.Normalize();

        transform.position += dirToTarget * speed * Time.deltaTime;
    }

    private int CalcDir()
    {
        if (target == null)
            return 1;

        if (target.transform.position.x > transform.position.x)
            return 1;
        else
            return 0;
    }

    public virtual void HandleFlip()
    {
        int nxtDir = CalcDir();

        if(facingDir != nxtDir)
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;

            transform.localScale = scale;
        }

        facingDir = nxtDir;
    }
}
