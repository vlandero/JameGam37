using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Obstacle
{
    [SerializeField] private float speed = 3f;
    private void Update()
    {
        if(isActive && isOnScreen)
        {
            Move();
        }
    }
    protected override void Deactivate()
    {
        base.Deactivate();
    }

    protected override void Activate()
    {
        base.Activate();
    }

    private void Move()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
}
