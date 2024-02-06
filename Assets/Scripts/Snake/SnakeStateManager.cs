using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeStateManager : MonoBehaviour
{
    public SnakeBaseState currentState;
    public SnakeNormalState snakeNormalState = new SnakeNormalState();
    public SnakeBoostState snakeBoostState = new SnakeBoostState();
    public SnakeFollowState snakeFollowState = new SnakeFollowState();
    public SnakeDeadState snakeDeadState = new SnakeDeadState();

    // Start is called before the first frame update
    void Start()
    {
        currentState = snakeNormalState;
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        currentState.OnCollisionEnter(this, collisionInfo);
    }

    void OnCollisionExit2D(Collision2D collisionInfo)
    {
        currentState.OnCollisionExit(this, collisionInfo);
    }

    void OnTriggerEnter2D(Collider2D collisionInfo)
    {
        currentState.OnTriggerEnter(this, collisionInfo );
    }

    void FixedUpdate() {
        currentState.FixedUpdate(this);
    }

    public void SwitchState(SnakeBaseState newState) {
        currentState = newState;
        newState.EnterState(this);
    }
}
