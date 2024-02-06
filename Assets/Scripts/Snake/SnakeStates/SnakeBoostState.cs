using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBoostState : SnakeBaseState
{
    public float moveSpeed = 400;
    public float nuggetReleaseFrequency = 2f;

    IController controller;
    SnakeGrowthManager snakeGrowthManager;
    NuggetManager nuggetManager;
    Rigidbody2D snakeRB;

    // state
    float nuggetReleaseTimer = 0; 

    public override void EnterState(SnakeStateManager snake)
    {
        controller = snake.GetComponent<IController>();
        controller.Initialize();

        snakeGrowthManager = snake.GetComponent<SnakeGrowthManager>();
        snakeGrowthManager.UpdateMarkerFrequency(.0025f);

        GameObject nuggetManagerObj = GameObject.FindGameObjectWithTag("Nugget Manager");
        nuggetManager =  nuggetManagerObj.GetComponent<NuggetManager>();

        snakeRB = snake.GetComponent<Rigidbody2D>();
    }

    public override void FixedUpdate(SnakeStateManager snake)
    {
        snakeRB.AddRelativeForce(new Vector2(0, moveSpeed));
    }

    public override void OnCollisionEnter(SnakeStateManager snake, Collision2D collisionInfo)
    {

    }

    public override void OnCollisionExit(SnakeStateManager snake, Collision2D collisionInfo)
    {

    }

    public override void OnTriggerEnter(SnakeStateManager snake, Collider2D collisionInfo)
    {
        if (collisionInfo.gameObject.tag == "Body Segment" && collisionInfo.gameObject.transform.parent != snake.transform.parent) {
            snake.SwitchState(snake.snakeDeadState);
        } else if (collisionInfo.gameObject.tag == "Death Wall") {
            // snake.SwitchState(snake.snakeDeadState);
            snakeGrowthManager.DescreaseLength();
        }
    }

    public override void UpdateState(SnakeStateManager snake)
    {
        controller.UpdateRotation(snake);
        controller.UpdateBoost(snake);

        // handle nugget release
        nuggetReleaseTimer += Time.deltaTime;
        if (nuggetReleaseTimer >= nuggetReleaseFrequency) {
            nuggetReleaseTimer = 0;
            snakeGrowthManager.DescreaseLength();
        }
    }
}
