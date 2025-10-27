using System;
using UnityEngine;
using static UnityEngine.Analytics.IAnalytic;

public class Entity : MonoBehaviour
{
    [Header("Default Attributes")]
    protected Rigidbody2D rb;
    protected Collider2D col;

    public void SetVelocity(float x, float y)
    {
        if (null == rb)
            return;

        rb.linearVelocity = new Vector2(x, y);
    }

    public void SetVelocity(Vector2 inData)
    {
        if (null == rb)
            return;

        rb.linearVelocity = new Vector2(inData.x, inData.y);
    }
}
