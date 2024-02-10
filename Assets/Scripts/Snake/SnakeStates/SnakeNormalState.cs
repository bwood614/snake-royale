using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random=UnityEngine.Random;
using UnityEngine.UIElements;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;

public class SnakeNormalState : SnakeBaseState
{
    public float moveSpeed = 10;

    IController controller;
    SnakeGrowthManager snakeGrowthManager;

    NuggetManager nuggetManager;
    BulletManager bulletManager;

    Rigidbody2D snakeRB;

    // state

    public override void EnterState(SnakeStateManager snake)
    {
        controller = snake.GetComponent<IController>();
        controller.Initialize();

        snakeGrowthManager = snake.GetComponent<SnakeGrowthManager>();
        snakeGrowthManager.SetMoveSpeed(moveSpeed);

        GameObject nuggetManagerObj = GameObject.FindGameObjectWithTag("Nugget Manager");
        nuggetManager =  nuggetManagerObj.GetComponent<NuggetManager>();

        GameObject bulletManagerObj = GameObject.FindGameObjectWithTag("Bullet Manager");
        bulletManager =  bulletManagerObj.GetComponent<BulletManager>();

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
        }
    }

    public override void UpdateState(SnakeStateManager snake)
    {
                    float travelDistanceThisFrame = moveSpeed * Time.deltaTime;
            snake.transform.Translate(0, travelDistanceThisFrame, 0);
            snakeGrowthManager.AddDistanceTraveled(travelDistanceThisFrame);
                controller.UpdateRotation(snake);
        controller.UpdateBoost(snake);

        if (controller.CheckForShoot()) {
            bulletManager.FireBullet(snake.transform.position, snake.transform.rotation);
        }
    }

}
