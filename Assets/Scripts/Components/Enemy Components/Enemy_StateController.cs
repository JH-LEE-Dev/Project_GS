using UnityEngine;

public class Enemy_StateController : MonoBehaviour
{
    [Header("Animation Control Details")]
    private Animator animator;
    private EnemyAnimState currentState;
    private Rigidbody2D rb;
    bool bMove = false;
    bool bIdle = true;
    float moveSpeedMultiplier = 1f;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        currentState = EnemyAnimState.Idle;
        rb = GetComponent<Rigidbody2D>();
    }

    public void ChangeState(EnemyAnimState newState)
    {
        if (currentState == newState)
            return;

        currentState = newState;

        switch (currentState)
        {
            case EnemyAnimState.Idle:
                break;
        }
    }

    private void Update()
    {
        HandleMoveState();
    }

    public EnemyAnimState GetCurrentState()
    {
        return currentState;
    }

    public void HandleMoveState()
    {
        bool nxtbMove = false;

        if (rb.linearVelocity.magnitude > 0)
            nxtbMove = true;
        else
            nxtbMove = false;

        if (bMove == nxtbMove)
            return;

        bMove = nxtbMove;

        if (bMove == true)
            bIdle = false;
        else
            bIdle = true;

        animator.SetBool("bMove", bMove);
        animator.SetBool("bIdle", bIdle);
    }

    public void SetMoveSpeedMultiplier(float speedMultiplier)
    {
        moveSpeedMultiplier = speedMultiplier;
        animator.SetFloat("moveSpeedMultiplier", moveSpeedMultiplier);
    }
}
