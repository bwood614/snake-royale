using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SnakeDeadState : SnakeBaseState
{
    public override void EnterState(SnakeStateManager snake)
    {
        SnakeGrowthManager snakeGrowthManager = snake.GetComponent<SnakeGrowthManager>();
        List<Vector3> bodySegmentPositions = snakeGrowthManager.DestroyAllBodySegments();
        GameObject nuggetManagerObj = GameObject.FindGameObjectWithTag("Nugget Manager");
        NuggetManager nuggetManager =  nuggetManagerObj.GetComponent<NuggetManager>();
        bodySegmentPositions.ForEach(position => nuggetManager.AddNuggetAtPosition(position));
    }

    public override void FixedUpdate(SnakeStateManager snake)
    {

    }

    public override void OnCollisionEnter(SnakeStateManager snake, Collision2D collisionInfo)
    {

    }

    public override void OnCollisionExit(SnakeStateManager snake, Collision2D collisionInfo)
    {
    }

    public override void OnTriggerEnter(SnakeStateManager snake, Collider2D collisionInfo)
    {

    }

    public override void UpdateState(SnakeStateManager snake)
    {

    }
}
