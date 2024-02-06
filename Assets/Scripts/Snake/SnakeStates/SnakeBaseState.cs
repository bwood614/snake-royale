using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SnakeBaseState
{
    public abstract void EnterState(SnakeStateManager snake);
    public abstract void UpdateState(SnakeStateManager snake);
    public abstract void OnCollisionEnter(SnakeStateManager snake, Collision2D collisionInfo);
    public abstract void OnTriggerEnter(SnakeStateManager snake, Collider2D collisionInfo);
    public abstract void OnCollisionExit(SnakeStateManager snake, Collision2D collisionInfo);
    public abstract void FixedUpdate(SnakeStateManager snake);

}
