using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBoostState : SnakeBaseState
{
    public float moveSpeed = 20;
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
        snakeGrowthManager.SetMoveSpeed(moveSpeed);

        GameObject nuggetManagerObj = GameObject.FindGameObjectWithTag("Nugget Manager");
        nuggetManager =  nuggetManagerObj.GetComponent<NuggetManager>();

        snakeRB = snake.GetComponent<Rigidbody2D>();
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
        if (collisionInfo.gameObject.tag == "Body Segment" && collisionInfo.gameObject.transform.parent != snake.transform.parent) {
            snakeGrowthManager.DescreaseLength();
        } else if (collisionInfo.gameObject.tag == "Death Wall") {
            // snake.SwitchState(snake.snakeDeadState);
            snakeGrowthManager.DescreaseLength();
        }
    }

    public override void UpdateState(SnakeStateManager snake)
    {
        // move snake head forward
        float travelDistanceThisFrame = moveSpeed * Time.deltaTime;
        snake.transform.Translate(0, travelDistanceThisFrame, 0);
        snakeGrowthManager.AddDistanceTraveled(travelDistanceThisFrame);

        // update rotation and boost state based on player input
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
