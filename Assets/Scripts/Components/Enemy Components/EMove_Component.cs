using UnityEngine;

public class EMove_Component : Entity_MovementComponent
{
    /// <summary>
    /// Attribute
    /// </summary>
    [Header("Target Details")]
    private GameObject target;

    [Header("Speed Details")]
    private float speed;

    
    /// <summary>
    /// Functions
    /// </summary>
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
        MoveToTarget();
    }

    private void MoveToTarget()
    {
        if (target == null)
            return;

        //Target을 향한 방향 계산
        Vector2 dirToTarget = target.transform.position - transform.position;
        dirToTarget.Normalize();

        transform.position = dirToTarget * speed * Time.deltaTime;
    }
}
